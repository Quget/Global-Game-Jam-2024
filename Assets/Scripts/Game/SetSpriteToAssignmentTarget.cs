using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetSpriteToAssignmentTarget : MonoBehaviour
{

	private SpriteRenderer spriteRenderer;
	private AssignmentService _assignmentService;
	private GameValueService _gameValueService;

	private Assignment assignment;
	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		_assignmentService = Services.Instance.GetService<AssignmentService>();
		_gameValueService = Services.Instance.GetService<GameValueService>();
		assignment = _assignmentService.CurrentAssignment;

	}

	private void Update()
	{
		if (assignment.ResultItemTag != _assignmentService.CurrentAssignment.ResultItemTag)
		{
			assignment = _assignmentService.CurrentAssignment;
			UpdateAssignment();
		}
	}

	private void UpdateAssignment()
	{
		ClearAssignments();

		if (assignment.ResultItemTag != null)
		{
			var lastSprite = GetSpriteByTag(assignment.ResultItemTag);
			spriteRenderer.sprite = lastSprite;
		}
	}

	//Very slow
	private Sprite GetSpriteByTag(string tag)
	{
		var itemObject = _gameValueService.GameValues.Items.Where(i => i.tag == tag).FirstOrDefault();
		if (itemObject != null)
		{
			return itemObject.GetComponent<SpriteRenderer>().sprite;
		}

		itemObject = _gameValueService.GameValues.CombinedItems.Where(i => i.tag == tag).FirstOrDefault();
		if (itemObject != null)
		{
			return itemObject.GetComponent<SpriteRenderer>().sprite;
		}
		return null;
	}
	private void ClearAssignments()
	{
		spriteRenderer.sprite = null;
	}

}
