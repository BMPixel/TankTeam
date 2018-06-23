using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRotate : MonoBehaviour {

	// Update is called once per frame
	void notRotate () {
		transform.rotation = Quaternion.Euler(0,0,0);
	}
}
