using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIOutStage : UIBase
{
	List<GameObject> m_StageList;
	List<List<GameObject>> m_RoundList;

	GameObject m_selectionInfo;
	public override void Initialize ()
	{
		Debug.Log("OutStage Init!!");
		m_StageList = new List<GameObject>();

		m_RoundList = new List<List<GameObject>>();

		for(int i = 1; ; i++)
		{
			if(null == gameObject.transform.Find("PlStageSelect" + i.ToString()))
			{
				break;
			}

			GameObject addStages = gameObject.transform.Find("PlStageSelect" + i.ToString()).gameObject;

			m_StageList.Add(addStages);
			//Debug.Log(addStages.name);
		}

		// At least, it will load more than one round
		m_RoundList.Add(new List<GameObject>());
		for(int i = 1; ; i++)
		{
			int j = 1;
			for( ; ; j++)
			{
				if(null == gameObject.transform.Find("PlRoundSelect" + i.ToString() + "_" + j.ToString()))
				{
					break;
				}

				GameObject addRound = gameObject.transform.Find("PlRoundSelect" + i.ToString() + "_" + j.ToString()).gameObject;
				m_RoundList[i-1].Add(addRound);

				//Debug.Log(addRound.name);
				
			}
			if(j == 1)
			{
				break;
			}

			m_RoundList.Add(new List<GameObject>());
		}

		m_selectionInfo = gameObject.transform.Find("PlSelectionInfo").gameObject;
	}

	public override void Open()
	{
		CheckInitialize ();
		Debug.Log("Stage in");
		gameObject.SetActive (true);

		foreach(var stage in m_StageList)
		{
			stage.SetActive(false);
		}

		foreach(var roundList in m_RoundList)
		{
			foreach(var round in roundList)
			{
				round.SetActive(false);
			}
		}

		m_selectionInfo.SetActive(false);
	}

	public override void Close()
	{
		Debug.Log("Stage Out");
		gameObject.SetActive (false);
	}

	public void OnClickBtArea(string areaNum)
	{
		int stageIndex = int.Parse(areaNum) - 1;

		foreach(var stage in m_StageList)
		{
			stage.SetActive(false);
		}

		foreach(var roundList in m_RoundList)
		{
			foreach(var round in roundList)
			{
				round.SetActive(false);
			}
		}

		m_StageList[stageIndex].SetActive(true);

		OutStageInfo info = (OutStageInfo)m_selectionInfo.GetComponent(typeof(OutStageInfo));
		info.HideInfo();
	}
	
	public void OnClickBtStage(string stageNum)
	{
		string[] stageIndexValue;

		string sep = " ";

		stageIndexValue = stageNum.Split(sep.ToCharArray());

		foreach(var roundList in m_RoundList)
		{
			foreach(var round in roundList)
			{
				round.SetActive(false);
			}
		}

		int areaIndex = int.Parse(stageIndexValue[0]) - 1;

		int stageIndex = int.Parse(stageIndexValue[1]) - 1;

		m_RoundList[areaIndex][stageIndex].SetActive(true);

		OutStageInfo info = (OutStageInfo)m_selectionInfo.GetComponent(typeof(OutStageInfo));
		info.HideInfo();
	}

	public void OnClickBtRound(string roundName)
	{
		OutStageInfo info = (OutStageInfo)m_selectionInfo.GetComponent(typeof(OutStageInfo));
		Debug.Log(roundName);
		info.RoundName = roundName;
	}

	public void OnClickBtStart()
	{
		OutStageInfo info = (OutStageInfo)m_selectionInfo.GetComponent(typeof(OutStageInfo));
		GameManager.Instance.ChangeState(GameState.kPlay, info.RoundName);
	}
}
