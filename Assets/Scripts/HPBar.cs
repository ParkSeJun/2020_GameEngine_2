using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : PoolableObject
{
	[SerializeField] Slider slider;

	private void Update()
	{
		var canvasPos = Camera.main.WorldToScreenPoint(cachedTransform.position);
		cachedTransform.position = canvasPos;
	}

	public void SetHp(float rate)
	{
		slider.value = rate;
	}
}
