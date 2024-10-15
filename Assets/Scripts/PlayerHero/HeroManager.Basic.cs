using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public partial class HeroManager : MonoBehaviour 
{
	private float m_maxHP;
	private float m_currentHP;

	private int m_armour;
	private float m_armourAdd;
	private int m_armourAddindex;
	
	private float m_avoid;
	private float m_avoidAdd;
	private int m_avoidAddIndex;
	private int m_heal; 
	private int m_healIndex;

	private int m_attackPow;
	private float m_attackPowAdd;
	private int m_attackPowAddIndex;
	private float m_attackSucess;
	private float m_attackSucessAdd;
	private int m_attackSucessAddIndex;
	private float m_attackSpeed;
	private float m_attackSpeedAdd;
	private int m_attackSpeedAddIndex;

	private HeroAnimation m_animation;

	private List<GameObject> m_enemies;
	private EnemyManager m_enemy;
	private GameObject m_enemyObj;


	private bool m_attack;
	private bool m_move;
	public bool Move
	{
		get
		{
			return m_move;
		}
	}
	private float m_moveSpeed;
	private float m_moveSpeedAdd;
	private int m_moveSpeedAddIndex;
	public float MoveSpeed
	{
		get
		{
			return m_moveSpeed;
		}
	}
	private bool m_die;

	private int[,] m_blockType;
	private float[,] m_blockValue;

	private bool m_init = false;


	// Use this for initialization

	private void m_Init()
	{
		//Debug.Log(m_currentHP);

		m_enemies = new List<GameObject>();

		m_attack = false;
		m_move = true;
		m_die = false;

		m_animation = (HeroAnimation)gameObject.GetComponent(typeof(HeroAnimation));

		gameObject.name = "Hero";


		

		m_blockType = new int[4, 3];	// 4 means counte of slots

		m_blockValue = new float[4, 10];

		m_heroSave = new HeroSave();
		//GameManager.Instance.DoBlockLoad();
	}

	// Update is called once per frame

	public void HeroDataLoad()
	{
		m_HeroDataLoad();
	}
	private void m_HeroDataLoad()
	{
		m_heroSave = SaveLoadManager.Instance.GameData.heroSave;

		Debug.Log("SaveLoadManager.Instance.GameData.heroSave : " + SaveLoadManager.Instance.GameData.heroSave.m_CurrentHP);

		if(m_heroSave.m_savefileMaked == false)
		{
			Debug.Log("Hero Init");
			m_maxHP = 100;
			m_currentHP = m_maxHP;

			m_attackPow = 10;
			m_attackPowAdd = 0f;
			m_attackPowAddIndex = -1;

			m_attackSucess = 0.8f;
			m_attackSucessAdd = 0f;
			m_attackSucessAddIndex = -1;

			m_armour = 0;
			m_armourAdd = 0f;
			m_armourAddindex = -1;

			m_avoid = 0.2f;
			m_avoidAdd = 0f;
			m_avoidAddIndex = -1;

			m_healIndex = -1;

			m_moveSpeed = 0.05f;
			m_moveSpeedAdd = 0f;
			m_moveSpeedAddIndex = -1;

			SaveLoadManager.Instance.Save();
		}
		else if(m_heroSave.m_savefileMaked == true)
		{
			Debug.Log("Hero Load");
			m_maxHP = m_heroSave.m_MaxHP;
		
			m_currentHP = m_heroSave.m_CurrentHP;
			m_attackPow = m_heroSave.m_AttackPow;
			m_attackPowAdd = 0f;
			m_attackPowAddIndex = -1;

			m_attackSucess = m_heroSave.m_AttackSucess;
			m_attackSucessAdd = 0f;
			m_attackSucessAddIndex = -1;

			m_armour = m_heroSave.m_Armour;
			m_armourAdd = 0f;
			m_armourAddindex = -1;

			m_avoid = m_heroSave.m_Avoid;
			m_avoidAdd = 0f;
			m_avoidAddIndex = -1;

			m_healIndex = -1;

			m_moveSpeed = m_heroSave.m_MoveSpeed;
			m_moveSpeedAdd = 0f;
			m_moveSpeedAddIndex = -1;
		}

		Debug.Log(m_heroSave.m_MaxHP);
		Debug.Log(m_heroSave.m_CurrentHP);
	}

	public void GamePlay()
	{
		UIManager.Instance.UIGame.SetHPRed(1 - m_currentHP/m_maxHP);
	}
	void Update () 
	{
	
	}

	public void GetDamage(int damage)
	{
		float randomNum = Random.Range(0f, 1f);

		//Debug.Log(randomNum);
		float totalAvoid = m_avoid + m_avoidAdd;

		if(randomNum > totalAvoid)
		{
			int getDamage = 0;

			if(damage <= m_armour)
			{
				getDamage = 1;
			}
			else if(damage > m_armour)
			{
				getDamage = damage - m_armour;
			}

			m_currentHP -= damage;
			//Debug.Log("Now HP : " + m_currentHP);

			//Debug.Log("Has Come " + damage);

			UIManager.Instance.UIGame.SetHPRed(1 - m_currentHP/m_maxHP);

			if(m_currentHP <= 0)
			{
				m_animation.Die();
				GameManager.Instance.StageLose();
			}			
		}

		if(m_avoidAdd > 0f)
		{
			// block count become zero
			//Debug.Log("Index : " + m_avoidAddIndex);
			m_blockType[m_avoidAddIndex, 2] = 0;
			m_avoidAdd = 0;
		}
	}

	public void AttackDid()
	{
		if(true == m_attack)
		{
			float randomNum = Random.Range(0f, 1f);

			float totalAttackSucess = m_attackSucess + m_attackSucessAdd;



			if(randomNum <= totalAttackSucess)
			{
				int totalAttackPow = m_attackPow + (int)m_attackPowAdd;

				//Debug.Log("Total Pow : " + totalAttackPow);

				m_enemy.GetDamage(totalAttackPow);

				if(m_attackPowAdd > 0f)
				{
					m_blockType[m_attackPowAddIndex, 2] = 0;
					m_attackPowAdd = 0f;
				}
				
			}
			
			if(m_attackSucessAdd > 0f)
			{
				m_blockType[m_attackSucessAddIndex, 2] = 0;
				m_attackSucessAdd = 0f;	
			}
		}
	}

	public void AttackTarget(GameObject enemyObj)
	{
		//Debug.Log(enemyObj.name + "Ene");

		m_enemies.Add(enemyObj);

		if(false == m_attack)
		{
			// attack animation start
			m_animation.AttackOn();
			// 
			m_enemyObj = enemyObj;
			m_enemy = (EnemyManager)m_enemyObj.GetComponent(typeof(EnemyManager));

			m_attack = true;
			m_move = false;
		}
	}

	public void RemoveTarget(GameObject enemyObj)
	{
		//Debug.Log(enemyObj.name + "Del");

		if(m_enemies.Contains(m_enemyObj) == true)
		{
			m_enemies.Remove(enemyObj);
		}

		//Debug.Log(m_enemies.Count);

		if(m_enemies.Count == 0)
		{
			m_enemyObj = null;
		}
		else if(m_enemies.Count > 0)
		{
			m_enemyObj = m_enemies[0];
		}

		if(null == m_enemyObj)
		{
			m_attack = false;
			m_move = true;

			m_animation.MoveOn();
		}
		else if(null != m_enemyObj)
		{
			m_enemy = (EnemyManager)m_enemyObj.GetComponent(typeof(EnemyManager));
		}
	}

	public void TargetDied(GameObject enemyObj)
	{
		//Debug.Log(enemyObj.name + "Die");

		m_enemies.Remove(enemyObj);

		if(0 == m_enemies.Count)
		{
			m_enemyObj = null;

			m_attack = false;
			m_move = true;

			m_animation.MoveOn();
		}
		else if(0 < m_enemies.Count)
		{
			m_enemyObj = m_enemies[0];
			m_enemy = (EnemyManager)m_enemyObj.GetComponent(typeof(EnemyManager));
		}
	}

	public void BlockLoad(List <string>name)
	{
		TextAsset BlockDB = Resources.Load("DB/Block/BlockAbilityValue", typeof(TextAsset)) as TextAsset;

		// Must change, after 
		int slotSize = 4;
		// Importent

		string[] dbInfo;

		for(int i = 0; i < slotSize; i++)
		{
			string readLine;
			string sep = "\t";

			StringReader BlockDBFind = new StringReader(BlockDB.text);

			while((readLine = BlockDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());
			
				if(dbInfo[0] == name[i])
				{
					//Debug.Log(int.Parse(dbInfo[1]));
					m_blockType[i,0] = int.Parse(dbInfo[2]);
					//Debug.Log(dbInfo[2]);
					m_blockType[i,1] = int.Parse(dbInfo[3]);

					for(int j = 0; j < 10; j++)
					{
						//Debug.Log("i : " + dbInfo[4 + j]);
						m_blockValue[i,j] = float.Parse(dbInfo[4 + j]);
					}

					break;
				}
			}
		}

		m_BlockTypeIndexing();
	}

	private void m_BlockTypeIndexing()
	{
		// Must change, after 
		int slotSize = 4;
		// Importent

		TextAsset AttackPow = Resources.Load("DB/Block/BlockTypeIndex/AttackPowAddType", typeof(TextAsset)) as TextAsset;
		TextAsset AttackSucess = Resources.Load("DB/Block/BlockTypeIndex/AttackSucessAddType", typeof(TextAsset)) as TextAsset;
		TextAsset Avoid = Resources.Load("DB/Block/BlockTypeIndex/AvoidSuccessAddType", typeof(TextAsset)) as TextAsset;
		TextAsset Heal = Resources.Load("DB/Block/BlockTypeIndex/HealType", typeof(TextAsset)) as TextAsset;

		for(int i = 0; i < slotSize; i++)
		{
			bool inside = false;
			inside = m_CheckBlockTypeInDB(AttackPow, m_blockType[i,0]);
			if(inside == true)
			{
				//Debug.Log("Attack Pow Index : " + i);
				m_attackPowAddIndex = i;
				continue;
			}

			inside = m_CheckBlockTypeInDB(AttackSucess, m_blockType[i,0]);
			if(inside == true)
			{
				//Debug.Log("Attack Sucess Index : " + i);
				m_attackSucessAddIndex = i;
				continue;
			}

			inside = m_CheckBlockTypeInDB(Avoid, m_blockType[i,0]);
			if(inside == true)
			{
				//Debug.Log("Attack Avoid Index : " + i);
				m_avoidAddIndex = i;
				continue;
			}

			inside = m_CheckBlockTypeInDB(Heal, m_blockType[i,0]);
			if(inside == true)
			{
				//Debug.Log("Heal Index : " + i);
				m_healIndex = i;
				continue;
			}
		}
	}

	private bool m_CheckBlockTypeInDB(TextAsset Text, int index)
	{
		string readLine;

		StringReader BlockDBFind = new StringReader(Text.text);

		while((readLine = BlockDBFind.ReadLine()) != null)
		{
			if(readLine == "Type")	// skip first line
			{
				continue;
			}

			if(int.Parse(readLine) == index)
			{
				return true;
			}
		}
		return false;
	}
	
}
