using UnityEngine;
using System.Collections;

public class HeroAnimation : MonoBehaviour 
{
	private bool m_move;
	private bool m_attack;

	private bool m_die;

	private float m_moveSpeed;

	private Animator m_animator;
	private AnimatorStateInfo m_aniStateInfo;

	private HeroManager m_manager;

	int m_hitState;
	int m_attackState;

	int m_currentState;
	int m_backState;

	// Use this for initialization
	void Start () 
	{
		m_move = true;
		m_attack = false;
		m_die = false;

		m_moveSpeed = 0.02f;

		m_animator = GetComponent<Animator>();

		m_manager = (HeroManager)gameObject.GetComponent(typeof(HeroManager));

		m_hitState = Animator.StringToHash("Base Layer.Hit");
		m_attackState = Animator.StringToHash("Base Layer.Attack");

		m_currentState = m_attackState;
		m_backState = m_attackState;

		MoveOn();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(true == m_attack)
		{
			m_aniStateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
			m_currentState = m_aniStateInfo.nameHash;

			if(m_currentState == m_hitState && m_backState == m_attackState)
			{
				m_manager.AttackDid();
				

				m_backState = m_hitState;
			}
			else if(m_backState == m_hitState && m_currentState == m_attackState)
			{
				m_backState = m_attackState;
			}
		}
	}

	public void MoveOn()
	{
		//Debug.Log("Hero Move");
		m_move = true;
		m_attack = false;

		m_animator.SetBool("AttackEnd", true);
		m_animator.SetBool("AttackBegin", false);
	}

	public void AttackOn()
	{
		//Debug.Log("OnTriger");

		m_move = false;
		m_attack = true;

		m_animator.SetBool("AttackEnd", false);
		m_animator.SetBool("AttackBegin", true);
	}

	public void Die()
	{
		m_move = false;
		m_attack = false;
		m_die = true;

		m_animator.SetBool("Die", true);
	}
}
