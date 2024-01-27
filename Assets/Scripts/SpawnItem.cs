using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject[] items;
    float timer;

    void Start()
    {
        timer = 2f;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if ( timer <= 0 )
        {
            SpawnRandomObject();
            timer = 2f;
        }
    }

    void SpawnRandomObject()
    {
        int randomIndex = Random.Range(0, items.Length);

        GameObject spawnedObject = Instantiate(items[randomIndex], transform.position, Quaternion.identity);

        Debug.Log("Spawned: " + spawnedObject.name);
    }

}


