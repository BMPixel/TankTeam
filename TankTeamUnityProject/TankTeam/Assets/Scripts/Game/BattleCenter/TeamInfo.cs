using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamInfo{

    public enum ControllType{Player,AI,NetWork}
    public int id;
    public Vector2 centerPoint;
    public List<Tank> members;
    public string name;
    public int money;
    public int kill;
    public Vector2 wanderTarget;
    public ControllType controllType;
    public Color color;

///TODO Leader need to be made

    public TeamInfo(int _id, string _name, ControllType type){
        id = _id;
        name = _name;
        members = new List<Tank>();
        controllType = type;
        color = new Color(Random.value,Random.value,Random.value);
        centerPoint = SceneRenderingManager.RandomPosition;
    }

    public TeamInfo(){
        id = -1;
        name = null;
        members = new List<Tank>();
        controllType = 0;
        color = new Color(Random.value,Random.value,Random.value);
        centerPoint = SceneRenderingManager.RandomPosition;
    }

    public void AddMember(Tank member,bool isLeader = false){
        members.Add(member);
    }

    public bool Flush(){
        centerPoint = Vector2.zero;
        for(int i = 0;i<members.Count;i++)
        {
            if(members[i] == null){ //delete null obj
                members.RemoveAt(i);
                continue;
            }
            centerPoint += (Vector2)members[i].transform.position;
        }
        if(members.Count == 0)
            return false;
        centerPoint /= members.Count;
        return true;
        
    }
}