using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    private GameValueService gameValueService;
    public float conveyorSpeed = 2f;
    GameObject[] items;
    void Start()
    {
        gameValueService = Services.Instance.GetService<GameValueService>();
        items = gameValueService.GameValues.Items;
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

    void OnTriggerExit2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, 0f);
    }
}


