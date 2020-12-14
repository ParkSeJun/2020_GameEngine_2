using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField] Transform gunTip;

	public Vector3 GetGunTipPosition() => gunTip.position;

	public Transform GetGunTipTransform() => gunTip;
}
