using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private List<ObjectPoolComponent> objectPoolComponents;
    private Dictionary<ObjectPoolTypeE, Queue<ObjectPoolItem>> pools;

    private Transform _tranform;

    private void Awake()
    {
        _tranform = transform;
    }

    private void Start()
    {
        ObjectPooling();
    }

    private void ObjectPooling()
    {
        pools = new Dictionary<ObjectPoolTypeE, Queue<ObjectPoolItem>>();

        foreach (ObjectPoolComponent poolComponent in objectPoolComponents)
        {
            GameObject getParent = new GameObject(poolComponent.name);
            getParent.transform.parent = _tranform;
            Queue<ObjectPoolItem> objectPool = new Queue<ObjectPoolItem>();

            for (int i = 0; i < poolComponent.poolSize; i++)
            {
                ObjectPoolItem obj = Instantiate(poolComponent.prefab).GetComponent<ObjectPoolItem>();
                obj.Setup(getParent.transform);
                objectPool.Enqueue(obj);
            }

            pools.Add(poolComponent.type, objectPool);
        }
    }

    public void InstantiateFromPool(ObjectPoolTypeE typeE, Vector3 position, Quaternion rotation)
    {
        ObjectPoolItem item = pools[typeE].Dequeue();
        item.gameObject.SetActive(true);
        item.Setup(position, rotation);

        pools[typeE].Enqueue(item);
    }
}
