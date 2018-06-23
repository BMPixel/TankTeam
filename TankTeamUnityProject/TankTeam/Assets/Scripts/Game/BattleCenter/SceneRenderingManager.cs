using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneRenderingManager : MonoBehaviour {

	public static Bounds mapBound{
		get{
			return new Bounds(new Vector2(20,15),new Vector2(58,32));
		}
	}

	public static Vector2 RandomPosition{
		get{
			for(int  i = 0;i < 10; i++){
				Vector2 p = new Vector2(Random.value * mapBound.size.x,Random.value * mapBound.size.y) + (Vector2)mapBound.min;
				if(Physics2D.BoxCastAll(p,new Vector2(2,2),0,Vector2.right,0.1f).Length == 0)
				{
					return p;
				}
			}
			return new Vector2(Random.value * mapBound.size.x,Random.value * mapBound.size.x) + (Vector2)mapBound.min;
		}
	}

	public Tilemap tm;
	private static SceneRenderingManager ins;
	// Use this for initialization
	void Start () {
		if(ins == null){
			ins = this;
		}else
		{
			Debug.LogErrorFormat("There is more than one SceneRenderingManager");
		}
		Debug.DrawLine(mapBound.min,mapBound.max,Color.red,12f);
	}
}
