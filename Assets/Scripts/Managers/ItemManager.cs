using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#region Item Type set up for pooling and spawning
[System.Serializable]
public class ItemType
{
    public GameObject itemPrefab;
    public List<GameObject> itemSpecificLocations;
    public List<Collider> itemAreaLocations;
    public int spawnCount;
    public bool rngSpawn;
    public bool spawnSpecific;
    public bool spawnArea;
}
#endregion
public class ItemManager : MonoBehaviour
{
    [Header("Item Types, Spawn Locations and RNG")]
    public List<ItemType> itemTypes;
    public ObjectPooler pooler;
    private void Start()
    {
        InitializeItemPools();
        SpawnItems();
    }

    void InitializeItemPools()
    {
        foreach(ItemType itemType in itemTypes)
        {
            if(!itemType.spawnArea)
            {
                pooler.InitializePool(itemType.itemPrefab, itemType.itemSpecificLocations.Count);
            }

            if(itemType.spawnArea)
            {
                pooler.InitializePool(itemType.itemPrefab, itemType.spawnCount);
            }
        }
    }

    void SpawnItems()
    {
        foreach(ItemType itemType in itemTypes)
        {
            //List<GameObject> itemsOfType = pooler.GetPooledObjectsByType(itemType.itemPrefab);
            List<Collider> spawnAreas = itemType.itemAreaLocations;

            for(int i = 0; i < itemType.itemSpecificLocations.Count; i++)
            {
                if(i < itemType.spawnCount)
                {
                    if(itemType.spawnSpecific == true && itemType.rngSpawn == false)
                    {
                        GameObject obj = pooler.ReturnRequest(itemType.itemPrefab);
                        obj.transform.position = itemType.itemSpecificLocations[i].transform.position;
                        obj.SetActive(true);
                    }
                    else if(itemType.spawnSpecific == true && itemType.rngSpawn == true)
                    {
                        int countMax = itemType.itemSpecificLocations.Count;

                        GameObject obj = pooler.ReturnRequest(itemType.itemPrefab);
                        obj.transform.position = new Vector3(
                            Random.Range(itemType.itemSpecificLocations[0].transform.position.x, itemType.itemSpecificLocations[countMax].transform.position.x),
                            itemType.itemSpecificLocations[i].transform.position.y,
                            Random.Range(itemType.itemSpecificLocations[0].transform.position.z, itemType.itemSpecificLocations[countMax].transform.position.z));
                        obj.SetActive(true);
                    }

                }
            }

            if(itemType.spawnArea == true && itemType.rngSpawn == true)
            {

                for (int i = 0; i < spawnAreas.Count; i++)
                {
                  
                    for (int j = 0; j < itemType.spawnCount; j++)
                    {
                        GameObject obj = pooler.ReturnRequest(itemType.itemPrefab);
                        obj.transform.position = new Vector3(
                                Random.Range(spawnAreas[i].bounds.min.x, spawnAreas[i].bounds.max.x),
                                spawnAreas[i].transform.position.y,
                                Random.Range(spawnAreas[i].bounds.min.z, spawnAreas[i].bounds.max.z));
                        obj.SetActive(true);
                        
                    }
                }
            }
        }
    }




}