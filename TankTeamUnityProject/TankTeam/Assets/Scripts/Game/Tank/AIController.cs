using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : TankController{
	[SerializeField]
	private float speed = 3;
	private Vector2 _toward;
	private Vector2 _direction;
	private Tank self;
    public override Vector3 toward{
        get
        {
			return _toward;
        }
    }

    public override Vector3 direction{
        get
        {
			return _direction;
        }
    }

    public override bool IsNetWork{
        get
        {
			return false;
        }
    }

    public override float maxSpeed{
        get
        {
			return speed;
        }
    }

    // Use this for initialization
    protected override void Start () {
		base.Start();
		self = GetComponent<Tank>();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		if(!isDied) // Think clearly
		{
			if(Time.frameCount % 20 == 0){
				AIBrain();
			}
		}
	}

	private void AIBrain()
	{
		if(Time.frameCount % 60 == 0 && self.leader) //set wander pos
		{
			BattleManager.GetTeam(self.teamId).wanderTarget = SceneRenderingManager.RandomPosition;
		}
		Vector2 avgPos = Vector2.zero;
		Tank mostImportantOne = null;
		float importance = 0;
		int emeNum = 0;
		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position,new Vector2(16,8),0,Vector2.right,0.1f,LayerMask.GetMask("Tank"));
		foreach (var hit in hits) // get tanks nearby
		{
			Tank tank = hit.transform.GetComponent<Tank>();
			if(tank.teamId != self.teamId)
			{
				emeNum ++;
				float ti = tank.strenghIndex - 1f/(tank.HPPercent + 0.1f);
				if(ti > importance || mostImportantOne == null){
					importance = ti;
					mostImportantOne = tank;
				}
				avgPos += (Vector2)hit.point;
			}
		}
		
		if(emeNum > 0)
		{
			_toward = (mostImportantOne.transform.position - transform.position + new Vector3(Random.value-0.5f,Random.value-0.5f)).normalized;
			avgPos /= emeNum;
			//Debug.Log(Vector2.Distance(avgPos,transform.position) + " + " + avgPos);
			Debug.DrawLine(avgPos,avgPos + Vector2.left*0.1f);
			if(Vector2.Distance(avgPos,transform.position) > 6)
			{
				_direction = -((Vector2)transform.position - avgPos).normalized;
			}else
			{
				Debug.DrawLine(avgPos,avgPos);
				_direction = (avgPos-(Vector2)transform.position).normalized;
				_direction = Quaternion.Euler(0,0,90) * _direction;
			}
			SendMessage("Fire");
			if(self.HPPercent < 0.3){
				_direction = ((Vector2)transform.position - avgPos).normalized;
			}
		}else
		{
			Vector2 wt = BattleManager.GetTeam(self.teamId).wanderTarget;
			Vector2 cp = BattleManager.GetTeam(self.teamId).centerPoint;
			Vector2 tar = (wt-cp).normalized*4 + cp;
			_direction = Vector2.MoveTowards(_direction,(tar - (Vector2)transform.position).normalized,60*Time.deltaTime);
			_toward = _direction;
		}

		//as a leader
		if(self.leader){
			TeamInfo t = BattleManager.GetTeam(self.teamId);
			if(t.money > 25){
				BattleManager.BuyATank(self.teamId,"TankNormal"); //buy Tank
			}
		}
	}
}
