using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOption : MonoBehaviour
{
    public string introduction;
    public int id;

    public void Active(int n)
    {
        if (n == id)
        {
            GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
            SendMessageUpwards("makeText", introduction);
        }
        else
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
    }

    public void Buy(int n)
    {
        if(id == n){
            ShopCenter.BuyATank(0, gameObject.name);
        }
    }

    public void Click()
    {
        SendMessageUpwards("ChildrenClick",id);
    }
}
