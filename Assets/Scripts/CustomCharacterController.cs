using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
	[SerializeField] float speed = 3f;

	Transform transform;
	CharacterController controller;

	void Awake()
	{
		transform = GetComponent<Transform>();
		controller = GetComponent<CharacterController>();
	}

	void FixedUpdate()
	{

		Vector3 inputVec = GetKeyInputVector();
		if (inputVec.magnitude > 0f)
		{
			controller.Move(GetMoveVector() * speed * Time.deltaTime);
		}

		controller.Move(Vector3.down * Time.deltaTime);

		Debug.Log(controller.velocity.y);
	}

	/// <summary>
	/// 키입력 WASD 벡터 반환
	/// </summary>
	Vector3 GetKeyInputVector() => new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

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
	Vector3 GetMouseInputVector() => new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0f);

}
