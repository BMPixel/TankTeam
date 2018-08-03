using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionBtn : MonoBehaviour {

	[SerializeField]
	private Object target;

	/// <summary>
	/// Close a window
	/// </summary>
	public void Close(){
		(target as GameObject).SetActive(false);
	}

	/// <summary>
	/// Pause Game
	/// </summary>
	public void Pause(){
		Time.timeScale = 0;
	}

	/// <summary>
	/// Resume Game
	/// </summary>
	public void Resume(){
		Time.timeScale = 1;
	}

	/// <summary>
	/// Resume or pause the gamme
	/// </summary>
	public void GamePauseToggle(){
		Time.timeScale = 1 - Time.timeScale;
	}

	/// <summary>
	/// Show a window
	/// </summary>
	public void Active(){
		(target as GameObject).SetActive(true);
	}
	
	public void Restart(){
        BattleManager.Quit();
		SceneManager.LoadScene(0);
	}

	public void Quit(){
		BattleManager.Quit();
		Application.Quit();
	}

	/// <summary>
	/// Add some teammate controlled by player
	/// </summary>
	public void Debug_TeamMate(){
		BattleManager.PlaceATank("TankNormal",BattleManager.playerTeam);
	}

    /// <summary>
    /// Create a enemy team
    /// </summary>
	public void Debug_Enemy(){
		BattleManager.CreateAnAITeam();
	}

	/// <summary>
	/// Add 300rmb to player
	/// </summary>
	public void Debug_Money(){
		BattleManager.GetTeam(BattleManager.playerTeam).money += 300;
	}

	/// <summary>
	/// Time player's speed with 2
	/// </summary>
	public void Fast(){
		foreach (var item in BattleManager.GetTeam(BattleManager.playerTeam).members)
		{
			item.GetComponent<PlayerController>().speed *= 2;
		}
	}
}
