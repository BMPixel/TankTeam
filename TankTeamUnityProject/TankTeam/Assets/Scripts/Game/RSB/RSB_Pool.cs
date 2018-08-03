using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSB_Pool : RSB
{

    // Use this for initialization
    void Start()
    {

    }

    public override void init(Vector2 pos, float rad)
    {
        base.init(pos,rad);
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("tigger");
        TankController tc = col.gameObject.GetComponent<TankController>();
        //Debug.Log(tc.gameObject.name);
        if (tc != null) {
            tc.speedAdjust/=2;
        }
    }

    public override void OnTriggerExit2D(Collider2D col)
    {
        TankController tc = col.GetComponent<TankController>();
        if (tc != null)
        {
            tc.speedAdjust*=2;
        }
    }
}
