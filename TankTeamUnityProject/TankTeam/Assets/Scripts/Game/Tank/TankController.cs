using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Basic class of PlayerController, AIController and NetworkController in the future.
/// </summary>
public abstract class TankController : MonoBehaviour
{
    /// <summary>
    /// The attack direction vector(the bullet follows this)
    /// </summary>
    /// <value>A vector</value>
    public abstract Vector3 toward { get; }
    /// <summary>
    /// The moving direction vector at a length of 1(the tank will moved following this)
    /// </summary>
    /// <value>A vector</value>
    public abstract Vector3 direction { get; }
    /// <summary>
    /// Whether Controlled by Network
    /// </summary>
    /// <value>Bool</value>
    public abstract bool IsNetWork { get; }
    public abstract float maxSpeed { get;set; }
    /// <summary>
    /// Rigidbody2D componment
    /// </summary>
    protected Rigidbody2D rb;
    /// <summary>
    /// this tag is used for letting the tank not shoting during dying anim
    /// </summary>
    protected bool isDied;
    /// <summary>
    /// when enter a poll, it should slow down
    /// </summary>
    public float speedAdjust = 1;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (!isDied)
        {
            /* change direction */
            transform.right = toward;
            if (!IsNetWork)
            {
                Vector2 targetV = direction * maxSpeed * speedAdjust;
                //			Debug.Log(targetV);
                rb.velocity = Vector2.MoveTowards(rb.velocity, targetV, maxSpeed / 6);
            }
            BroadcastMessage("notRotate");
        }
        else
        { // die and not move
            rb.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Called by animations in the tank
    /// </summary>
    private void Die()
    {
        isDied = true;
    }
}
