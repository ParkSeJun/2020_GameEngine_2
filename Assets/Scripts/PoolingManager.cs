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

	public int MonsterTypeCount => monsters.Length;

	// Cached Variables
	Transform cachedTransform;

	private void Start()
	{
		Debug.Log("PM AWAKE");
		cachedTransform = transform;

		bulletLinePool = new ObjectPool<BulletLine>();
		bulletLinePool.Init(bulletLine);
		bulletLineParent = new GameObject(nameof(BulletLine)).transform;
		bulletLineParent.SetParent(cachedTransform);

		gunShotEffectPool = new ObjectPool<GunShotEffect>();
		gunShotEffectPool.Init(gunShotEffect);
		gunShotEffectParent = new GameObject(nameof(GunShotEffect)).transform;
		gunShotEffectParent.SetParent(cachedTransform);

		hpBarPool = new ObjectPool<HPBar>();
		hpBarPool.Init(hpBar);
		hpBarParent = GameObject.FindGameObjectWithTag("Canvas").transform;

		int monstersCount = monsters.Length;
		monsterPools = new ObjectPool<Monster>[monstersCount];
		monsterParents = new Transform[monstersCount];
		for (int i = 0; i < monstersCount; i++)
		{
			monsterPools[i] = new ObjectPool<Monster>();
			monsterPools[i].Init(monsters[i]);
			monsterParents[i] = new GameObject(nameof(Monster)).transform;
			monsterParents[i].SetParent(cachedTransform);
		}

	}

	public BulletLine SpawnBulletLine()
	{
		var bulletLine = bulletLinePool.Spawn(this.bulletLine);
		bulletLine.transform.SetParent(bulletLineParent);
		return bulletLine;
	}

	public GunShotEffect SpawnGunShotEffect(Transform gunTip)
	{
		var gunShotEffect = gunShotEffectPool.Spawn(this.gunShotEffect);
		gunShotEffect.ApplyGunTip(gunTip);
		return gunShotEffect;
	}

	public HPBar SpawnHPBar(Transform hpPos)
	{
		var hpBar = hpBarPool.Spawn(this.hpBar);
		hpBar.transform.SetParent(hpBarParent);
		hpBar.SetHpTransform(hpPos);
		return hpBar;
	}

	public Monster SpawnMonster(int type)
	{
		var monster = monsterPools[type].Spawn(this.monsters[type]);
		monster.transform.SetParent(monsterParents[type]);
		return monster;
	}

	public void OnDestroy()
	{
		bulletLinePool.Destory();
		gunShotEffectPool.Destory();
		hpBarPool.Destory();
		int monstersCount = monsters.Length;
		for (int i = 0; i < monstersCount; i++)
			monsterPools[i].Destory();
	}
}
