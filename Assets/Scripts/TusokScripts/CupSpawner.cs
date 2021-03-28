using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : MonoBehaviour
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
  }
  public void SpawnCup()
  {

    //checks if cups in scene are full
    if (currentSpawned >= maxCupAllowed)
    {

      Debug.LogWarning("Cups in the Scene are full!");
      return;

    }
    else
    {
      var cup = ObjectPoolManager.pool.GetPooledObject("Cup");

      cup.transform.position = spawnPoint.position;
      cup.SetActive(true);
      currentSpawned++;
    }

  }

  private void StartSpawn()
  {
    StartCoroutine(GetCup());
  }

  private IEnumerator GetCup()
  {

    yield return new WaitForSeconds(timeToSpawn);
    SpawnCup();
  }





}
