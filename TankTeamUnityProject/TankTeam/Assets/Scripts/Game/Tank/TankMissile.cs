using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMissile : Tank
{
	public int fireDamage = 2;
	private TankController tc;
	public float fireCD;
	private float fireTime = 0;

    public override float strenghIndex
    {
        get
        {
            return 3;
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
			Missile b = Missile.GetMissile();
			SendMessage("RenderFire");
			//b.Init(teamId,fireDamage,tc.toward*10 + tc.direction*tc.maxSpeed,transform.position);
			b.Init(teamId,fireDamage,(Quaternion.Euler(0,0,(Random.value*60)-30))*tc.toward *10,transform.position);
		}
    }

    protected override void FirstShoot()
    {
    }

    protected override void Start () 
	{
		tc = GetComponent<TankController>();
		base.Start();
	}
	
	void Update () {
		
	}
}
