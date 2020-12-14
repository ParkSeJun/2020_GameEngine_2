using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : Singletone<PoolingManager>
{
	[SerializeField] GameObject bulletLine;
	ObjectPool<BulletLine> bulletLinePool;
	Transform bulletLineParent;

	[SerializeField] GameObject gunShotEffect;
	ObjectPool<GunShotEffect> gunShotEffectPool;
	Transform gunShotEffectParent;

	//[SerializeField] GameObject[] monsters;
	//ObjectPool<BulletLine> bulletLinePool;
	//Transform bulletLineParent;


	// Cached Variables
	Transform cachedTransform;

	private void Awake()
	{
		cachedTransform = transform;

		bulletLinePool = new ObjectPool<BulletLine>(bulletLine);
		bulletLineParent = new GameObject(nameof(BulletLine)).transform;
		bulletLineParent.SetParent(cachedTransform);

		gunShotEffectPool = new ObjectPool<GunShotEffect>(gunShotEffect);
		gunShotEffectParent = new GameObject(nameof(GunShotEffect)).transform;
		gunShotEffectParent.SetParent(cachedTransform);
	}

	public BulletLine SpawnBulletLine()
	{
		var bulletLine = bulletLinePool.Spawn();
		bulletLine.transform.SetParent(bulletLineParent);
		return bulletLine;
	}

	public GunShotEffect SpawnGunShotEffect(Transform gunTip)
	{
		var gunShotEffect = gunShotEffectPool.Spawn();
		gunShotEffect.ApplyGunTip(gunTip);
		return gunShotEffect;
	}

}
