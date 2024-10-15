using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserManager : TMonoSingleton<UserManager> 
{
	// Data of User & Game Data
	// Use this for initialization

	List<string> bordName;
	private GameObject m_hero;
	public GameObject Hero
	{
		get
		{
			return m_hero;
		}
	}

	public override void Initialize ()
	{
		bordName = new List<string>{"A1","B1","C1","D1"};
		gameObject.name = "UserManager";

		m_hero = Instantiate(Resources.Load("Prefab/Player/Hero")) as GameObject;

		m_hero.transform.position = new Vector2(100f, 100f);
		m_hero.name = "Hero";
	}

	public override void Destroy ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void SendBordInfo()
	{
		for(int i = 0; ; i++)
		{
			if(SaveLoadManager.Instance.GameData.blockSave.blockName[i] == null)
			{
				break;
			}
			bordName[i] = SaveLoadManager.Instance.GameData.blockSave.blockName[i];
		}
		GameManager.Instance.SetBoardInfo(bordName);
	}
}
