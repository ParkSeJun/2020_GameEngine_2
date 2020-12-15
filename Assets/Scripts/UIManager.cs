using UnityEngine;

public class UIManager : Singletone<UIManager>
{
	[SerializeField] GameObject aim;
	[SerializeField] GameObject uiPause;
	[SerializeField] GameObject uiLoading;
	[SerializeField] GameObject uiClear;
	[SerializeField] GameObject uiGameOver;

	public bool IsShowingUI => uiPause && uiPause.activeInHierarchy || uiLoading && uiLoading.activeInHierarchy || uiClear && uiClear.activeInHierarchy || uiGameOver && uiGameOver.activeInHierarchy;

	public void ShowInGameUI(bool isShow)
	{
		Debug.Log(aim);
		aim.SetActive(isShow);
		GameObject.FindObjectOfType<PlayerHP>(true)?.gameObject.SetActive(isShow);
		Cursor.lockState = !isShow ? CursorLockMode.None : CursorLockMode.Locked;
		Cursor.visible = !isShow;
	}

	public void ShowPause(bool isShow)
	{
		ShowInGameUI(!isShow);
		uiPause.SetActive(isShow);
	}

	public void ShowLoading(bool isShow)
	{
		ShowInGameUI(!isShow);
		uiLoading.SetActive(isShow);
	}

	public void ShowClear(bool isShow)
	{
		ShowInGameUI(!isShow);
		uiClear.SetActive(isShow);
	}

	public void ShowGameOver(bool isShow)
	{
		ShowInGameUI(!isShow);
		uiGameOver.SetActive(isShow);
	}
}
