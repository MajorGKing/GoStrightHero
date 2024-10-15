using UnityEngine;
using System.Collections;

public partial class HeroManager : MonoBehaviour 
{
	private HeroSave m_heroSave;
	public HeroSave HeroSaveData
	{
		get
		{
			m_heroSave.m_savefileMaked = true;

			m_heroSave.m_MaxHP = m_maxHP;

			m_heroSave.m_CurrentHP = m_currentHP;
	
			m_heroSave.m_AttackPow = m_attackPow;
			
			m_heroSave.m_AttackSucess = m_attackSucess;
			
			m_heroSave.m_Armour = m_armour;

			m_heroSave.m_Avoid = m_avoid;

			m_heroSave.m_MoveSpeed = m_moveSpeed;
			
			return m_heroSave;
		}
	}

	void Start () 
	{
		if(m_init == false)
		{
			m_Init();
			m_init = true;
		}
	}
}
