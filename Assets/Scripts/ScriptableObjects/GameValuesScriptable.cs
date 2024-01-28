using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "GameValues", menuName = "Laughter/Game Values", order = 1)]
public class GameValuesScriptable :ScriptableObject
{
	public float MaxLaughter = 100;
	public float LaughterDecreaseRatePerSecond = 0.5f;
	public float LaughterStart;
	public int NewAssignmentAfterSecondsMin = 30;
	public int NewAssignmentAfterSecondsMax = 50;

	public int correctAfterXSpawned = 3;

	public ItemObject[] ItemObjects;

	public Assignment[] Assignments;
	//public GameObject[] Items;
	//public GameObject[] CombinedItems;
}
