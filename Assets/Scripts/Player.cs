using Cinemachine;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] Transform cameraTransform;
	[SerializeField] CinemachineVirtualCamera camDefault;
	[SerializeField] CinemachineVirtualCamera camZoom;
	[SerializeField] GameObject playerHpPrefab;

	// Properties
	[SerializeField] float speed;
	float jumpPower;
	float velocity_y;
	[SerializeField] bool isGround;
	[SerializeField] bool isZoom;
	[ReadOnly] float nextDmgTime; // 대미지를 입을 수 있는 시각
	[ReadOnly] int hp;

	// Cached Variables
	Transform cachedTransform;
	CharacterController controller;
	Gun _myGun;
	Gun myGun => _myGun ?? (_myGun = GetComponentInChildren<Gun>());
	PlayerHP uiHp;

	void Awake()
	{
		cachedTransform = GetComponent<Transform>();
		controller = GetComponent<CharacterController>();
	}

	void Start()
	{
		velocity_y = 0f;
		isGround = false;
		isZoom = false;
		nextDmgTime = 0f;
		hp = 5;

		if (!uiHp)
			uiHp = GameObject.Instantiate(playerHpPrefab, GameManager.Instance.Canvas).GetComponent<PlayerHP>();
		uiHp.SetHp(hp);

		speed = Constants.DefaultStatus.Player.Move_Speed;
		jumpPower = Constants.DefaultStatus.Player.Jump_Power;
	}

	void Update()
	{
		isGround = IsOnGround();
		isZoom = Input.GetMouseButton(1);

		// 카메라 회전
		UpdateCamera();

		// 점프 (Space)
		ProcessJump();

		// 중력
		ProcessGravity();

		// 플레이어 이동
		ProcessMove();

		// 격발 (좌클릭)
		ProcessFire();

		// 조준 모드 (우클릭)
		ProcessZoom();

	}

	void ProcessZoom()
	{
		// 줌 모드 실행
		if (Input.GetMouseButtonDown(1))
		{
			// 카메라 변경
			camDefault.enabled = false;
			camZoom.enabled = true;

			// Gun 줌 애니메이션 실행
			myGun.SetZoomState(true);
		}

		if (Input.GetMouseButtonUp(1))
		{
			// 카메라 변경
			camDefault.enabled = true;
			camZoom.enabled = false;

			// Gun Idle 애니메이션 실행
			myGun.SetZoomState(false);
		}
	}

	void ProcessFire()
	{
		if (!Input.GetMouseButtonDown(0))
			return;

		if (myGun.CanFire())
			myGun.Fire(cameraTransform.position, cameraTransform.forward);
	}

	void ProcessMove()
	{
		Vector3 inputVec = GetKeyInputVector();
		if (inputVec.magnitude > 0f)
		{
			var moveVec = GetMoveVector();

			moveVec *= speed;

			if (!isGround)
				moveVec *= Constants.Physics.Speed_Multiplier_On_Air;

			if (isZoom)
				moveVec *= Constants.DefaultStatus.Player.Zoom_Move_Speed_Multiplier;

			controller.Move(moveVec * Time.deltaTime);

			myGun.SetMovingState(true);
		}
		else
		{
			myGun.SetMovingState(false);
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


	public bool Damage(int damage)
	{
		if (Time.realtimeSinceStartup < nextDmgTime)
			return false;

		nextDmgTime = Time.realtimeSinceStartup + 1.5f;
		hp -= damage;

		// HP UI 변동
		uiHp.SetHp(hp);

		return true;
	}

	public void Heal(int heal)
	{
		hp += heal;

		// HP UI 변동
		uiHp.SetHp(hp);
	}


	bool IsOnGround()
	{
		RaycastHit rayResult;
		bool isHit = Physics.Raycast(new Ray(cachedTransform.position, Vector3.down), out rayResult, Constants.Physics.Ground_Check_Distance, Constants.Layer.Mask.MAP);
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
