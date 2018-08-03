using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class based on TakController, make the actions responsed by keyboard and mouse
/// </summary>
public class PlayerController : TankController 
{
    /// <summary>
    /// Maxspeed during moving
    /// </summary>
    public float speed = 3f;
    private TeamInfo team;

    public override Vector3 toward{
        get{
            if(TouchController.tc.attackJoystick != null && TouchController.tc.attackJoystick.inputVector != Vector2.zero){
                return ((TouchController.tc.attackJoystick.inputVector*8 +team.centerPoint) - (Vector2)transform.position).normalized;
            }
            Vector2 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return (mp - (Vector2)transform.position).normalized;
        }
    }

    public override Vector3 direction{
        get{
            BattleManager.GetTeam(team.id).Flush(); // apply Camera location
            Camera.main.SendMessage("Catch");
            if(TouchController.tc.moveJoystick != null && TouchController.tc.moveJoystick.inputVector != Vector2.zero){
                return (((TouchController.tc.moveJoystick.inputVector.normalized*50) + team.centerPoint)
             - (Vector2)transform.position).normalized;
            }
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                return (((new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")).normalized*10) + team.centerPoint)
                - (Vector2)transform.position).normalized;
            }
            return Vector2.zero;
        }
    }

    public override bool IsNetWork{
        get{
            return false;
        }
    }

    public override float maxSpeed{
        get{
            return speed;
        }
        set{
            speed = value;
        }
    }

    protected override void Start () {
		base.Start();
        Tank tank = GetComponent<Tank>();
        team = BattleManager.GetTeam(tank.teamId);
	}
	
	protected override void Update () {
        base.Update();
        if(!isDied)
        {
            if(Input.GetMouseButton(0)){
                SendMessage("Fire");
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                SendMessage("Action");
            }
        }
        // if(team.money >= 100){ // But Tank Auto
        //     BattleManager.BuyATank(team.id,"TankMissile");
        // }
	}
}
