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
			if (laughter <= 0)
			{
				GameOver();
			}
			else if ((laughter >= _gameValueService.GameValues.MaxLaughter))
			{
				Victory();
			}
		}
	}

	private float tickTimer = 0;
	private float newAssignmentTimer = 0;

	[SerializeField]
	private GameObject gameOverObject;

	[SerializeField]
	private GameObject victoryObject;

	[SerializeField]
	private AudioClip victorySound;

	[SerializeField]
	private AudioClip gameOverSound;

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
		_assignmentService.NewRandomAssignment();
	}

	private void Update()
	{

		if (gameOverObject.activeInHierarchy || victoryObject.activeInHierarchy)
		{
			if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Delete))
			{
				SceneManager.LoadScene(0);
			}
		}
		else
		{
			SecondTick();
		}
	}

	private void GameOver()
	{
		gameOverObject.gameObject.SetActive(true);
		AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position, 1);
		Clear();
	}

	private void Victory()
	{
		victoryObject.gameObject.SetActive(true);
		AudioSource.PlayClipAtPoint(victorySound, Camera.main.transform.position, 1);
		Clear();
	}

	private void Clear()
	{
		Destroy(FindObjectOfType<SpawnItem>()?.gameObject);
		Destroy(FindObjectOfType<charactercontroller>());

		var bodies = FindObjectsOfType<Rigidbody2D>();
        foreach (var  body in bodies)
        {
			body.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * 1000);
        }
    }
	private void SecondTick()
	{
		tickTimer += Time.deltaTime;
		if (tickTimer >= 1)
		{
			Laughter -= _gameValueService.GameValues.LaughterDecreaseRatePerSecond;

			NewAssignmentUpdateTick();

			tickTimer = 0;
		}
	}

	private void NewAssignmentUpdateTick()
	{
		newAssignmentTimer -= 1;
		if (newAssignmentTimer <= 0)
		{
			newAssignmentTimer = Random.Range(_gameValueService.GameValues.NewAssignmentAfterSecondsMin, _gameValueService.GameValues.NewAssignmentAfterSecondsMax);
			_assignmentService.NewRandomAssignment();
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
			Instance = null;
	}
}