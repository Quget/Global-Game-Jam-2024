using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaughterBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
	private GameValueService _gameValueService;

	private void Awake()
	{
		
	}

	public void SetUpSlider()
    {
		_gameValueService = Services.Instance.GetService<GameValueService>();

		slider.minValue = 0;
        slider.maxValue = _gameValueService.GameValues.MaxLaughter;
    }

	/// <summary>
	/// Between 0 and gameValues.MaxLaughter;
	/// </summary>
	/// <param name="percentage"></param>
	public void UpdateLaughter(float percentage)
    {
		slider.value = percentage;
	}
}
