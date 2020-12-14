using Unity.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] Transform gunTip;
	[SerializeField] GunData[] gunDatas;

	// Properties
	[ReadOnly] int curGunIndex;
	float nextCooldown; // 다음 격발 가능 시각

	// Cached Variables
	Renderer cachedRenderer;
	Animator cachedAnimator;

	// Const
	const float rayLimit = 100f;

	private void Awake()
	{
		curGunIndex = 0;
		nextCooldown = 0f;
		cachedRenderer = GetComponent<MeshRenderer>();
		cachedAnimator = GetComponent<Animator>();
	}

	private void Start()
	{
		SetGunData(curGunIndex);
	}

	void Update()
	{
		int newGunIndex = -1;
		if (Input.GetKeyDown(KeyCode.Alpha1))
			newGunIndex = 0;
		if (Input.GetKeyDown(KeyCode.Alpha2))
			newGunIndex = 1;
		if (Input.GetKeyDown(KeyCode.Alpha3))
			newGunIndex = 2;

		if (newGunIndex != -1 && newGunIndex != curGunIndex)
			SetGunData(newGunIndex);
	}

	public void SetZoomState(bool isZoom)
	{
		cachedAnimator.SetBool("isZoom", isZoom);
	}

	public void SetMovingState(bool isMoving)
	{
		cachedAnimator.SetBool("isMoving", isMoving);
	}

	public void SetGunData(int gunIndex)
	{
		curGunIndex = gunIndex;

		cachedRenderer.sharedMaterial.SetTexture("_MainTex", GetCurrentGunData().Texture);
	}

	public void Fire(Vector3 camPos, Vector3 direction)
	{
		// 쿨다운 적용
		nextCooldown = Time.realtimeSinceStartup + GetCurrentGunData().Cooldown;

		// 쏘는 애니메이션
		cachedAnimator.SetTrigger("fireEvent");

		RaycastHit result;
		bool isHit = Physics.Raycast(new Ray(camPos, direction), out result, rayLimit, Constants.Layer.Mask.MAP | Constants.Layer.Mask.ENEMY);
		Vector3 start = GetGunTipPosition();
		Vector3 end = isHit ? result.point : camPos + direction * rayLimit;

		// 궤적 생성
		var bulletLine = PoolingManager.Instance.SpawnBulletLine();
		bulletLine.SetLine(start, end, GetCurrentGunData().BulletSize, GetCurrentGunData().BulletColor);

		// 발사 폭발 이펙트
		PoolingManager.Instance.SpawnGunShotEffect(GetGunTipTransform());

		// 무언가를 맞힘
		if (isHit)
		{
			// 몬스터를 맞힘
			if (result.collider.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.ENEMY))
			{
				// 몬스터에게 대미지 주기
				result.collider.GetComponentInParent<Monster>().Damage(GetCurrentGunData().Damage);
			}
			// 벽을 맞힘
			else if (result.collider.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.MAP))
			{

			}
		}


	}

	public bool CanFire() => nextCooldown < Time.realtimeSinceStartup;

	public Vector3 GetGunTipPosition() => gunTip.position;

	public Transform GetGunTipTransform() => gunTip;

	public GunData GetCurrentGunData() => gunDatas[curGunIndex];

}
