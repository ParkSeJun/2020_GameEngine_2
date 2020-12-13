using UnityEngine;

public class PoolableObject : MonoBehaviour
{
	bool isAlive;
	public bool IsAlive => isAlive;

	ObjectPool<PoolableObject> myPool;

	public void Spawn(ObjectPool<PoolableObject> pool)
	{
		myPool = pool;

		isAlive = true;
	}

	public void Release()
	{
		isAlive = false;
		gameObject.SetActive(false);
	}
}
