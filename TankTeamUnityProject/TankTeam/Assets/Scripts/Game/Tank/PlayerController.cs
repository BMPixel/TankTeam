using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : TankController 
{
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
                return (((TouchController.tc.moveJoystick.inputVector.normalized*10) + team.centerPoint)
             - (Vector2)transform.position).normalized;
            }
            return (((new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")).normalized*10) + team.centerPoint)
             - (Vector2)transform.position).normalized;
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
        if(team.money >= 25){
            BattleManager.BuyATank(team.id,"TankNormal");
        }
	}
}
