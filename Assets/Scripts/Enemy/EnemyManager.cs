using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour 
{
	public int maxHP;
	public int currentHP;
	
	public int attackPOW;
	public float attackSucess;
	public float attackSpeed;

	public float moveSpeed;
	public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
	}

	public int armour;

	public float avoid;


	private bool m_move;
	private bool m_attack;
	private bool m_die;


	private EnemyAnimation m_animation;

	private HeroManager m_hero;

	private GameObject m_energyBar;
	private float m_orgEnergySize;

	
	
	// Use this for initialization
	void Start () 
	{
		m_animation = (EnemyAnimation)gameObject.GetComponent(typeof(EnemyAnimation));
		m_move = true;
		m_attack = false;
		m_die = false;

		m_energyBar = gameObject.transform.Find("EnemyHP").gameObject;

		m_orgEnergySize = m_energyBar.transform.localScale.x;

		//Debug.Log("m_orgEnergySize : " + m_orgEnergySize);

		gameObject.name = "Enemy1";
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	void OnTriggerEnter2D(Collider2D other)
    {
		if(true == m_move)
		{
			//Debug.Log("OnTriger");
			if(other.tag == "Player")
			{
				m_move = false;
				m_attack = true;

				m_animation.Attack();		

				m_hero = (HeroManager)other.gameObject.GetComponent(typeof(HeroManager));
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
    {
		if(true == m_attack)
		{
			
			if(other.tag == "Player")
			{
				m_move = true;
				m_attack = false;

				m_animation.Move();

			}
		}
	}

	public void AttackDid()
	{
		if(true == m_attack)
		{
			float randomNum = Random.Range(0f, 1f);
			if(randomNum <= attackSucess)
			{
				m_hero.GetDamage(attackPOW);
			}
		}
	}

	public void GetDamage(int damage)
	{
		float randomNum = Random.Range(0f, 1f);
		if(randomNum <= avoid)
		{
			return ;
		}

		int getDamage = 0;

		if(damage <= armour)
		{
			getDamage = 1;
		}
		else if(damage > armour)
		{
			getDamage = damage - armour;
		}

		currentHP -= getDamage;

		 m_energyBar.transform.localScale = new Vector3((float)currentHP/(float)maxHP * m_orgEnergySize, m_energyBar.transform.localScale.y, m_energyBar.transform.localScale.z);
		

		if(0 >= currentHP)
		{
			if(null == m_hero)
			{
				GameObject hero = GameObject.Find("Hero");
				m_hero = (HeroManager)hero.GetComponent(typeof(HeroManager));
			}

			m_energyBar.transform.localScale = new Vector3(0f, m_energyBar.transform.localScale.y, m_energyBar.transform.localScale.z);

			//Debug.Log(gameObject.name + " die : !!!");
			m_animation.Die();	

			m_hero.TargetDied(gameObject);

			Destroy(gameObject, 3);

			GameManager.Instance.EnemyDie();
		}
	}
}


