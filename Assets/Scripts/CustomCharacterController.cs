using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
	// Properties
	[SerializeField] float speed;
	float jumpPower;
	float velocity_y;
	[SerializeField] bool isGround;

	// Cached Variables
	Transform transform;
	CharacterController controller;

	void Awake()
	{
		Application.targetFrameRate = 60;
		transform = GetComponent<Transform>();
		controller = GetComponent<CharacterController>();
	}

	void Start()
	{
		velocity_y = 0f;
		isGround = false;
		speed = Constants.DefaultStat.Player_Move_Speed;
		jumpPower = Constants.DefaultStat.Player_Jump_Power;
	}

	void Update()
	{
		// 카메라 회전
		transform.Rotate(0f, InputManager.Instance.GetMouseInputVector().x * Constants.GameEnv.Camera_X_Speed, 0f);

		// 점프
		if (Input.GetKey(KeyCode.Space) && IsOnGround())
			velocity_y = -jumpPower;

		// 중력
		isGround = IsOnGround();

		if (!isGround)
		{
			velocity_y += Constants.Physics.Gravity_Acceleration * Time.deltaTime;
			if (velocity_y > Constants.Physics.Max_Gravity_Acceleration)
				velocity_y = Constants.Physics.Max_Gravity_Acceleration;
		}
		else
			velocity_y = Mathf.Clamp(velocity_y, velocity_y, 0f);

		controller.Move(Vector3.down * velocity_y);

		// 플레이어 이동
		Vector3 inputVec = GetKeyInputVector();
		if (inputVec.magnitude > 0f)
		{
			if (isGround)
				controller.Move(GetMoveVector() * Constants.DefaultStat.Player_Move_Speed * Time.deltaTime);
			else
				controller.Move(GetMoveVector() * Constants.DefaultStat.Player_Move_Speed * Constants.Physics.Speed_Multiplier_On_Air * Time.deltaTime);
		}
	}

	bool IsOnGround()
	{
		RaycastHit rayResult;
		bool isHit = Physics.Raycast(new Ray(transform.position, Vector3.down), out rayResult, Constants.Physics.Ground_Check_Distance, ~Constants.Layer.MAP);
		Debug.DrawLine(transform.position, transform.position + Vector3.down * Constants.Physics.Ground_Check_Distance, isHit ? Color.red : Color.blue);
		return isHit;
	}

	/// <summary>
	/// 키입력 WASD 벡터 반환
	/// </summary>
	Vector3 GetKeyInputVector() => new Vector3(Input.GetAxisRaw(Constants.Input.AxisName_Move_Horizontal), 0f, Input.GetAxisRaw(Constants.Input.AxisName_Move_Vertical)).normalized;

	/// <summary>
	/// 키입력에 의한 이동 벡터 반환
	/// </summary>
	Vector3 GetMoveVector()
	{
		Vector3 inputVec = GetKeyInputVector();
		float angle = Mathf.Deg2Rad * transform.localEulerAngles.y;

		Vector3 outVec = Vector3.zero;
		outVec.x = Mathf.Cos(angle) * inputVec.x + Mathf.Sin(angle) * inputVec.z;
		outVec.z = -Mathf.Sin(angle) * inputVec.x + Mathf.Cos(angle) * inputVec.z;

		return outVec;
	}


}
