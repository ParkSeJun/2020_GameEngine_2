using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
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

	private void Awake()
	{
		Application.targetFrameRate = 60;
	}
}
