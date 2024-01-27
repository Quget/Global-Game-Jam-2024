using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    private AssignmentService assignmentService;
    private Assignment assignment;
    private LaughterBar laughterBar = null;

    void Start()
    {
        assignmentService = Services.Instance.GetService<AssignmentService>();
        assignment = assignmentService.GetRandomAssignment();

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == assignment.ResultItemTag)
        {
            laughterBar.UpdateLaughter(+10);
            Destroy(col.gameObject);
        }

        if(col.tag != assignment.ResultItemTag)
        {
            laughterBar.UpdateLaughter(-5);
            Destroy(col.gameObject);
        }
    }


}
