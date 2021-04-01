using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyCupsScript : MonoBehaviour
{

     public static DirtyCupsScript Instance;
     public int maxAmountInScene = 3;
     public int currentDirtyCupsInScene = 0;

     int index = 0;


     [SerializeField]
     private Transform[] dirtyCupSpawnPos = new Transform[3];

     private void Start()
     {
          for (int i = 0; i < dirtyCupSpawnPos.Length; i++)
          {
            dirtyCupSpawnPos[i] = GetComponent<Transform>();
            dirtyCupSpawnPos[i] = transform.GetChild(i);
          }
     }

     //make a function that references positino of the dirtyCupSpawnPositions

     public void SpawnHere()
     {
          var dirtycup = ObjectPoolManager.pool.GetPooledObject("DirtyCup");
          dirtycup.transform.position = dirtyCupSpawnPos[index].position;
          dirtycup.SetActive(true);
          index++;
          if (index >= maxAmountInScene)
          {
               index = 0;
          }
     }
}