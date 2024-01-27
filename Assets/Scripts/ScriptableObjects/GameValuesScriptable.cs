using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameValues", menuName = "Laughter/Game Values", order = 1)]
public class GameValuesScriptable :ScriptableObject
{
	public float MaxLaughter = 100;
	public float LaughterDecreaseRatePerSecond = 0.5f;
	public float LaughterStart;
	public Assignment[] Assignments;
	public GameObject[] Items;
	public GameObject[] CombinedItems;
}
