using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIGame : UIBase
{
	private Text m_lbScore;
	private Text m_lbTime;
	private Image m_imHPRed;
	

	private List<Sprite> m_bordSprite;

	public override void Initialize ()
	{
		if(false == m_isInitialized)
		{
			//m_lbScore = transform.FindChild ("LbScore").GetComponent<Text> ();
			m_imHPRed = transform.Find ("ImHPRed").GetComponent<Image> ();

			m_lbTime = gameObject.transform.Find("LbRemainingTime").GetComponent<Text>();

			m_isInitialized = true;
		}
	}

	public void SetScore(int score)
	{
		//m_lbScore.text = score.ToString ();
	}

	public void SetTime(float time)
	{
		int displayTime = (int)time;
		m_lbTime.text = displayTime.ToString();
	}

	public void SetHPRed(float hpPercent)
	{
		//Debug.Log("HP Display : " + hpPercent);
		m_imHPRed.fillAmount = hpPercent;
	}

	public void SetBord(List<string> bordInfo)
	{
		// way of Sprite List initialize 
		m_bordSprite = new List<Sprite>();
		//Debug.Log("setBord!");

		//Debug.Log(bordInfo.Count);
		
		foreach (var item in bordInfo)
		{
			//Debug.Log(item);
			m_bordSprite.Add((Sprite)Resources.Load<Sprite>("BlockIcon/"+item));
		}
		
	}

	public Sprite GetBordSprite(int bordNum)
	{
		return m_bordSprite[bordNum];
	}
}
