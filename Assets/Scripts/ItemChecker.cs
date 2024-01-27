using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    private AssignmentService _assignmentService;

    void Awake()
    {
		_assignmentService = Services.Instance.GetService<AssignmentService>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_assignmentService.CurrentAssignment.ResultItemTag != null && col.gameObject.layer == LayerMask.NameToLayer("Itemlayer"))
        {
			if (col.tag == _assignmentService.CurrentAssignment.ResultItemTag)
			{
				GameController.Instance.Laughter += 10;
			}

			if (col.tag != _assignmentService.CurrentAssignment.ResultItemTag)
			{
				GameController.Instance.Laughter -= 5;
			}
			Destroy(col.gameObject);
		}
    }
}
