using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController Instance { get; private set; }

	private AssignmentService _assignmentService;
	private GameValueService _gameValueService;

	private LaughterBar laughterBar = null;

	private float laughter = 0;
	public float Laughter
	{
		get
		{
			return laughter;
		}
		set
		{
			laughter = value;
			laughterBar.SetLaughter(value);
		}
	}

	private float tickTimer = 0;

	private void Awake()
    {
		if (Instance != null && GameController.Instance != this)
		{
			Destroy(Instance.gameObject);
			return;
		}
		Instance = this;

		_assignmentService = Services.Instance.GetService<AssignmentService>();
		_gameValueService = Services.Instance.GetService<GameValueService>();

		laughterBar = FindObjectOfType<LaughterBar>();
	}

	private void Start()
	{
		Laughter = _gameValueService.GameValues.LaughterStart;
		var currentAssignment = _assignmentService.GetRandomAssignment();
		_assignmentService.SetAssignment(currentAssignment);
	}

	private void Update()
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			SceneManager.LoadScene(0);
		}

		SecondTick();
	}

	private void SecondTick()
	{
		tickTimer += Time.deltaTime;
		if (tickTimer >= 1)
		{
			Laughter -= _gameValueService.GameValues.LaughterDecreaseRatePerSecond;
			tickTimer = 0;
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
			Instance = null;
	}
}