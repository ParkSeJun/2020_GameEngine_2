using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Transform cameraTransform;

	// Properties
	[SerializeField] float speed;
	float jumpPower;
	float velocity_y;
	[SerializeField] bool isGround;

	// Cached Variables
	Transform cachedTransform;
	CharacterController controller;
	Gun _myGun;
	Gun myGun => _myGun ?? (_myGun = GetComponentInChildren<Gun>());


	void Awake()
	{
		cachedTransform = GetComponent<Transform>();
		controller = GetComponent<CharacterController>();
	}

	void Start()
	{
		velocity_y = 0f;
		isGround = false;
		speed = Constants.DefaultStatus.Player_Move_Speed;
		jumpPower = Constants.DefaultStatus.Player_Jump_Power;
	}

	void Update()
	{
		isGround = IsOnGround();

		// 카메라 회전
		UpdateCamera();

		// 점프
		ProcessJump();

		// 중력
		ProcessGravity();

		// 플레이어 이동
		ProcessMove();

		// 격발
		ProcessFire();
	}

	void ProcessFire()
	{
		if (!Input.GetMouseButtonDown(0))
			return;

		RaycastHit result;
		bool isHit = Physics.Raycast(new Ray(cameraTransform.position, cameraTransform.forward), out result, 100f, Constants.Layer.MAP | Constants.Layer.ENEMY);

		Vector3 start = myGun.GetGunTipPosition();
		Vector3 end = isHit ? result.point : cameraTransform.position + cameraTransform.forward * 100f;

		var bulletLine = PoolingManager.Instance.SpawnBulletLine();
		bulletLine.SetLine(start, end);
	}

	void ProcessMove()
	{
		Vector3 inputVec = GetKeyInputVector();
		if (inputVec.magnitude > 0f)
		{
			if (isGround)
				controller.Move(GetMoveVector() * speed * Time.deltaTime);
			else
				controller.Move(GetMoveVector() * speed * Constants.Physics.Speed_Multiplier_On_Air * Time.deltaTime);
		}
	}

	void UpdateCamera()
	{
		cachedTransform.Rotate(0f, InputManager.Instance.GetMouseInputVector().x * Constants.GameEnv.Camera_X_Speed, 0f);
	}

	void ProcessJump()
	{
		if (Input.GetKey(KeyCode.Space) && IsOnGround())
			velocity_y = -jumpPower;
	}

	void ProcessGravity()
	{
		if (!IsOnGround())
		{
			velocity_y += Constants.Physics.Gravity_Acceleration * Time.deltaTime;
			if (velocity_y > Constants.Physics.Max_Gravity_Acceleration)
				velocity_y = Constants.Physics.Max_Gravity_Acceleration;
		}
		else
			velocity_y = Mathf.Clamp(velocity_y, velocity_y, 0f);

		controller.Move(Vector3.down * velocity_y);
	}

	bool IsOnGround()
	{
		RaycastHit rayResult;
		bool isHit = Physics.Raycast(new Ray(cachedTransform.position, Vector3.down), out rayResult, Constants.Physics.Ground_Check_Distance, Constants.Layer.MAP);
		Debug.DrawLine(cachedTransform.position, cachedTransform.position + Vector3.down * Constants.Physics.Ground_Check_Distance, isHit ? Color.red : Color.blue);
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
		float angle = Mathf.Deg2Rad * cachedTransform.localEulerAngles.y;

		Vector3 outVec = Vector3.zero;
		outVec.x = Mathf.Cos(angle) * inputVec.x + Mathf.Sin(angle) * inputVec.z;
		outVec.z = -Mathf.Sin(angle) * inputVec.x + Mathf.Cos(angle) * inputVec.z;

		return outVec;
	}


}
