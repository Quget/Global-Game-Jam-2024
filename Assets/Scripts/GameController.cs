using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameValues gameValues;

    private LaughterBar laughterBar = null;

    private void Awake()
    {
		laughterBar = FindObjectOfType<LaughterBar>();
        laughterBar.SetUpSlider(gameValues);

		CreateServices();
	}

	private void CreateServices()
	{
		Services.Instance.AddService(new GameValueService(gameValues));
		Services.Instance.AddService(new AssignmentService(Services.Instance.GetService<GameValueService>()));
	}

	private void Start()
	{
		laughterBar.UpdateLaughter(40);
		Debug.Log(Services.Instance.GetService<AssignmentService>().GetRandomAssignment().ResultItemTag);
	}
}