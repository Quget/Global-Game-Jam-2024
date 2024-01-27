using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    private AssignmentService _assignmentService;
    private Assignment assignment;

    void Awake()
    {
		_assignmentService = Services.Instance.GetService<AssignmentService>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == _assignmentService.CurrentAssignment.ResultItemTag)
        {
            GameController.Instance.Laughter += 10;
            Destroy(col.gameObject);
        }

        if(col.tag != _assignmentService.CurrentAssignment.ResultItemTag)
        {
			GameController.Instance.Laughter -= 5;
			Destroy(col.gameObject);
        }
    }
}
