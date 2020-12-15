using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitle : MonoBehaviour
{
	[SerializeField] GameObject uiTutorial;

	public void OnClickedGameStart()
	{
		uiTutorial.SetActive(true);
		gameObject.SetActive(false);
	}

	public void OnClickedExit()
	{
		Debug.Log("EXIT");
		Application.Quit();
	}
}
