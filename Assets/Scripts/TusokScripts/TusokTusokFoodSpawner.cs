using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TusokTusokFoodSpawner : Singleton<TusokTusokFoodSpawner>
{
    [Tooltip("this is for the area for the food to spawn along x-axis")]
    [SerializeField]
    [Range(0f, 2f)]
    private float SpawnRange;

    [Tooltip("This is where you set the maximum capacity of the kawali")]
    [Range(5, 15)]
    public int maxKawaliCapacity;


    //current capacity 
    private int currentCapacity;

    public void Start() {
        currentCapacity = 0;
    }

    public void Spawn(OrderData data) {

        if (currentCapacity >= maxKawaliCapacity) {
            Debug.LogWarning("Capacity is full!");
            return;
        } else { 
            currentCapacity++;
            var food = ObjectPoolManager.Instance.GetPooledObject(data.name);
            food.transform.position = new Vector2(Random.Range(-SpawnRange, SpawnRange), this.gameObject.transform.position.y);
            food.SetActive(true);
        } 

    }
}
