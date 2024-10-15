using UnityEngine;
using System.Collections;

public abstract class UIBase : MonoBehaviour, IInitializable
{
	protected bool m_isInitialized = false;

	public virtual void Open()
	{
		CheckInitialize ();
		gameObject.SetActive (true);
	}

	public virtual void Close()
	{
		gameObject.SetActive (false);
	}

	public bool IsInitialized()
	{
		return m_isInitialized;
	}

	public void CheckInitialize()
	{
		if (true == m_isInitialized)
		{
			return;
		}
		Initialize ();
	}
	
	public abstract void Initialize();
}
