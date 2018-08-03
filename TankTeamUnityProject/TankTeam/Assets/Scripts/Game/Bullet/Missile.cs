using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	static public List<Missile> pool = new List<Missile>();
	private Rigidbody2D rb;
	private Collider2D coll;
	private int teamId;
	private int damage;
	private float bornTime;
	private float speed = 0.1f;
	private Vector2 direction;
	private float directionSpeed;
	[SerializeField]
	private float lifeTime = 1;
	[SerializeField]
    private float acceleration = 0.6f;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
	}

	public void Init(int _teamId,int _damage,Vector2 _velocity,Vector2 pos)
	{
		speed = 0;
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		if(BattleManager.GetTeam(teamId) != null){
			foreach(Tank tank in BattleManager.GetTeam(teamId).members){
				if(tank != null){
					Physics2D.IgnoreCollision(coll,tank.GetComponent<Collider2D>(),false);
				}
			}
		}
		transform.position = pos;
		gameObject.SetActive(true);
		teamId = _teamId;
		damage = _damage;
		direction = _velocity;
		directionSpeed = direction.magnitude;
		rb.velocity = direction*speed;
		GetComponent<Animator>().SetBool("Break",false);
		foreach(Tank tank in BattleManager.GetTeam(teamId).members){
			if(tank != null){
				Physics2D.IgnoreCollision(coll,tank.GetComponent<Collider2D>(),true);
			}
		}
		bornTime = Time.fixedTime;
		
	}

	void Update(){ //check Alive
		if(Time.fixedTime - bornTime >= lifeTime){ // die
			GetComponent<Animator>().SetBool("Break",true);
		}

		if(speed < 3){
			speed+= acceleration*Time.deltaTime;
		}

		rb.velocity = direction * speed;
		transform.right = direction;

		RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position,6,Vector2.right,0.01f,LayerMask.GetMask("Tank"));
		float minDis = 1;
		Tank minTank = null;
		foreach (var h in hit)
		{
			Tank t = h.transform.GetComponent<Tank>();
			if(t != null && t.teamId != teamId){
				float dis = (t.transform.position - transform.position).sqrMagnitude;
				if(minTank == null || (dis < minDis && t.GetComponent<Tank>().HP > 0)){
					minDis = dis;
					minTank = t;
				}
			}
		}
		if(minTank != null){
			direction = Vector2.MoveTowards(direction,(minTank.transform.position - transform.position).normalized*directionSpeed,10*Time.deltaTime);
		}


	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		//Debug.Log("---Collision!!");
		Tank t = coll.gameObject.GetComponent<Tank>();
		if(t != null)
		{
			if(t.teamId == teamId)
			{
				transform.position += (Vector3)rb.velocity.normalized;
				return; // the same Team
			}
			TeamInfo team = BattleManager.GetTeam(teamId);
			if(t.HP > 0 && t.HP - damage <= 0 && team!=null){
				team.kill++;
				team.money += (int)(t.strenghIndex * 10);
			}
			t.HP -= damage;
		}
		GetComponent<Animator>().SetBool("Break",true);
		rb.velocity = Vector3.zero;
	}

	public void Recycle()
	{
		gameObject.SetActive(false);
	}

	static public Missile GetMissile()
	{
		foreach(Missile b in pool){
			if(!b.gameObject.activeInHierarchy)
			{
				return b;
			}
		}
		Missile bu = Instantiate<GameObject>(ResourcesManager.RequireObject<GameObject>("Missile"),ResourcesManager.trans).GetComponent<Missile>();
		pool.Add(bu);
		return bu;
	}

}
