using UnityEngine;

public class PoolableObject : MonoBehaviour
{
	bool isAlive;
	public bool IsAlive => isAlive;

	protected Transform cachedTransform;

	ObjectPool<PoolableObject> myPool;

	virtual public void Awake()
	{
		cachedTransform = transform;
	}

	public void Spawn(ObjectPool<PoolableObject> pool)
	{
		myPool = pool;

		isAlive = true;

		OnSpawn();
	}

	public virtual void OnSpawn() { }

	public virtual void Release()
	{
		isAlive = false;
		gameObject.SetActive(false);
	}
}
