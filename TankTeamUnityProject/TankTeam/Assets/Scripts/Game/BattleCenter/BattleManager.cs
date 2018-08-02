using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

	private static BattleManager ins;
    private static Dictionary<int, TeamInfo> teams;
	public static Dictionary<int, Tank> tanks;
	public static int playerTeam = -1;

	public static int teamsCount{
		get{
			return teams.Count;
		}
	}

	private List<int> tempInts;

    public static TeamInfo GetTeam(int id){
		if(teams.ContainsKey(id))
			return teams[id];
		return null;
	}

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
		teams[0] = new TeamInfo(0,"bmpixel",TeamInfo.ControllType.Player);
		for (int i = 0; i < 2; i++)
		{
			PlaceATank("TankMissile",0);
		}
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
			}
		}

		if(Time.frameCount % 300 == 0){
			int tid = GetNewTeamId();
			TeamInfo t = new TeamInfo(tid,"AI-"+tid,TeamInfo.ControllType.AI);
			NewTeam(t);
			int n = 5;
			if(playerTeam != -1){
				n = teams[playerTeam].members.Count + 2;
			}
			for (int i = 0; i < Random.value * n; i++)
			{
				PlaceATank("TankNormal", tid);
			}
		}
	}

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

	public static void NewTeam(TeamInfo team){
		if(GetTeam(team.id) == null)
			teams[team.id] = team;
		else{
			Debug.LogError("A team ahs already exist!!");
		}
	}

	public static bool BuyATank(int tid, string tankName){
		TeamInfo team = GetTeam(tid);
		GameObject tank = ResourcesManager.RequireObject<GameObject>(tankName);
		if(team == null || tank == null){
			return false;
		}
		if(team.money >= (int)(tank.GetComponent<Tank>().strenghIndex * 25)){
			team.money -= (int)(tank.GetComponent<Tank>().strenghIndex * 25);
			PlaceATank(tankName,tid);
			return true;
		}
		return false;
	}
}
