using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankNormal : Tank 
{
	public int fireDamage = 2;
	private TankController tc;
	public float fireCD;
	private float fireTime = 0;

  	public override float strenghIndex{
      get
      {
          return 1f;
      }
  	}
    // Use this for initialization
    protected override void Start () 
	{
		tc = GetComponent<TankController>();
		base.Start();
	}

    protected override void Action()
	{
		//Debug.Log(string.Format("TankNormal:{0} Action!",name));
  	}

  	protected override void Fire()
  	{
		if(Time.fixedTime - fireTime > fireCD){
			fireTime = Time.fixedTime - Random.value * fireCD;
			//Debug.Log(string.Format("TankNormal:{0} Fight!",name));
			Bullet b = Bullet.GetBullet();
			SendMessage("RenderFire");
			//b.Init(teamId,fireDamage,tc.toward*10 + tc.direction*tc.maxSpeed,transform.position);
			b.Init(teamId,fireDamage,tc.toward*10,transform.position);
		}
  	}

  	protected override void FirstShoot()
  	{
		//Debug.Log(string.Format("TankNormal:{0} FirstShoot!",name));
  	}
}
