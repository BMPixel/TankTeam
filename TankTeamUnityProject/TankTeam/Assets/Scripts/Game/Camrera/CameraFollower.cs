using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
	
	[SerializeField]
	private float speed;
	private float zPos;
	void Start () {
		zPos = transform.position.z;
	}
	
	void Update () {
		if(BattleManager.playerTeam != -1)
		{
			Vector3 pos = Vector2.MoveTowards(transform.position,BattleManager.GetTeam(BattleManager.playerTeam).centerPoint,speed*Time.deltaTime);
			pos.z = zPos;
			transform.position = pos;
		}
	}

	void Catch(){
		Vector3 pos = BattleManager.GetTeam(BattleManager.playerTeam).centerPoint;
		pos.z = zPos;
		transform.position = pos;
	}
}
