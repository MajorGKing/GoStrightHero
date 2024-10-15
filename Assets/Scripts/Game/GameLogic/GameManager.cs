using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class GameManager : TMonoSingleton<GameManager>, IStatable<GameState>, IDisposable
{
	public override void Initialize ()
	{
		gameObject.name = "GameManager";

		m_BasicInitialize();
	}

	public override void Destroy ()
	{
		Dispose ();
	}

	public void Dispose()
	{
		if (true == m_isDisposed)
		{
			return;
		}

		// Do Dispose

		m_isDisposed = true;
	}

	void Update()
    {
        float updateTime = Time.deltaTime;

        m_stateController.Update(updateTime);
    }
}
