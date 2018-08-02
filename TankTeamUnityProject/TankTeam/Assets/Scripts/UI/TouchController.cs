using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

	public static TouchController tc;
	public Joystick moveJoystick;
	public Joystick attackJoystick;
	// Use this for initialization
	void Start () {
		if(tc != null){
			Debug.LogError("TouchController exist!");
		}
		tc = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
