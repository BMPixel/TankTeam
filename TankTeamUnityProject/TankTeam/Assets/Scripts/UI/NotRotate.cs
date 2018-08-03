using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make obj not rotate with parent, use SendMessage() to call it each frame
/// </summary>
public class NotRotate : MonoBehaviour {

	// Update is called once per frame
	void notRotate () {
		transform.rotation = Quaternion.Euler(0,0,0);
	}
}
