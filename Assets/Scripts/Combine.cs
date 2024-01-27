using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine : MonoBehaviour
{
    public GameObject[] combinedItems;
    public string tag1 = "Feather";
    public string tag2 = "Plant";

    private bool instantiationDone = false;
    private List<Collider2D> collidersInside = new List<Collider2D>();
    private List<Collider2D> collidersToRemove = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tag1) || other.CompareTag(tag2))
        {
            collidersInside.Add(other);

            if (collidersInside.Count == 2 && !instantiationDone)
            {
                StartCoroutine(InstantiateWithDelay());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tag1) || other.CompareTag(tag2))
        {
            collidersToRemove.Add(other);
        }
    }

    IEnumerator InstantiateWithDelay()
    {
        yield return new WaitForSeconds(1f);

        if (collidersInside.Exists(c => c.CompareTag(tag1)) && collidersInside.Exists(c => c.CompareTag(tag2)))
        {
            instantiationDone = true;
            foreach (Collider2D collider in collidersInside)
            {
                Destroy(collider.gameObject);
            }

            Instantiate(combinedItems[0], transform.position, Quaternion.identity);
        }

        foreach (Collider2D colliderToRemove in collidersToRemove)
        {
            collidersInside.Remove(colliderToRemove);
        }
        collidersToRemove.Clear();
        instantiationDone = false;
    }
}

