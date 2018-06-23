using System.Collections;
using UnityEngine;

[System.Serializable]
public class ResourceKeyValuePair
{
    public string key;
    public UnityEngine.Object res;

    public ResourceKeyValuePair(string key, Object res)
    {
        this.key = key;
        this.res = res;
    }

    public ResourceKeyValuePair()
    {}
}