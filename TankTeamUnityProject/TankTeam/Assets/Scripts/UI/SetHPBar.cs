using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHPBar : MonoBehaviour {

	private Image hpBar;
	// Use this for initialization
	
	void setHP(float percent)
	{
		if(hpBar == null)
		{
			hpBar = GetComponent<Image>();
			if(hpBar == null)
				Debug.LogError("hp Comp null!");
		}
		hpBar.transform.localScale = new Vector2(percent,1);
	}

	void setColor(Color color){
		hpBar.color = color;
	}
}
