using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combine : MonoBehaviour
{
    [SerializeField]
    private Transform itemOutPosition;

    private GameValueService gameValueService;
    private bool instantiationDone = false;
    private List<Collider2D> collidersInside = new List<Collider2D>();
    private List<Collider2D> collidersToRemove = new List<Collider2D>();

    private void Awake()
    {
        gameValueService = Services.Instance.GetService<GameValueService>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Itemlayer"))
        {
            collidersInside.Add(col);

            if (collidersInside.Count == 2 && !instantiationDone)
            {
                StartCoroutine(InstantiateWithDelay());
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Itemlayer"))
        {
            collidersToRemove.Add(col);
        }
    }

    IEnumerator InstantiateWithDelay()
    {
        yield return new WaitForSeconds(1f);

        foreach (var assignment in gameValueService.GameValues.Assignments)
        {
            if(collidersInside
                .Where(c=> assignment.ItemTags.Contains(c.gameObject.tag))
                .Distinct()
                .Count() == 2)
            {
                instantiationDone = true;
                foreach (Collider2D collider in collidersInside)
                {
                    Destroy(collider.gameObject);
                }

                foreach (var combinedItem in gameValueService.GameValues.CombinedItems)
                {
                    if(assignment.ResultItemTag == combinedItem.tag)
                    {
                        Instantiate(combinedItem, itemOutPosition.position, Quaternion.identity);
                        break;
                    }
                }

                break;
            }
        }

        foreach (Collider2D colliderToRemove in collidersToRemove)
        {
            collidersInside.Remove(colliderToRemove);
        }
        collidersToRemove.Clear();
        instantiationDone = false;
    }
}

