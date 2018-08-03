using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCenter : MonoBehaviour
{

    [SerializeField]
    private int selected;
    // Use this for initialization
    void Start()
    {
        selected = 0;
        BroadcastMessage("Active", selected);
    }

    public void ChildrenClick(int n)
    {
        if (selected != n)
        {
            BroadcastMessage("Active", n);
            selected = n;

        }
        else
        {
            BroadcastMessage("Buy", n);
        }

    }

    public void makeText(string text)
    {
        BroadcastMessage("setText", text);
    }

    public static bool BuyATank(int tid, string tankName)
    {
        TeamInfo team = BattleManager.GetTeam(tid);
        GameObject tank = ResourcesManager.RequireObject<GameObject>(tankName);
        if (team == null)
        {
            return false;
        }
        if (tank != null)
        {
            if (team.money >= (int)(tank.GetComponent<Tank>().strenghIndex * 25))
            {
                team.money -= (int)(tank.GetComponent<Tank>().strenghIndex * 25);
                BattleManager.PlaceATank(tankName, tid);
                return true;
            }
        }
        else
        {
            switch (tankName)
            {
                case "Service_Fix":
                    {
                        foreach (var item in team.members)
                        {
                            if (team.money >= 2 && item.HPPercent != 1)
                            {
                                team.money -= 2;
                                item.HP += 5;
                            }
                        }
                        return true;
                    }
                default: return false;
            }
        }
        return false;
    }
}
