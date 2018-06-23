using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBtn : MonoBehaviour {

	[SerializeField]
	private Object target;
	//testing
	public void Close(){
		(target as GameObject).SetActive(false);
	}

	public void Pause(){
		Time.timeScale = 0;
	}

	public void Resume(){
		Time.timeScale = 1;
	}

	public void GamePauseToggle(){
		Time.timeScale = 1 - Time.timeScale;
	}

	public void Active(){
		(target as GameObject).SetActive(true);
	}

	public void Debug_TeamMate(){
		BattleManager.PlaceATank("TankNormal",BattleManager.playerTeam);
	}

	public void Debug_Enemy(){
		int i = BattleManager.GetNewTeamId();
		TeamInfo team = new TeamInfo(i,"AI-"+i,TeamInfo.ControllType.AI);
		BattleManager.NewTeam(team);
		for (int j = 0; j < 6; j++)
		{
			BattleManager.PlaceATank("TankNormal",i);
		}
	}

	public void Debug_Money(){
		BattleManager.GetTeam(BattleManager.playerTeam).money += 300;
	}

	public void Fast(){
		foreach (var item in BattleManager.GetTeam(BattleManager.playerTeam).members)
		{
			item.GetComponent<PlayerController>().speed *= 2;
		}
	}
}
