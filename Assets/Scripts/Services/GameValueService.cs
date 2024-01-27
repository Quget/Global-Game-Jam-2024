using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValueService : IServiceAble
{
	public GameValuesScriptable GameValues { get; private set; }
	public GameValueService()
	{
		GameValues = Resources.Load<GameValuesScriptable>("GameValues");
	}
}
