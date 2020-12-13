using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
	private void Awake()
	{
		Application.targetFrameRate = 60;
	}
}
