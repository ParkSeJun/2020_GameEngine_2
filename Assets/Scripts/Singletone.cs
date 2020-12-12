using UnityEngine;

public class Singletone<T> : MonoBehaviour
	where T : class
{
	public static T Instance
	{
		get
		{
			if (_instance == null)
				_instance = Object.FindObjectOfType(typeof(T)) as T;
			return _instance;
		}
	}

	private static T _instance;
}
