using UnityEngine;

public class InputManager : Singletone<InputManager>
{
	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	/// <summary>
	/// 마우스 입력 벡터 반환
	/// </summary>
	public Vector2 GetMouseInputVector()
	{
		return new Vector2(Input.GetAxisRaw(Constants.Input.AxisName_Mouse_X), Input.GetAxisRaw(Constants.Input.AxisName_Mouse_Y));
	}
}
