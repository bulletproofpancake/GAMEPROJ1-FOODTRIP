using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyCupsScript : MonoBehaviour
{

     public static DirtyCupsScript Instance;
     public int maxAmountInScene = 3;
     public int currentDirtyCupsInScene = 0;

    public List<GameObject> dirtyCups; // not sure if this is still needed

     int index = 0;


     [SerializeField]
     private Transform[] dirtyCupSpawnPos = new Transform[3];

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
     {
          for (int i = 0; i < dirtyCupSpawnPos.Length; i++)
          {
            dirtyCupSpawnPos[i] = gameObject.GetComponent<Transform>();
            dirtyCupSpawnPos[i] = transform.GetChild(i);
          }
     }

     //make a function that references positino of the dirtyCupSpawnPositions

     public void SpawnHere()
     {
          var dirtycup = ObjectPoolManager.pool.GetPooledObject("DirtyCup");
          dirtycup.transform.position = dirtyCupSpawnPos[index].position;
         currentDirtyCupsInScene++;
          dirtycup.SetActive(true);
         // AddDirtyCups(dirtycup);
          index++;
          if (index >= 3)
          {
               index = 0;
          }
     }

    public void AddDirtyCups(GameObject obj)
    {
        dirtyCups.Add(obj);
    }

    public void RemoveDirtyCups(GameObject obj)
    {
        dirtyCups.Remove(obj);
    }
}