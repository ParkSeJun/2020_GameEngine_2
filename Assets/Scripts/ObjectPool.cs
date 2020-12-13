using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T>
	where T : PoolableObject
{
	List<T> poolList;
	GameObject prefab;

	public ObjectPool(GameObject prefab)
	{
		this.prefab = prefab;
	}

	public T Spawn()
	{
		var stock = poolList.First(e => !e.IsAlive);
		if (!stock)
		{
			stock = GameObject.Instantiate(prefab).GetComponent<T>();
			poolList.Add(stock);
		}
		stock.gameObject.SetActive(true);
		stock.Spawn(this as ObjectPool<PoolableObject>);
		return stock;
	}
}
