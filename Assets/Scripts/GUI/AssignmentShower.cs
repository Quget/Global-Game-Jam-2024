using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AssignmentShower : MonoBehaviour
{
	[SerializeField]
	private HorizontalLayoutGroup horizontalLayoutGroup;

	[SerializeField]
	private Image assignmentImagePrefab;

	private AssignmentService _assignmentService;
	private GameValueService _gameValueService;

	private Assignment assignment;
	private void Awake()
	{
		_assignmentService = Services.Instance.GetService<AssignmentService>();
		_gameValueService = Services.Instance.GetService<GameValueService>();
		assignment = _assignmentService.CurrentAssignment;
		
	}

	private void Start()
	{
		UpdateAssignment();
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
			foreach(var item in assignment.ItemTags)
			{
				Image image = Instantiate(assignmentImagePrefab, horizontalLayoutGroup.transform);
				var sprite = GetSpriteByTag(item);
				image.sprite = sprite;
			}

			Image lastImage = Instantiate(assignmentImagePrefab, horizontalLayoutGroup.transform);
			var lastSprite = GetSpriteByTag(assignment.ResultItemTag);
			lastImage.sprite = lastSprite;
		}
	}

	//Very slow
	private Sprite GetSpriteByTag(string tag)
	{
		var itemObject = _gameValueService.GameValues.ItemObjects.Where(i => i.tag == tag).FirstOrDefault();
		if (itemObject != null)
		{
			return itemObject.GetComponent<SpriteRenderer>().sprite;
		}
		return null;
	}
	private void ClearAssignments()
	{
		for(int i = 0; i < horizontalLayoutGroup.transform.childCount; i++)
		{
			Destroy(horizontalLayoutGroup.transform.GetChild(i).gameObject);
		}
	}
}
