using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour 
{
	private Text textComp;
	public void setText(string text)
	{
		if(textComp == null)
		{
			textComp = GetComponent<Text>();
			if(textComp == null)
				Debug.LogError("Text Comp null!");
		}
		textComp.text = text;
	}
}
