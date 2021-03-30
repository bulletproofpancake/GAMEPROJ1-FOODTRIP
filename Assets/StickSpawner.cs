using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickSpawner : MonoBehaviour
{
     [Tooltip("Make sure to create empty object and use it as reference for spawner")]
     [SerializeField]
     private Transform spawnPoint;

     [Space]
     [Tooltip("Set the amount of cups to spawn in the scene")]
     [Range(1, 10)]
     [SerializeField]
     private int maxCupAllowed;


     private int currentSpawned;

     [Space]
     [Tooltip("This is the time for the cup to spawn")]
     [Range(0f, 2f)]
     [SerializeField]
     private float timeToSpawn;

     private void Start()
     {
          currentSpawned = 0;
          spawnPoint = GameObject.Find("StickSpawner").transform;
     }
     public void SpawnStick()
     {

          //checks if cups in scene are full
          if (currentSpawned >= maxCupAllowed)
          {

               Debug.LogWarning("Sticks in the Scene are full!");
               return;

          }
          else
          {
               var cup = ObjectPoolManager.pool.GetPooledObject("Stick");

               cup.transform.position = spawnPoint.position;
               cup.SetActive(true);
               currentSpawned++;
          }

     }

     public void StartSpawn()
     {
          StartCoroutine(GetStick());
     }

     private IEnumerator GetStick()
     {

          yield return new WaitForSeconds(timeToSpawn);
          SpawnStick();
     }
}
