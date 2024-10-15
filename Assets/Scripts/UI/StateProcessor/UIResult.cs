using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIResult : UIBase
{
	private GameObject m_plWin;
	private GameObject m_plLose;
	private GameObject m_plTimeOut;
	public override void Initialize ()
	{
		m_plWin = transform.Find ("PlWin").gameObject;
		m_plLose = transform.Find ("PlLose").gameObject;
		m_plTimeOut = transform.Find ("PlTimeOut").gameObject;

		if(GameManager.Instance.StageClear == true)
		{
			m_plWin.SetActive(true);
			m_plLose.SetActive(false);
			m_plTimeOut.SetActive(false);
		}
		else if(GameManager.Instance.StageFail == true)
		{
			m_plWin.SetActive(false);
			m_plLose.SetActive(true);
			m_plTimeOut.SetActive(false);
		}
		else if(GameManager.Instance.StageTimeOut == true)
		{
			m_plWin.SetActive(false);
			m_plLose.SetActive(false);
			m_plTimeOut.SetActive(true);
		}
	}

	public void OnClickBtGoMain()
	{
		SaveLoadManager.Instance.Save();
		GameManager.Instance.ChangeState (GameState.kOutGame);
	}
}
