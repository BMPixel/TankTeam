using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoShow : MonoBehaviour {

	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(BattleManager.playerTeam != -1)
		{
			TeamInfo pt = BattleManager.GetTeam(BattleManager.playerTeam);
			text.text = string.Format("Money: {0}G\nKill: {1}\nTeamLeft: {2}\nTeamMates: {3}",pt.money,pt.kill,BattleManager.teamsCount,pt.members.Count);

		}else{
			text.text = "You Died";
		}
	}
}
