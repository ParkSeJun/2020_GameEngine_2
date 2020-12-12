using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
	[SerializeField] float speed = 3f;

	// Properties
	[SerializeField] float gravityAccSpeed;
	[SerializeField] bool isGround;

	// Cached Variables
	Transform transform;
	CharacterController controller;

	void Awake()
	{
		transform = GetComponent<Transform>();
		controller = GetComponent<CharacterController>();
	}

	void Start()
	{
		gravityAccSpeed = 0f;
	}

	void FixedUpdate()
	{
		Vector3 inputVec = GetKeyInputVector();
		if (inputVec.magnitude > 0f)
		{
			controller.Move(GetMoveVector() * speed * Time.deltaTime);
		}

		isGround = IsOnGround();

		if (!isGround)
			gravityAccSpeed += Constants.Physics.GravityAcceleration * Time.deltaTime;
		else
			gravityAccSpeed = 0f;

		controller.Move(Vector3.down * gravityAccSpeed);
	}

	bool IsOnGround()
	{
		RaycastHit rayResult;
		bool isHit = Physics.Raycast(new Ray(transform.position, Vector3.down), out rayResult, Constants.Physics.GroundCheckDistance, ~Constants.Layer.MAP);
		Debug.DrawLine(transform.position, transform.position + Vector3.down * Constants.Physics.GroundCheckDistance, isHit ? Color.red : Color.blue);
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

	/// <summary>
	/// 마우스 입력 벡터 반환
	/// </summary>
	Vector3 GetMouseInputVector() => new Vector3(Input.GetAxisRaw(Constants.Input.AxisName_Mouse_X), Input.GetAxisRaw(Constants.Input.AxisName_Mouse_Y), 0f);

}
