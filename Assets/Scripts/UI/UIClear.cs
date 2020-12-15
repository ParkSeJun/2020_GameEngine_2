using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIClear : MonoBehaviour
{
	bool isTriggered;

	private void Start()
	{
		isTriggered = false;
	}

	void Update()
	{
		if (isTriggered)
			return;

		if (Input.GetMouseButtonDown(0))
		{
			isTriggered = true;
			SceneManager.LoadScene("Intro");
		}
	}
}
