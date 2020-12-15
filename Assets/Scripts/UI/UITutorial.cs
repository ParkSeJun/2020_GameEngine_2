using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITutorial : MonoBehaviour
{
	bool isTriggered;

	private void Start()
	{
		isTriggered = false;
	}

	private void Update()
	{
		if (isTriggered)
			return;


		if (Input.GetMouseButtonDown(0))
		{
			isTriggered = true;
			SceneManager.LoadScene("Main");
		}

	}
}
