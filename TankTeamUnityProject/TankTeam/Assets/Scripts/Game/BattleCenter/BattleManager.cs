using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manager class for lifecircle and provide global functions, it's sole in one game!
/// </summary>
public class BattleManager : MonoBehaviour {
	/// <summary>
	/// Used for produce random tankname.
	/// </summary>
	/// <value></value>
	public static string[] TANKNAMES = new string[4]{"TankNormal", "TankMissile" , "TankMercenary", "TankScout"};
	private static BattleManager ins;
    private static Dictionary<int, TeamInfo> teams;
	/// <summary>
	/// A map of all tanks alive in the game
	/// </summary>
	public static Dictionary<int, Tank> tanks;
	/// <summary>
	/// The index of player's team in {teams}
	/// </summary>
	public static int playerTeam = -1;

	public static int teamsCount{
		get{
			return teams.Count;
		}
	}

	private List<int> tempInts;

	void Awake () {
		tempInts = new List<int>(100);
		teams = new Dictionary<int, TeamInfo>();
		tanks = new Dictionary<int, Tank>();
		if(ins == null)
		{
			ins = this;
		}else
		{
			Debug.LogError("There is more than one BattleManager");
		}
		Debug.Log("---BattleManager instantiated!");
	}

	public void Start(){
		/* make player team */
		teams[0] = new TeamInfo(0,"bmpixel",TeamInfo.ControllType.Player);
		playerTeam = 0;
		for (int i = 0; i < 2; i++)
		{
			PlaceATank("TankNormal",0);
		}
		GetTeam(0).money +=100;

		/* make some enemies */
		for (int i = 1; i <= 10; i++)
		{
			teams[i] = new TeamInfo(i,"AI-"+i,TeamInfo.ControllType.AI);
			for (int j = 0; j < Random.value * 3; j++)
			{
				PlaceATank("TankNormal",i);
			}
		}


	}

	void Update () {
		/* Delete all teams with on alive player */
		tempInts.Clear();
		foreach (KeyValuePair<int,TeamInfo> team in teams)
		{
			if(!team.Value.Flush())
			{
				tempInts.Add(team.Key);
			}
		}
		foreach (var i in tempInts)
		{
			teams.Remove(i);
			if(i == playerTeam){  // no player
				playerTeam = -1;
				ResourcesManager.RequireObject<GameObject>("GameOverUI").SetActive(true);
			}
		}

		/* Make a new team each 100frames */
		if(Time.frameCount % 100 == 0){
			CreateAnAITeam();
		}
	}

    /// <summary>
    /// From id of teams to teamInfo
    /// </summary>
    /// <param name="id">Id of team</param>
    /// <returns>TeamInfo class intance</returns>
    public static TeamInfo GetTeam(int id)
    {
        if (teams.ContainsKey(id))
            return teams[id];
        return null;
    }

	/// <summary>
	/// Create a team with some tanks controlled by AI
	/// </summary>
	/// <returns>Team's Info</returns>
	public static TeamInfo CreateAnAITeam(){
		int tid = GetNewTeamId();
        TeamInfo t = new TeamInfo(tid, "AI-" + tid, TeamInfo.ControllType.AI);
        teams[tid] = t;
        float n = 5;
        if (playerTeam != -1)
        {
            n = Random.value * teams[playerTeam].members.Count;
        }
        for (int i = 0; i < n; i++)
        {
            PlaceATank(BattleManager.TANKNAMES[Mathf.FloorToInt(Random.value * BattleManager.TANKNAMES.Length)], tid);
        }
		return t;
	}

	/// <summary>
	/// Create a new tank to battle ground
	/// </summary>
	/// <param name="name">Type string of tank e.g "TankMissile"</param>
	/// <param name="teamId">Create tank for which team</param>
	/// <returns>The Instance of the tank</returns>
	public static GameObject PlaceATank(string name, int teamId){
		TeamInfo team = GetTeam(teamId);
		if(team == null){
			Debug.LogError("The Team does nor exist!!");
			return null;
		}
		Vector2 pos = GetTeam(teamId).centerPoint + new Vector2((float)(Random.value-0.5)*4f, (float)(Random.value-0.5)*4f);
		GameObject go = Instantiate(
					ResourcesManager.RequireObject<GameObject>(name),
					pos,
					Quaternion.identity
					);
		int id = ins.GetNewTankId();
		go.GetComponent<Tank>().Init(id,teamId,true,team.controllType,team.name+"-"+id);
		return go;
	}

	/// <summary>
	/// Get an unused tank id
	/// </summary>
	/// <returns>New tankId</returns>
	int GetNewTankId(){
		tempInts.Clear();
		foreach (KeyValuePair<int,Tank> t in tanks)
		{
			if(t.Value == null)
			{
				tempInts.Add(t.Key);
				continue;
			}
		}
		foreach (var i in tempInts)
		{
			tanks.Remove(i);
		}
		for (int i = 0; i < tanks.Count+1; i++)
		{
			if (!tanks.ContainsKey(i))
			{
				return i;
			}
		}
		Debug.LogError("tank");
		return tanks.Count+1;
	}

	/// <summary>
	/// Get a new teamId
	/// </summary>
	/// <returns>teamId</returns>
	public static int GetNewTeamId(){
		for (int i = 0; i < teams.Count+1; i++)
		{
			if (!teams.ContainsKey(i))
			{
				return i;
			}
		}
		Debug.LogError("team");
		return teams.Count+1;
	}

	/// <summary>
	/// Before quitting the game
	/// </summary>
	public static void Quit(){
		teams.Clear();
		tanks.Clear();
		Bullet.pool.Clear();
		Missile.pool.Clear();
	}

	
}
