using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TusokTusokFoodSpawner : Singleton<TusokTusokFoodSpawner>
{
    [Tooltip("this is for the area for the food to spawn along x-axis")]
    [SerializeField]
    [Range(0f, 2f)]
    private float SpawnRange;

    [Tooltip("Amount of same food to spawn per click of button")]
    [Space]
    [SerializeField]
    private int batchToSpawn;
    public void Spawn(OrderData data) {

        var food = ObjectPoolManager.Instance.GetPooledObject(data.name);

        food.transform.position = new Vector2(Random.Range(-SpawnRange, SpawnRange), this.gameObject.transform.position.y);
        food.SetActive(true);

    }
}
