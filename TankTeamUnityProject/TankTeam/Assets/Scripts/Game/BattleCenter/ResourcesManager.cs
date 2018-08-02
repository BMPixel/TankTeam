using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour {

	[SerializeField]
	private List<ResourceKeyValuePair> resourcesList = new List<ResourceKeyValuePair>(); 
	private static Dictionary<string, Object> dict;

	public static Transform trans;
	void Awake(){
		if(dict == null){ // there shall only be one resourcemanager
			trans = transform;
			dict = new Dictionary<string, Object>();
			foreach(ResourceKeyValuePair r in resourcesList){
				if(r.key != null){
					dict.Add(r.key,r.res);
				}
			}
			Debug.Log("---ResourcesManager instantiated! ResSize:" + dict.Count);
		}else{
			Debug.LogError("---ResourcesManager is more than one!");
		}
	}

	static public T RequireObject<T>(string name) where T:class{
		if(dict.ContainsKey(name)){
			return dict[name] as T;
		}else{
			Debug.LogException(new System.Exception("the resource named " + name + " doesnt exist!!"));
			return null;
		}
	}
}
