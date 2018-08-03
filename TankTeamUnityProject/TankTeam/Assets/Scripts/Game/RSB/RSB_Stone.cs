using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSB_Stone : RSB
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
    }

    public override void OnTriggerExit2D(Collider2D col)
    {
        
    }
}
