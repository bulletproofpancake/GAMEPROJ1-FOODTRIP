using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public List<ObjectPoolItems> itemsToPool;
    private List<GameObject> pooledObjects;

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        
        foreach (var item in itemsToPool)
        {
            for (var i = 0; i < item.amountToPool; i++)
            {
                var obj = Instantiate(item.objectToPool, item.parent);
                obj.AddComponent<PooledObjectItem>().ID = item.id;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string id)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //we need to make sure that the object is not active
            //and that the object has the same id
            if (!pooledObjects[i].activeInHierarchy &&
                pooledObjects[i].GetComponent<PooledObjectItem>().ID == id)
            {
                return pooledObjects[i];
            }
        }

        //if all objects are currently in use
        //check if the object can expand and then instantiate a new object and add it to the pool
        foreach (var item in itemsToPool)
        {
            if(item.id == id)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool, item.parent);
                    obj.AddComponent<PooledObjectItem>();
                    obj.GetComponent<PooledObjectItem>().ID = item.id;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
    
}

[System.Serializable]
public class ObjectPoolItems
{
    public string id;
    public GameObject objectToPool;
    public int amountToPool;
    public Transform parent;
    public bool shouldExpand;
}

public class PooledObjectItem:MonoBehaviour
{
    private string _id;

    public string ID
    {
        get => _id;
        set => _id = value;
    }
}
