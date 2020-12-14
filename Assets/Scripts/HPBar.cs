using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : PoolableObject
{
	[SerializeField] Slider slider;
	Transform hpPos;

	private void Update()
	{
		var canvasPos = Camera.main.WorldToScreenPoint(hpPos.position);

		float distance = Vector3.Distance(hpPos.position, GameManager.Instance.PlayerTransform.position);
		cachedTransform.localScale = Vector3.one * 3f / distance;

		cachedTransform.position = canvasPos;
	}

	public void SetHpTransform(Transform hpPosTransform)
	{
		hpPos = hpPosTransform;
	}

	public void SetHp(float rate)
	{
		slider.value = rate;
	}
}
