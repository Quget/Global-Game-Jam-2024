using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AssignmentService: IServiceAble
{
	private readonly GameValueService _gameValueService;
	public AssignmentService(GameValueService gameValueService)
	{
		_gameValueService = gameValueService;
	}

	public Assignment GetRandomAssignment()
	{
		return _gameValueService.GameValues.Assignments[Random.Range(0, _gameValueService.GameValues.Assignments.Length)];
	}

	public Assignment GetRandomAssignmentByRequiredItemsCount(int count)
	{
		var assignmentsByCount = _gameValueService.GameValues.Assignments.Where(a => a.ItemTags.Length == count).ToArray();

		if (assignmentsByCount == null || assignmentsByCount.Count() == 0)
			return GetRandomAssignment();

		return assignmentsByCount[Random.Range(0, assignmentsByCount.Count())];
	}
}