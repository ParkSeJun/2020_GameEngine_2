using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : Singletone<PoolingManager>
{
	[SerializeField] GameObject bulletLine;
	ObjectPool<BulletLine> bulletLinePool;

	private void Awake()
	{
		bulletLinePool = new ObjectPool<BulletLine>(bulletLine);
	}

	public BulletLine SpawnBulletLine()
	{
		return bulletLinePool.Spawn();
	}

}
