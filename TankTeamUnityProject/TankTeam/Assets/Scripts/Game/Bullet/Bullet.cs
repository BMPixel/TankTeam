using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	static public List<Bullet> pool = new List<Bullet>();
	private Rigidbody2D rb;
	private Collider2D coll;
	private int teamId;
	private int damage;
	private float bornTime;
	public float lifeTime = 1; // the lifetime of the Bullet in frame


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
	}

	public void Init(int _teamId,int _damage,Vector2 _velocity,Vector2 pos)
	{
		lifeTime = 1f;
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		if(BattleManager.GetTeam(teamId) != null){
			foreach(Tank tank in BattleManager.GetTeam(teamId).members){
				if(tank != null){
					Physics2D.IgnoreCollision(coll,tank.GetComponent<Collider2D>(),false);
				}
			}
		}
		gameObject.SetActive(true);
		teamId = _teamId;
		damage = _damage;
		rb.velocity = _velocity;
		transform.position = pos + _velocity.normalized/2;
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

	static public Bullet GetBullet()
	{
		Debug.Log("Test");
		foreach(Bullet b in pool){
			if(!b.gameObject.activeInHierarchy)
			{
				return b;
			}
		}
		Bullet bu = Instantiate<GameObject>(ResourcesManager.RequireObject<GameObject>("Bullet"),ResourcesManager.trans).GetComponent<Bullet>();
		pool.Add(bu);
		return bu;
	}

}
