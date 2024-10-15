using UnityEngine;
//using System.Collections;

public class EnemyAnimation : MonoBehaviour 
{
	private bool m_move;
	private bool m_attack;
	private bool m_die;

	private float m_moveSpeed;

	private Animator m_animator;
	private AnimatorStateInfo m_aniStateInfo;

	private EnemyManager m_manager;

	private int m_hitState;
	private int m_attackState;
	private int m_dieState;

	private int m_currentState;
	private int m_backState;

	private HeroManager m_hero;


	// Use this for initialization
	void Start () 
	{
		m_move = true;
		m_attack = false;
		m_die = false;

		m_animator = GetComponent<Animator>();
		
		m_hitState = Animator.StringToHash("Base Layer.Hit");
		m_attackState = Animator.StringToHash("Base Layer.Attack");
		m_dieState = Animator.StringToHash("Base Layer.Die");

		m_currentState = m_attackState;
		m_backState = m_attackState;
		

		m_manager = (EnemyManager)gameObject.GetComponent(typeof(EnemyManager));

		m_moveSpeed = m_manager.MoveSpeed;

		// it is not good. nxt time, find better way.
		GameObject hero = GameObject.Find("Hero");
		m_hero = (HeroManager)hero.GetComponent(typeof(HeroManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		//add about delta time
		if(true == m_move)
		{
			Vector3 nextPos = gameObject.transform.position;
			nextPos.x -= m_moveSpeed * Time.deltaTime;

			gameObject.transform.position = nextPos;
		}
		else if(true == m_attack)
		{
			m_aniStateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
			m_currentState = m_aniStateInfo.nameHash;

			if(m_currentState == m_hitState && m_backState == m_attackState)
			{
				m_manager.AttackDid();
				//Debug.Log("attack!! OK");

				m_backState = m_hitState;
			}
			else if(m_backState == m_hitState && m_currentState == m_attackState)
			{
				m_backState = m_attackState;
			}
		}
		else if(true == m_die)
		{
			m_aniStateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
			m_currentState = m_aniStateInfo.nameHash;

			if(m_currentState == m_dieState)
			{
				m_animator.SetBool("Die", false);
				m_animator.SetBool("Died", true);
			}
		}
		// add move about hero move

		if(true == m_hero.Move)
		{
			Vector3 nextPos = gameObject.transform.position;
			nextPos.x -= m_hero.MoveSpeed;

			gameObject.transform.position = nextPos;
		}
		

	}

	public void Move()
	{
		m_move = true;
		m_attack = false;
				
		m_animator.SetBool("AttackEnd", true);
		m_animator.SetBool("AttackBegin", false);
	}

	public void Attack()
	{
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
		m_animator.SetBool("AttackEnd", false);
		m_animator.SetBool("AttackBegin", false);
	}
}
