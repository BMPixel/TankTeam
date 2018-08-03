using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The basic for all tank in the game, including its hp, id and so on.
/// </summary>
public abstract class Tank : MonoBehaviour
{
    private float hp;
    public float HP
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
                hp = Mathf.Min(totalHP, value);
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

        /* Create control type for it */
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
            /* Set label with name on */
            tag = Instantiate<GameObject>(ResourcesManager.RequireObject<GameObject>("Tag"), transform);
            tag.BroadcastMessage("setText", BattleManager.GetTeam(teamId).name);
        }else
        {
            /* Label with no names on */
            tag = Instantiate<GameObject>(ResourcesManager.RequireObject<GameObject>("TagNoName"), transform);
        }
        /* Set hp display in label */
        tag.BroadcastMessage("setHP", HPPercent);
        HP = totalHP;
    }

    /// <summary>
    /// Die process functions --- only called by the animation on the tank
    /// </summary>
    private void Die()
    {
        Destroy(gameObject, 1f);
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public float HPPercent
    {
        get { return (float)HP / (float)totalHP; }
    }

    /// <summary>
    /// The level of it's power, define by the coder,
    /// <param>affect the money droped after dying, the price in shop, and wheter enemies are afraid of it</param>
    /// </summary>
    public abstract float strenghIndex { get; }

    /// <summary>
    /// Set when the player shots
    /// </summary>
    protected abstract void Fire();

    /// <summary>
    /// Set when the tank was just put into battlefield
    /// </summary>
    protected abstract void FirstShoot();

    /// <summary>
    /// Set when the player press the skill btn
    /// </summary>
    protected abstract void Action();

}
