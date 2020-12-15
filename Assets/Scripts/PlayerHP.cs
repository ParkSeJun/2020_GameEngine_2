using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
	[SerializeField] GameObject[] hearts;

	public void SetHp(int leftHp)
	{
		for (int i = 0; i < hearts.Length; i++)
		{
			hearts[i].SetActive(i < leftHp);
		}
	}
}
