using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : Singletone<PoolingManager>
{
	[SerializeField] GameObject bulletLine;
	ObjectPool<BulletLine> bulletLinePool;
	Transform bulletLineParent;

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
	}

	public BulletLine SpawnBulletLine()
	{
		var bulletLine = bulletLinePool.Spawn();
		bulletLine.transform.SetParent(bulletLineParent);
		return bulletLine;
	}

}
