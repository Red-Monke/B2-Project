using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();

    //initialize pools for any given type and count
    public void InitializePool(GameObject prefab, int count)
    {
        if (!pooledObjects.ContainsKey(prefab))
        {
            pooledObjects[prefab] = new List<GameObject>();
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                pooledObjects[prefab].Add(obj);
            }
        }
    }

    //get the list of pooled objects by GameObject key
    public List<GameObject> GetPooledObjectsByType(GameObject type)
    {
        if (pooledObjects.ContainsKey(type))
        {
            return pooledObjects[type];
        }
        return new List<GameObject>();
    }

    //returns inactive gameobjects
    public GameObject ReturnRequest(GameObject type)
    {
        List<GameObject> list = GetPooledObjectsByType(type);
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                continue;
            }
            if (!list[i].activeInHierarchy)
            {
                list[i].SetActive(true);
                return list[i];
            }
        }
        return null;
    }
}
