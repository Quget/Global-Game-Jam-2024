using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaughterBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void SetUpSlider(GameValues gameValues)
    {

        slider.minValue = 0;
        slider.maxValue = gameValues.MaxLaughter;
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
