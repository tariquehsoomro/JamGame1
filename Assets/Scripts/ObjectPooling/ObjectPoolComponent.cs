using System;
using UnityEngine;

[Serializable]
public class ObjectPoolComponent
{
    public string name;
    public ObjectPoolTypeE type;
    public GameObject prefab;
    public int poolSize;
}