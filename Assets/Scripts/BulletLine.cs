using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLine : PoolableObject
{
	// Cached Variables
	LineRenderer lineRenderer;
	CoroutineHandle effectHandle;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	public void SetLine(Vector3 start, Vector3 end)
	{
		lineRenderer.SetPositions(new Vector3[] { start, end });
		if (effectHandle.IsValid)
			Timing.KillCoroutines(effectHandle);
		effectHandle = Timing.RunCoroutine(YieldEffect());
	}

	IEnumerator<float> YieldEffect()
	{
		float time = 0f;
		const float destTime = 0.5f;
		while (true)
		{
			time += Time.deltaTime;
			float rate = time / destTime;
			float t = Mathf.Lerp(0.05f, 0f, rate);
			lineRenderer.widthMultiplier = t;
			if (rate >= 1f)
				break;
			yield return Timing.WaitForOneFrame;
		}
		lineRenderer.widthMultiplier = 0f;
		Release();
	}
}
