using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSpawning : MonoBehaviour
{
    private GameValueService gameValueService;
    GameObject[] items;
    public SpawnItem spawnItem;
    private bool isInSpawn;

    void Start()
    {
        gameValueService = Services.Instance.GetService<GameValueService>();
        items = gameValueService.GameValues.Items;
        isInSpawn = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "Conveyer Belt")
        {
            spawnItem.StopSpawning();
            StartCoroutine(DelayedDestroy(col.gameObject, 2f));
            isInSpawn = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Conveyer Belt")
        {
            spawnItem.StartSpawning();
            isInSpawn = false;
        }
    }

    private IEnumerator DelayedDestroy(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (obj != null && isInSpawn == true)
        {
            Destroy(obj);
        }
    }
}
