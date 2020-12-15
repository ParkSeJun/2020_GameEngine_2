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
		if (!IsInCamera(hpPos.position) || UIManager.Instance.IsShowingUI)
		{
			cachedTransform.localScale = Vector3.zero;
			return;
		}

		var canvasPos = Camera.main.WorldToScreenPoint(hpPos.position);
		float distance = Vector3.Distance(hpPos.position, GameManager.Instance.PlayerTransform.position);
		cachedTransform.localScale = Vector3.one * 3f / distance;
		cachedTransform.position = canvasPos;
	}

	bool IsInCamera(Vector3 pos)
	{
		var viewport = Camera.main.WorldToViewportPoint(pos);
		return viewport.x >= 0f && viewport.y >= 0f && viewport.z >= 0f;
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
