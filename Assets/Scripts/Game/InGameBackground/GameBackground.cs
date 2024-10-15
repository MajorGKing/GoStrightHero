using UnityEngine;
using System.Collections;

public class GameBackground : MonoBehaviour 
{

	// Use this for initialization
	private HeroManager m_hero;

	private float m_canvasWidth;

	private float m_imageWidth;

	private float m_diffSpeed;
	void Start () 
	{
		//GameObject hero = GameObject.Find("Hero");
		//m_hero = (HeroManager)hero.GetComponent(typeof(HeroManager));

		m_canvasWidth = UIManager.Instance.CanvasWidth;

		m_imageWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;

		m_diffSpeed = 1f;

		if(gameObject.tag == "Background1")
		{
			m_diffSpeed = 0.1f;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_hero == true)
		{
			if(true == m_hero.Move)
			{
				Vector3 nextPos = gameObject.GetComponent<RectTransform>().position;
				nextPos.x -= (m_hero.MoveSpeed * m_diffSpeed)  * Time.deltaTime;

				gameObject.GetComponent<RectTransform>().position = nextPos;

				if(gameObject.GetComponent<RectTransform>().position.x < (0 - m_canvasWidth/2f - m_imageWidth/2f) * UIManager.Instance.UiWidthScale)
				{
					nextPos.x = (m_canvasWidth/2f + m_imageWidth/2f) * UIManager.Instance.UiWidthScale;
					gameObject.transform.position = nextPos;
				}
			}
		}
		else if(m_hero == null)
		{
			GameObject hero = GameObject.Find("Hero");
			m_hero = (HeroManager)hero.GetComponent(typeof(HeroManager));
		}
	}
}
