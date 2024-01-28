using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combine : MonoBehaviour
{
    [SerializeField]
    private Transform itemOutPosition;

    [SerializeField]
    private Animator craftingStationAnimator;

    [SerializeField]
    private bool addIsMaking = true;

    private GameValueService gameValueService;
    private bool instantiationDone = false;

    private bool isCreating = false;
    private List<Collider2D> collidersInside = new List<Collider2D>();
    private List<Collider2D> collidersToRemove = new List<Collider2D>();

    [SerializeField]
    private AudioClip machineGenerate = null;

	[SerializeField]
	private AudioClip machineFailed = null;

	[SerializeField]
	private AudioClip machineSuccess = null;

	[SerializeField]
	private AudioClip itemAdd = null;


	private void Awake()
    {
        gameValueService = Services.Instance.GetService<GameValueService>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Itemlayer"))
        {
            collidersInside.Add(col);

            if (addIsMaking)
            {
				col.gameObject.SetActive(false);
				AudioSource.PlayClipAtPoint(itemAdd, Camera.main.transform.position, 1);
			}
			    

			if (collidersInside.Count == 2 && !instantiationDone)
            {
                StartCoroutine(InstantiateWithDelay());
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (addIsMaking)
        {
            return;
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Itemlayer"))
        {
            collidersToRemove.Add(col);
        }
    }

    IEnumerator InstantiateWithDelay()
    {
        isCreating = true;
		craftingStationAnimator?.SetTrigger("Craft");

        if (!addIsMaking)
        {
			foreach (Collider2D collider in collidersInside)
			{
				collider.gameObject.SetActive(false);
			}
		}

        AudioSource.PlayClipAtPoint(machineGenerate, Camera.main.transform.position,1);
		yield return new WaitForSeconds(1f);
        bool correct = false;
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

                foreach (var combinedItem in gameValueService.GameValues.ItemObjects)
                {
                    if(assignment.ResultItemTag == combinedItem.tag)
                    {
                        var item = Instantiate(combinedItem, 
                            itemOutPosition != null?itemOutPosition.position:transform.position,
                            Quaternion.identity);

                        item.PlayCreateSound();

						correct = true;
						break;
                    }
                }

                break;
            }
        }

		AudioSource.PlayClipAtPoint(correct?machineSuccess:machineFailed, Camera.main.transform.position, 1);

		craftingStationAnimator?.SetTrigger("StopCraft");

		foreach (Collider2D colliderToRemove in collidersToRemove)
        {
            if(colliderToRemove != null)
				Destroy(colliderToRemove.gameObject);

        }

		foreach (Collider2D colliderToRemove in collidersInside)
		{
			if (collidersInside != null)
				Destroy(colliderToRemove.gameObject);

		}

		collidersInside.Clear();
		collidersToRemove.Clear();
		isCreating = false;
		instantiationDone = false;
    }
}

