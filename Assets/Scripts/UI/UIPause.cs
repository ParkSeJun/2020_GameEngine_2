using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
	public void OnClickedContinue()
	{
		UIManager.Instance.ShowPause(false);
	}

	public void OnClickedMain()
	{
		// TODO: 씬이동 : 메인으로
		SceneManager.LoadScene("Main");
	}
}
