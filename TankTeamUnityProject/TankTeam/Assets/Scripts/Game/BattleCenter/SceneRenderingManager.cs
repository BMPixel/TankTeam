using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneRenderingManager : MonoBehaviour
{

    public static Vector2 RandomPosition
    {
        get
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 p = new Vector2(Random.Range(ins.start.x, ins.end.x), Random.Range(ins.start.y, ins.end.y));
                if (Physics2D.BoxCastAll(p, new Vector2(2, 2), 0, Vector2.right, 0.1f).Length == 0)
                {
                    return p;
                }
            }
            return new Vector2(Random.Range(ins.start.x, ins.end.x), Random.Range(ins.start.y, ins.end.y));
        }
    }

    private static SceneRenderingManager ins;
    // Use this for initialization
    [SerializeField]
    private GameObject ground;
    [SerializeField]
	private Transform groundParent;
    private Vector2 start;
    [SerializeField]
    private Vector2 end;
    void Start()
    {
        if (ins == null)
        {
            ins = this;
        }
        else
        {
            Debug.LogErrorFormat("There is more than one SceneRenderingManager");
        }
        Debug.DrawLine(start, end, Color.red, 12f);
        //// adjust ground
        ground.transform.position = start;
        ground.GetComponent<SpriteRenderer>().size = end - start;
		groundParent = ground.transform.parent;
		for (int i = 0; i < 10; i++)
		{
			putRes("RSB_Stone");
            putRes("RSB_Pool");
            putRes("RSB_Water");
		}
    }

    private void putRes(string name)
    {
		GameObject go = ResourcesManager.RequireObject<GameObject>(name);
		Instantiate<GameObject>(go,groundParent);
		go.GetComponent<RSB>().init(SceneRenderingManager.RandomPosition, Random.Range(5,10));
    }
}
