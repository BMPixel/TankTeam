using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankController : MonoBehaviour 
{
	public abstract Vector3 toward{get;}
	public abstract Vector3 direction{get;}
	public abstract bool IsNetWork{get;}
	public abstract float maxSpeed{get;}
	protected Rigidbody2D rb;
	protected bool isDied;
	
	protected virtual void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	protected virtual void Update () 
	{
		if(!isDied)
		{
			transform.right = toward;	
			if(!IsNetWork)
			{
				Vector2 targetV = direction*maxSpeed;
	//			Debug.Log(targetV);
				rb.velocity = Vector2.MoveTowards(rb.velocity,targetV,maxSpeed/6);
			}
			BroadcastMessage("notRotate");
		}else{ // die and not move
			rb.velocity = Vector3.zero;
		}
	}

	private void Die(){
		isDied = true;
	}
}
