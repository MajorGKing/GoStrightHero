using UnityEngine;
using System.Collections;

[System.Serializable]

public class HeroSave
{
	public bool m_savefileMaked = false;
	public float m_MaxHP;
	public float m_CurrentHP;
	public int m_Armour;
	public float m_Avoid;
	public int m_Heal; 
	public int m_AttackPow;
	public float m_AttackSucess;
	public float m_AttackSpeed;
	public float m_MoveSpeed;

	public HeroSave()
	{
		m_savefileMaked = false;
	}
}
