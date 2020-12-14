using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotEffect : PoolableObject
{
	// Cached Variables
	CoroutineHandle effectHandle;

	public void ApplyGunTip(Transform gunTip)
	{
		cachedTransform.SetParent(gunTip);
		cachedTransform.localPosition = Vector3.zero;

		if (effectHandle.IsValid)
			Timing.KillCoroutines(effectHandle);
		effectHandle = Timing.RunCoroutine(YieldDespawnTimer());
	}

	IEnumerator<float> YieldDespawnTimer()
	{
		yield return Timing.WaitForSeconds(0.5f);

		Release();
	}
}
