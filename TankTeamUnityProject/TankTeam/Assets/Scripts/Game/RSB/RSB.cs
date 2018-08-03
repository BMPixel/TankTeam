using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic class for all placeable objects e.g Water Hill Stone
/// </summary>
public abstract class RSB : MonoBehaviour
{
    
    public virtual void init(Vector2 pos, float rad)
    {
        transform.position = pos;
        transform.localScale = new Vector2(rad, rad);
    }

    public abstract void OnTriggerEnter2D(Collider2D col);

    public abstract void OnTriggerExit2D(Collider2D col);
}
