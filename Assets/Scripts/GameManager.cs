using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
	[SerializeField] StageData stageData;

	Transform playerTransform;
	public Transform PlayerTransform
	{
		get
		{
			if (!playerTransform)
				playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
			return playerTransform;
		}
	}

	Player playerEntity;
	public Player PlayerEntity
	{
		get
		{
			if (!playerEntity)
				playerEntity = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
			return playerEntity;
		}
	}

	Transform canvas;
	public Transform Canvas
	{
		get
		{
			if (!canvas)
				canvas = GameObject.FindGameObjectWithTag("Canvas")?.transform;
			return canvas;
		}
	}

	private void Awake()
	{
		Application.targetFrameRate = 60;
	}

	private void Start()
	{
		for (int i = 0; i < stageData.Monsters.Length; i++)
		{
			for (int j = 0; j < stageData.Monsters[i].count; j++)
			{
				var monster = PoolingManager.Instance.SpawnMonster(Random.Range(0, PoolingManager.Instance.MonsterTypeCount - 1));
				monster.transform.position = new Vector3(Random.Range(stageData.Monsters[i].region.xMin, stageData.Monsters[i].region.xMax),
														stageData.Monsters[i].y,
														Random.Range(stageData.Monsters[i].region.yMin, stageData.Monsters[i].region.yMax));
				monster.SetRegion(stageData.Monsters[i].region);
			}
		}
	}


#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (stageData)
		{
			for (int i = 0; i < stageData.Monsters.Length; i++)
			{
				Helper.DrawRect(stageData.Monsters[i].region, stageData.Monsters[i].y, Color.red);
			}
		}
	}
#endif
}
