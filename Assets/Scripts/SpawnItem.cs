using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject[] items;
    public float conveyorSpeed = 2f;

    void Start()
    {
        if (items.Length > 0)
        {
            InvokeRepeating("SpawnRandomObject", 0f, 2f);
        }
    }

    void SpawnRandomObject()
    {
        int randomIndex = Random.Range(0, items.Length);
        GameObject spawnedObject = Instantiate(items[randomIndex], transform.position, Quaternion.identity);

        Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = spawnedObject.AddComponent<Rigidbody2D>();
        }

        rb.velocity = new Vector2(-conveyorSpeed, 0f);

        Debug.Log("Spawned: " + spawnedObject.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, 0f);
    }
}


