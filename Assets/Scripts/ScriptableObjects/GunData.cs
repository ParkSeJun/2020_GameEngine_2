using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "Scriptable Object/Gun Data", order = 1000)]
public class GunData : ScriptableObject
{
	[SerializeField] float damage;
	[SerializeField] float cooldown;
	[SerializeField] float bulletSize;
	[SerializeField] Texture2D texture;

	public float Damage => damage;
	public float Cooldown => cooldown;
	public float BulletSize => bulletSize;
	public Texture2D Texture => texture;
}
