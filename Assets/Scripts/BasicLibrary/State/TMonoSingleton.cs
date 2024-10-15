using UnityEngine;
using System.Collections;

public abstract class TMonoSingleton<T> : MonoBehaviour where T : TMonoSingleton<T>
{
	private static T m_instance = null;
	public static T Instance
	{
		get
		{
			return m_instance;
		}
	}

	public static void Create ()
	{
		if (null == m_instance) 
		{
			m_instance = new UnityEngine.GameObject ().AddComponent<T>();
			DontDestroyOnLoad (m_instance);
			m_instance.Initialize();
		}
	}

	public abstract void Initialize ();

	public abstract void Destroy ();
}
