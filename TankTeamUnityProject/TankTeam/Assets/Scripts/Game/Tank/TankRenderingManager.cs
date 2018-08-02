using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRenderingManager : MonoBehaviour 
{
	private Tank tank;
	void Start () {
		tank = GetComponent<Tank>();
		BroadcastMessage("setColor",BattleManager.GetTeam(tank.teamId).color);
	}
	
	void Update () {
		
	}

	void RenderFire(){
		BroadcastMessage("FireAnimate");
	}
}
