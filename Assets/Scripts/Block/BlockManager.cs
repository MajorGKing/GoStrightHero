using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour 
{
	private int m_bordType;
	public int BordType
	{
		get
		{
			return m_bordType;
		}
	}

	private Animator m_animator;

	//private Sprite m_bordSprite;

	// Use this for initialization
	void Start () 
	{
		m_bordType = GameManager.Instance.GetBlockValue();

		gameObject.GetComponent<Image>().sprite = UIManager.Instance.UIGame.GetBordSprite(m_bordType);

		m_animator = GetComponent<Animator>();

		m_animator.SetBool("Selected", false);
		m_animator.SetBool("Clear", false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void BlockSelected()
	{
		//m_move = true;
		//m_attack = false;

		m_animator.SetBool("Selected", true);
		m_animator.SetBool("Clear", false);
	}

	public void BlockClear()
	{
		m_animator.SetBool("Selected", false);
		m_animator.SetBool("Clear", true);

		StartCoroutine(ReBlock(0.5f));
	}

	public void BlockSelectCancle()
	{
		//m_move = true;
		//m_attack = false;

		m_animator.SetBool("Selected", false);
		m_animator.SetBool("Clear", false);
	}

	IEnumerator ReBlock(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);	

		m_bordType = GameManager.Instance.GetBlockValue();
		Debug.Log("m_bordType is " + m_bordType);

		gameObject.GetComponent<Image>().sprite = UIManager.Instance.UIGame.GetBordSprite(m_bordType);
		m_animator.SetBool("Selected", false);
		m_animator.SetBool("Clear", false);
	}
}
