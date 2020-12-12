using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	float x;

	// Cached Variables
	Transform cachedTransform;

	void Awake()
	{
		cachedTransform = GetComponent<Transform>();
	}

	void Start()
	{
		x = 9f;
	}

	void Update()
	{
		var mouseAxis = InputManager.Instance.GetMouseInputVector();
		x -= mouseAxis.y * Constants.GameEnv.Camera_Y_Speed;
		x = ClampAngle(x, Constants.GameEnv.Camera_X_Limit_Min, Constants.GameEnv.Camera_X_Limit_Max);

		var newEulerAngle = cachedTransform.localEulerAngles;
		newEulerAngle.x = x;
		cachedTransform.localEulerAngles = newEulerAngle;
	}

	float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);

	}
}
