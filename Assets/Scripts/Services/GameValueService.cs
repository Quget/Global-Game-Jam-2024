using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValueService : IServiceAble
{
	public GameValues GameValues { get; private set; }

	public GameValueService(GameValues gameValues)
	{
		GameValues = gameValues;
	}
}
