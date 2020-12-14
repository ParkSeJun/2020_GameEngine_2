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

	[SerializeField] GameObject hpBar;
	ObjectPool<HPBar> hpBarPool;
	Transform hpBarParent;

	[SerializeField] GameObject[] monsters;
	ObjectPool<Monster>[] monsterPools;
	Transform[] monsterParents;


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

		hpBarPool = new ObjectPool<HPBar>(hpBar);
		hpBarParent = GameObject.FindGameObjectWithTag("Canvas").transform;

		int monstersCount = monsters.Length;
		monsterPools = new ObjectPool<Monster>[monstersCount];
		monsterParents = new Transform[monstersCount];
		for (int i = 0; i < monstersCount; i++)
		{
			monsterPools[i] = new ObjectPool<Monster>(monsters[i]);
			monsterParents[i] = new GameObject(nameof(Monster)).transform;
			monsterParents[i].SetParent(cachedTransform);
		}

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

	public HPBar SpawnHPBar(Transform hpPos)
	{
		var hpBar = hpBarPool.Spawn();
		hpBar.transform.SetParent(hpBarParent);
		hpBar.SetHpTransform(hpPos);
		return hpBar;
	}

	public Monster SpawnMonster(int type)
	{
		var monster = monsterPools[type].Spawn();
		monster.transform.SetParent(monsterParents[type]);
		return monster;
	}
}
