using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public partial class GameManager : TMonoSingleton<GameManager>, IStatable<GameState>, IDisposable
{
	private GameObject m_hero;
	private HeroManager m_heroManager;
	private GameObject m_enemy;

	private float m_stageTime;

	private int m_totalEnemyCount;
	private int m_totalWaveTime;
	private int m_currentWave;
	private int m_currentWaveEnemies;
	private int m_nowRemindEnemies;

	private List<List<int>> m_enemyList = new List<List<int>>();

	private bool m_stageClear;
	public bool StageClear
	{
		get
		{
			return m_stageClear;
		}
	}
	private bool m_stageFail;
	public bool StageFail
	{
		get
		{
			return m_stageFail;
		}
	}
	private bool m_stageTimeOut;
	public bool StageTimeOut
	{
		get
		{
			return m_stageTimeOut;
		}
	}

	private void m_PalyInit()
	{
		m_stageTime = 0f;
		m_totalEnemyCount = 0;
		m_totalWaveTime = 0;
		m_currentWave = -1;
		m_currentWaveEnemies = 0;
		m_nowRemindEnemies = 0;

		m_stageClear = false;
		m_stageFail = false;
		m_stageTimeOut = false;
	}
	public void GamePlay()
	{
		m_hero = UserManager.Instance.Hero;

		
		//Instantiate(Resources.Load("Prefab/Player/Hero"));
		//m_hero = GameObject.Find("Hero(Clone)");

		m_heroManager = (HeroManager)m_hero.GetComponent(typeof(HeroManager));
		Vector3 heroPos = new Vector3(-2f, 2f, 90f);
		m_hero.transform.position = heroPos;

		//m_heroManager.StartInit();
		m_StageEnemyLoad();


		
		// Wrong Posision?
		m_heroManager.BlockLoad(m_bordName);
	}

	public void DoBlockLoad()
	{
		//m_heroManager.BlockLoad(m_bordName);
	}

	private void m_StageEnemyLoad()
	{
		m_enemyList.Clear();

		//Debug.Log("DB/Stage/" + m_stageName);
		TextAsset StageDB = Resources.Load("DB/Stage/" + m_stageName, typeof(TextAsset)) as TextAsset;

		string[] dbInfo;

		string readLine;
		string sep = "\t";

		StringReader BlockDBFind = new StringReader(StageDB.text);

		// Setp 1. Time
		while((readLine = BlockDBFind.ReadLine()) != null)
		{
			dbInfo = readLine.Split(sep.ToCharArray());
			
			if(dbInfo[0] == "Time")
			{
				m_stageTime = float.Parse(dbInfo[1]);
				break;
			}
		}

		// Setp 2. Total enemies count
		while((readLine = BlockDBFind.ReadLine()) != null)
		{
			dbInfo = readLine.Split(sep.ToCharArray());
			
			if(dbInfo[0] == "Enemy")
			{
				m_totalEnemyCount = int.Parse(dbInfo[1]);
				break;
			}
		}

		// Step 3. Wave times
		while((readLine = BlockDBFind.ReadLine()) != null)
		{
			dbInfo = readLine.Split(sep.ToCharArray());
			
			if(dbInfo[0] == "Wave")
			{
				m_totalWaveTime = int.Parse(dbInfo[1]);
				break;
			}
		}

		// Step 4. Load each wave
		for(int i = 0; i < m_totalWaveTime; i++)
		{
			while((readLine = BlockDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());
				int waveNum = i + 1;

				string waveString = "Wave" + waveNum.ToString();
			
				if(dbInfo[0] == waveString)
				{
					int waveEnemy = int.Parse(dbInfo[1]);
					//Debug.Log("waveEnemy : " + waveEnemy);
					List <int>WaveInfo = new List<int>();

					for(int j = 0; j < waveEnemy; j++)
					{
						WaveInfo.Add(int.Parse(dbInfo[2 + j]));
					}

					m_enemyList.Add(WaveInfo);

					break;
				}
			}
		}

		// Step 5. Instantiate of first wave
		m_currentWave = 0;
		m_InstantiateEnemy();
	}

	public void EnemyDie()
	{
		m_nowRemindEnemies--;

		if(0 == m_nowRemindEnemies)
		{
			m_currentWave++;
			m_InstantiateEnemy();
		}
	}

	private void m_InstantiateEnemy()
	{
		if(m_currentWave == m_totalWaveTime)
		{
			m_StageClear();
			return ;
		}

		m_currentWaveEnemies = m_enemyList[m_currentWave].Count;
		m_nowRemindEnemies = m_currentWaveEnemies;

		//Debug.Log("m_currentWaveEnemies" + m_currentWaveEnemies);

		for(int i = 0; i < m_currentWaveEnemies; i++)
		{
			m_enemy = Instantiate(Resources.Load("Prefab/Enemy/1")) as GameObject; 

			float yValue = UnityEngine.Random.Range(0.5f, 3f);

			Vector3 enemyPos = new Vector3((2.5f + 10f), yValue, 0f);
			m_enemy.transform.position = enemyPos;

			m_enemy.transform.parent = UIManager.Instance.UIGame.transform;
		}
	}

	private void m_StageClear()
	{
		m_stageClear = true;
		m_stageTimeOut = false;
		m_stageFail = false;
		StartCoroutine(EndGame());
	}

	private void m_StageDraw()
	{
		m_stageClear = false;
		m_stageTimeOut = true;
		m_stageFail = false;
		StartCoroutine(EndGame());
	}

	private void m_StageLose()
	{
		m_stageClear = false;
		m_stageTimeOut = false;
		m_stageFail = true;
		StartCoroutine(EndGame());
	}


	IEnumerator EndGame()
	{
		//print(Time.time);
		yield return new WaitForSeconds(3f);
		m_hero.transform.position = new Vector3(10f, 10f, 10f);

		GameManager.Instance.ChangeState (GameState.kResult);
    }


	public void StageDraw()
	{
		m_StageDraw();
	}

	public void StageLose()
	{
		m_StageLose();
	}
}
