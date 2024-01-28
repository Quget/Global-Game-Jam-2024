using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class SpawnItem : MonoBehaviour
{
    private GameValueService gameValueService;
    private AssignmentService assignmentService;
    public float conveyorSpeed = 2f;

    private ItemObject[] items;

	[SerializeField]
	private AudioClip spawnSound = null;

    private int spawnCount = 0;

	void Start()
    {
        gameValueService = Services.Instance.GetService<GameValueService>();
        assignmentService = Services.Instance.GetService<AssignmentService>();
		items = gameValueService.GameValues.ItemObjects.Where(i => !i.IsCombinedItem).ToArray();
        if (items.Length > 0)
        {
            InvokeRepeating("SpawnRandomObject", 0f, 5f);
        }
    }

    void SpawnRandomObject()
    {
        int randomIndex = Random.Range(0, items.Length);
        ItemObject spawnedObject = Instantiate(items[randomIndex], transform.position, Quaternion.identity);

        if (spawnCount == gameValueService.GameValues.correctAfterXSpawned)
        {
            string tag = assignmentService.CurrentAssignment.ItemTags[Random.Range(0, assignmentService.CurrentAssignment.ItemTags.Length)];

			var newItem = items.Where(e => e.tag == tag).FirstOrDefault();

            if(newItem != null)
            {
                spawnedObject = newItem;
			}
			spawnCount = 0;
		}

        spawnedObject.Throw(-transform.up * 200);
        if(spawnSound != null)
        {
			AudioSource.PlayClipAtPoint(spawnSound, Camera.main.transform.position, 1);
		}
        spawnCount++;
	}

    public void StartSpawning()
    {
        InvokeRepeating("SpawnRandomObject", 5f, 5f);
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }
}
    public class ConveyorItemController : MonoBehaviour
    {
        private void Update()
        {
            if (transform.position.x <= 9)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }