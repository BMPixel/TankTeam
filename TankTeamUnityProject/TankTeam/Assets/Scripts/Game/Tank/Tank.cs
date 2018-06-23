using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            if (value <= 0)
            {
                hp = 0;
                BroadcastMessage("Die");
            }
            else
            {
                hp = value;
			    gameObject.BroadcastMessage("setHP",HPPercent);
            }
        }
    }
    [SerializeField]
    private int totalHP = 20;
    public int teamId;
    public int tankId;
    public bool leader;

    public void Init(int tankId, int teamId, bool leader, TeamInfo.ControllType controllType, string name)
    {
		this.name = name;
        this.tankId = tankId;
        this.teamId = teamId;
        this.leader = leader;
        switch (controllType)
        {
            case TeamInfo.ControllType.Player:
            {
				gameObject.AddComponent<PlayerController>();
				break;
            }
            case TeamInfo.ControllType.AI:
			{
				gameObject.AddComponent<AIController>();
				break;
			}
            default:
            {
                Debug.LogError("wrong ControllerType");
				break;
            }
        }
		
		BattleManager.GetTeam(teamId).AddMember(this);
		BattleManager.tanks[tankId] = this;
    }

    protected virtual void Start()
    {
        FirstShoot();
        GameObject tag = null;
        if (leader)
        {
            tag = Instantiate<GameObject>(ResourcesManager.RequireObject<GameObject>("Tag"), transform);
            tag.BroadcastMessage("setText", BattleManager.GetTeam(teamId).name);
        }else
        {
            tag = Instantiate<GameObject>(ResourcesManager.RequireObject<GameObject>("TagNoName"), transform);
        }
        tag.BroadcastMessage("setHP", HPPercent);
        HP = totalHP;
    }

    private void Die()
    {
        Destroy(gameObject, 1f);
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public float HPPercent
    {
        get { return (float)HP / (float)totalHP; }
    }

    public abstract float strenghIndex { get; }

    protected abstract void Fire();

    protected abstract void FirstShoot();

    protected abstract void Action();

}
