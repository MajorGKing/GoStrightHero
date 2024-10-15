using UnityEngine;
using System.Collections;

public class HeroAttack : MonoBehaviour 
{
	private HeroManager m_heroManager;
	// Use this for initialization
	void Start () 
	{
		m_heroManager = (HeroManager)transform.parent.gameObject.GetComponent(typeof(HeroManager));
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
    {
		//if(true == m_move)
		//{
			
			if(other.tag == "Enemy_Col")
			{
				//Debug.Log("Enemy Enter");

				m_heroManager.AttackTarget(other.transform.parent.gameObject);
			}
		//}
	}

	void OnTriggerExit2D(Collider2D other)
    {
		if(other.tag == "Enemy_Col")
		{
			//Debug.Log("Enemy Exit");

			m_heroManager.RemoveTarget(other.transform.parent.gameObject);
		}
	}
}
