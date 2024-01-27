using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private LaughterBar laughterBar = null;

    private void Awake()
    {
		laughterBar = FindObjectOfType<LaughterBar>();
        laughterBar.SetUpSlider();
	}

	private void Start()
	{
		laughterBar.UpdateLaughter(40);

		Debug.Log(Services.Instance.GetService<AssignmentService>().GetRandomAssignment().ResultItemTag);
	}

	private void Update()
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			SceneManager.LoadScene(0);
		}
	}
}