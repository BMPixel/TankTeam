using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScout : Tank
{
	public int fireDamage = 10;
	private TankController tc;
	public float fireCD = 5;
	private float fireTime = 0;

    public override float strenghIndex
    {
        get
        {
            return 0.4f;
        }
    }

    protected override void Action()
    {
    }

    protected override void Fire()
    {
        if(Time.fixedTime - fireTime > fireCD){
			fireTime = Time.fixedTime;
			//Debug.Log(string.Format("TankNormal:{0} Fight!",name));
			Bullet b = Bullet.GetBullet();
			SendMessage("RenderFire");
			//b.Init(teamId,fireDamage,tc.toward*10 + tc.direction*tc.maxSpeed,transform.position);
			b.Init(teamId,fireDamage,tc.toward *20,transform.position);
            b.lifeTime = 0.2f;
		}
    }

    protected override void FirstShoot()
    {
    }

    protected override void Start () 
	{
		tc = GetComponent<TankController>();
		base.Start();
        tc.maxSpeed = 7f;
	}
	
	void Update () {
		
	}
}
