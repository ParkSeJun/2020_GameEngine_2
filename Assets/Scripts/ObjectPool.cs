using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T>
	where T : PoolableObject
{
	List<T> poolList = new List<T>();
	GameObject prefab;

	public void Init(GameObject prefab)
	{
		Debug.Log("INIT" + prefab);
		this.prefab = prefab;
	}

	public T Spawn(GameObject prefab)
	{
		var stock = poolList.FirstOrDefault(e => e != null && !e.IsAlive);
		if (!stock)
		{
			GameObject obj = GameObject.Instantiate(prefab);
			stock = obj?.GetComponent<T>();
			poolList.Add(stock);
		}
		stock.gameObject.SetActive(true);
		stock.Spawn(this as ObjectPool<PoolableObject>);
		return stock;
	}

	public void Destory()
	{
		poolList.Clear();
		poolList = new List<T>();
	}
}
