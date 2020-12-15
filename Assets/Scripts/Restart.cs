using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
	void Start()
	{
		System.GC.Collect();
		string sceneName = PlayerPrefs.GetString("Restart");
		SceneManager.LoadScene(sceneName);
	}
}
