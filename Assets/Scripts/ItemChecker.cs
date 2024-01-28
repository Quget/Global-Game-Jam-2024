using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    private AssignmentService _assignmentService;

	[SerializeField]
	private AudioClip correct = null;

	[SerializeField]
	private AudioClip incorrect = null;
	void Awake()
    {
		_assignmentService = Services.Instance.GetService<AssignmentService>();
    }

	//void OnTriggerEnter2D(Collider2D col)
	void OnTriggerStay2D(Collider2D col)
	{

        if (_assignmentService.CurrentAssignment.ResultItemTag != null 
			&& col.gameObject.layer == LayerMask.NameToLayer("Itemlayer")
			 && col.gameObject.transform.parent == null)
        {
			if (col.tag == _assignmentService.CurrentAssignment.ResultItemTag)
			{
				GameController.Instance.Laughter += 10;
				AudioSource.PlayClipAtPoint(correct, Camera.main.transform.position, 1);
			}

			if (col.tag != _assignmentService.CurrentAssignment.ResultItemTag)
			{
				GameController.Instance.Laughter -= 5;
				AudioSource.PlayClipAtPoint(incorrect, Camera.main.transform.position, 1);
			}
			Destroy(col.gameObject);
		}
    }
}
