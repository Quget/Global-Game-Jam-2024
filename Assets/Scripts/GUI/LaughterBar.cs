using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaughterBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
	private GameValueService _gameValueService;
	public void SetUpSlider()
    {
		_gameValueService = Services.Instance.GetService<GameValueService>();

		slider.minValue = 0;
        slider.maxValue = _gameValueService.GameValues.MaxLaughter;
    }

	/// <summary>
	/// Between 0 and gameValues.MaxLaughter;
	/// </summary>
	/// <param name="value"></param>
	public void SetLaughter(float value)
    {
		slider.value = value;
	}

	public void AddLaughter(float value)
	{
		float newValue = slider.value + value;
		if(newValue > slider.maxValue)
		{
			slider.value = slider.maxValue;
			return;
		}

		if (newValue < 0)
		{
			slider.value = 0;
			return;
		}

		slider.value = newValue;
	}

	public float GetLaughter()
	{
		return slider.value;
	}
}
