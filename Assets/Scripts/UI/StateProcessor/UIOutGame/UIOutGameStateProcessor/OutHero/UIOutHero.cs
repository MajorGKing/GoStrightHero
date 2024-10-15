using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIOutHero : UIBase
{
	private Dictionary<UIOutHeroType, UIBase> m_outUIHeroList;
	private UIOutHeroType m_currentState;

	private GameObject m_plShowInfo;
	private Text m_infoText;

	public override void Initialize ()
	{
		m_outUIHeroList = new Dictionary<UIOutHeroType, UIBase> ();
		m_outUIHeroList.Add (UIOutHeroType.kBlock, gameObject.transform.Find ("PlSettingBlock").GetComponent<UIOutHeroBlock> ());
		m_outUIHeroList.Add (UIOutHeroType.kEquip, gameObject.transform.Find ("PlSettingEquip").GetComponent<UIOutHeroEquip> ());
		m_outUIHeroList.Add (UIOutHeroType.kItem, gameObject.transform.Find ("PlSettingItem").GetComponent<UIOutHeroItem> ());
		//m_outUIList.Add (UIOutGameType.kOption, gameObject.transform.FindChild ("PlStageSelector").GetComponent<UIOutOption> ());		

		m_currentState = UIOutHeroType.kBlock;
		Open(m_currentState);

		m_plShowInfo = gameObject.transform.Find("PlShowInfo").gameObject;
		m_infoText = m_plShowInfo.transform.Find("InfoText").GetComponent<Text>();

		m_isInitialized = true;
	}

	public override void Open()
	{
		CheckInitialize ();
		Debug.Log("Hero in");
		gameObject.SetActive (true);
	}

	public override void Close()
	{
		Debug.Log("Hero out");
		gameObject.SetActive (false);
	}

	public void Open(UIOutHeroType type)
	{
		Debug.Log("Open : " + type);
		m_currentState = type;
		m_outUIHeroList [type].Open ();
	}

	public void Close(UIOutHeroType type)
	{
		Debug.Log("Close : " + type);
		m_outUIHeroList [type].Close ();
	}

	public void ChangeState(UIOutHeroType type)
	{
		Close(m_currentState);
		Open(type);
	}

	public void OnClickBtSettingBlock()
	{
		ChangeState(UIOutHeroType.kBlock);
	}

	public void OnClickBtSettingEquip()
	{
		ChangeState(UIOutHeroType.kEquip);
	}

	public void OnClickBtSettingItem()
	{
		ChangeState(UIOutHeroType.kItem);
	}

	public void ShowInfo(string text)
	{
		//Debug.Log("Show Info In");
		Debug.Log(text);

		

		m_infoText.text = text;
	}
}
