using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlush : MonoBehaviour {

	private Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
	}

	void FireAnimate(){
		anim.Play("Light",0,0);
	}
}
