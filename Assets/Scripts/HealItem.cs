using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
	private void Update()
	{
		float distance = Vector3.Distance(transform.position, GameManager.Instance.PlayerTransform.position);
		if (distance <= 1f)
		{
			GameManager.Instance.PlayerEntity.Heal(5);
			gameObject.SetActive(false);
		}
	}
}
