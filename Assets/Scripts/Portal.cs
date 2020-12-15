using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
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

		float distance = Vector3.Distance(transform.position, GameManager.Instance.PlayerTransform.position);
		if (distance > 1f)
			return;

		isTriggered = true;

		var nextScene = GameManager.Instance.GetNextSceneName();
		if (string.IsNullOrEmpty(nextScene))
		{
			// TODO: 게임 클리어 화면 띄우기
		}
		else
		{
			SceneManager.LoadScene(nextScene);
		}
	}


}
