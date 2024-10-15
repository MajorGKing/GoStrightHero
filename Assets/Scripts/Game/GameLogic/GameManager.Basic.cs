using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public partial class GameManager : TMonoSingleton<GameManager>, IStatable<GameState>, IDisposable
{
	private TStateController<GameState> m_stateController;

	private bool m_isDisposed;

	List<string> m_bordName;
	List<int> m_bordValue;

	private int m_totalBlockValue;

	private string m_stageName;

	private void m_BasicInitialize ()
	{
		m_isDisposed = false;
		m_stateController = new TStateController<GameState> ();
		m_stateController.AddState (GameState.kIdle, new GameIdleProcessor ());
		m_stateController.AddState (GameState.kOutGame, new GameOutGameProcessor ());
		m_stateController.AddState (GameState.kPlay, new GamePlayProcessor ());
		m_stateController.AddState (GameState.kResult, new GameResultProcessor ());

		ChangeState (GameState.kIdle);

		m_bordName = new List<string>();
		m_bordValue = new List<int>();
		m_selectedBords = new List<GameObject>();

		m_totalBlockValue = 0;

		m_selectedBordType = -1;

		m_stageName = null;
	}


	public GameState GetState()
	{
		return m_stateController.CurrentState;
	}

	public IStateProcessor ChangeState(GameState state)
	{
		return m_stateController.ChangeState(state);
	}

	public IStateProcessor ChangeState(GameState state, string stageName)
	{
		m_stageName = stageName;
		//Debug.Log("Stage Name : " + stageName);
		return m_stateController.ChangeState(state);
	}

	public TStateController<GameState> GetStateController()
	{
		return m_stateController;
	}
	
	public float StageTime
	{
		get
		{
			return m_stageTime;
		}
	}
}
