using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
public class OutStageInfo : MonoBehaviour
{
	private Text m_stageInfo;
	private GameObject m_startBtn;
	private string m_infoString;
	private string m_roundName;
	
	public string RoundName
	{
		get
		{
			return m_roundName;
		}
		set
		{
			if(m_initialize == false)
			{
				m_Initialize();
			}

			gameObject.SetActive(true);

			m_roundName = value;

			m_ShowInfo();
		}
	}

	

	private bool m_initialize = false;

	public void Start()
	{
		if(m_initialize == false)
		{
			m_Initialize();
		}
	}


	private void m_Initialize()
	{
		m_roundName = "";
		m_stageInfo = gameObject.transform.Find("TxSelectInfoText").gameObject.GetComponent<Text>();
		m_startBtn  = gameObject.transform.Find("BtStartGame").gameObject;
		m_infoString = "";
		m_initialize = true;
		m_stageInfo.text = "";
	}

	private void m_ShowInfo()
	{
		TextAsset infoTxt = Resources.Load("DB/Stage/StageInfo/" + m_roundName, typeof(TextAsset)) as TextAsset;
		StringReader infoTxtString = new StringReader(infoTxt.text);
		string readLine;

		m_infoString = "";
		while((readLine = infoTxtString.ReadLine()) != null)
		{
			m_infoString += readLine;
			m_infoString += "\n";
		}

		m_stageInfo.text = m_infoString;

		if(SaveLoadManager.Instance.GameData.heroSave.m_CurrentHP <= 0)
		{
			m_startBtn.SetActive(false);
		}
		else if(SaveLoadManager.Instance.GameData.heroSave.m_CurrentHP > 0)
		{
			m_startBtn.SetActive(true);
		}
	}

	public void HideInfo()
	{
		if(gameObject.activeSelf == true)
		{
			m_infoString = "";
			m_stageInfo.text = "";

			gameObject.SetActive(false);
		}
	}
}
