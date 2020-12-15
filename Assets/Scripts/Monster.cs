using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

public class Monster : PoolableObject
{
	[SerializeField] Rect region;
	[SerializeField] Transform hpHere;
	[ReadOnly] HPBar hpBar;

	public enum State
	{
		Idle = 0,
		Move,
		Trace,
		Attack,
		DIe,
	}

	State state;
	float endOfIdleTime; // Idle 대기 끝나는 시각
	float endOfMoveTime; // Move 끝나는 시각
	float endOfAttackTime; // Attack 끝나는 시각
	float endOfDieTime; // Die 끝나는 시각
	Vector3 destPos;

	float hp;
	float maxHp;

	CharacterController controller;
	Animator animator;

	public override void Awake()
	{
		base.Awake();
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	public override void OnSpawn()
	{
		maxHp = 10f;
		hp = maxHp;
		hpBar = PoolingManager.Instance.SpawnHPBar(hpHere);
		hpBar.SetHp(hp / maxHp);
		GetComponentInChildren<BoxCollider>().enabled = true;

		state = State.Idle;
		SetState(State.Idle);
	}

	private void Update()
	{
		if (state == State.Idle)
		{
			// 플레이어 발견 -> Trace
			if (IsNearPlayer() && CanSeePlayer())
			{
				SetState(State.Trace);
				return;
			}
			// 배회 (Move)
			if (endOfIdleTime < Time.realtimeSinceStartup)
			{
				SetState(State.Move);
				return;
			}
		}

		else if (state == State.Move)
		{
			// 플레이어 발견 -> Trace
			if (IsNearPlayer() && CanSeePlayer())
			{
				SetState(State.Trace);
				return;
			}
			if (IsOverMoveTime() || IsArriveToMoveDest())
			{
				SetState(State.Idle);
				return;
			}

			Debug.DrawLine(cachedTransform.position, destPos, Color.blue);
		}

		else if (state == State.Trace)
		{
			if (!IsNearPlayer() || !CanSeePlayer())
			{
				SetState(State.Idle);
				return;
			}

			destPos = GameManager.Instance.PlayerTransform.position;
			Debug.DrawLine(cachedTransform.position, destPos, Color.red);
		}

		else if (state == State.Attack)
		{
			if (IsOverAttackTime())
			{
				SetState(State.Idle);
				return;
			}
		}

		else if (state == State.DIe)
		{
			if (IsOverDieTime())
			{
				Release();
				return;
			}
		}

		if (state == State.Move || state == State.Trace)
		{
			var dir = destPos - cachedTransform.position;
			dir.y = 0f;
			dir = dir.normalized;

			cachedTransform.LookAt(cachedTransform.position + dir);
			controller.Move(dir * 1f * Time.deltaTime);
		}

		if (state != State.DIe && IsClosePlayer())
		{
			bool isHit = GameManager.Instance.PlayerEntity.Damage(1);
			if (isHit)
				SetState(State.Attack);
		}
	}

	void SetState(State state)
	{
		switch (state)
		{
			case State.Idle:
				endOfIdleTime = Time.realtimeSinceStartup + 1f;
				animator.CrossFade("Idle", 0.3f, 0); //("Idle");
				break;

			case State.Move:
				destPos = new Vector3(Random.Range(region.xMin, region.xMax), cachedTransform.position.y, Random.Range(region.yMin, region.yMax));
				endOfMoveTime = Time.realtimeSinceStartup + 3f;
				animator.CrossFade("Move", 0.3f, 0);
				break;

			case State.Trace:
				animator.CrossFade("Move", 0.3f, 0);
				break;

			case State.Attack:
				animator.Play("Attack", 0);
				endOfAttackTime = Time.realtimeSinceStartup + 1.5f;
				break;

			case State.DIe:
				animator.Play("Die", 0);
				endOfDieTime = Time.realtimeSinceStartup + 1.5f;
				if (hpBar)
					hpBar.gameObject.SetActive(false);
				GetComponentInChildren<BoxCollider>().enabled = false;
				break;
		}

		this.state = state;
	}

	public async void Damage(float damage)
	{
		if (hp < 0f)
			return;

		hp -= damage;

		// HP바 변동
		hpBar.SetHp(hp / maxHp);

		if (hp < 0f)
		{
			SetState(State.DIe);
		}
		else
		{
			animator.CrossFade("Damage", 0.3f, 0);
			await Task.Delay(833);
			if (state != State.DIe)
				animator.CrossFade("Move", 0.3f, 0);
		}
	}

	public void SetRegion(Rect region)
	{
		this.region = region;
	}

	public void SetStatusMultiplier(float multiplier)
	{
		maxHp = 10f * multiplier;
		hp = maxHp;
		hpBar.SetHp(hp / maxHp);
	}

	bool IsOverDieTime() => endOfDieTime < Time.realtimeSinceStartup;
	bool IsOverAttackTime() => endOfAttackTime < Time.realtimeSinceStartup;
	bool IsOverMoveTime() => endOfMoveTime < Time.realtimeSinceStartup;
	bool IsArriveToMoveDest() => Vector3.Distance(cachedTransform.position, new Vector3(destPos.x, cachedTransform.position.y, destPos.z)) < 0.1f;
	bool IsNearPlayer() => Vector3.Distance(GameManager.Instance.PlayerTransform.position, cachedTransform.position) < 8f;
	bool IsClosePlayer() => Vector3.Distance(GameManager.Instance.PlayerTransform.position, cachedTransform.position) < 1.5f;
	bool CanSeePlayer()
	{
		Vector3 diff = GameManager.Instance.PlayerTransform.position - cachedTransform.position;
		RaycastHit hitResult;
		bool isHit = Physics.Raycast(new Ray(cachedTransform.position, diff.normalized), out hitResult, 8f, Constants.Layer.Mask.PLAYER | Constants.Layer.Mask.MAP);
		return isHit && hitResult.collider.CompareTag("Player");
	}


	public override void Release()
	{
		if (hpBar)
			hpBar.Release();

		base.Release();
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Helper.DrawRect(region, transform.position.y, Color.red);
	}
#endif
}
