using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class UIOutGame : UIBase
{
	private Dictionary<UIOutGameType, UIBase> m_outUIList;

	private UIOutGameType m_currentState;

	private GameObject m_plUserInfo;
	private Text m_txGold;
	private Text m_txHP;
	private Text m_txLevel;

	// TODO : will be chage
	private int blockSize = 6;
	public override void Initialize ()
	{
		m_isInitialized = true;

		m_plUserInfo = gameObject.transform.Find("PlUserInfo").gameObject;
		m_txGold = m_plUserInfo.transform.Find("TxGold").GetComponent<Text>();
		m_txHP = m_plUserInfo.transform.Find("TxHP").GetComponent<Text>();
		m_txLevel  = m_plUserInfo.transform.Find("TxLevel").GetComponent<Text>();
		//GameObject uiOutRoot = gameObject;

		m_outUIList = new Dictionary<UIOutGameType, UIBase> ();
		m_outUIList.Add (UIOutGameType.kStage, gameObject.transform.Find ("PlStageSelector").GetComponent<UIOutStage> ());
		m_outUIList.Add (UIOutGameType.kHeroSetting, gameObject.transform.Find ("PlHeroSetting").GetComponent<UIOutHero> ());
		m_outUIList.Add (UIOutGameType.kShop, gameObject.transform.Find ("PlShop").GetComponent<UIOutShop> ());
		//m_outUIList.Add (UIOutGameType.kOption, gameObject.transform.FindChild ("PlStageSelector").GetComponent<UIOutOption> ());		

		//Close(UIOutGameType.kStage);
		//Close(UIOutGameType.kHeroSetting);
		//Close(UIOutGameType.kShop);
		
		m_currentState = UIOutGameType.kStage;
		Open(m_currentState);

		UpdateGold();
		UpdateHP();
		UpdateLevel();
	}

	public void Open(UIOutGameType type)
	{
		Debug.Log("Open : " + type);

		UpdateGold();
		UpdateHP();
		UpdateLevel();

		m_currentState = type;
		m_outUIList [type].Open ();
	}

	public void Close(UIOutGameType type)
	{
		Debug.Log("Close : " + type);
		m_outUIList [type].Close ();
	}

	public void ChangeState(UIOutGameType type)
	{
		Close(m_currentState);
		Open(type);
	}


	public void OnClickBtReset()
	{
		Debug.Log("Reset Start!!");
		SaveLoadManager.Instance.ResetSave();
	}

	public void OnClickBtExti()
	{
		SaveLoadManager.Instance.GameData.userSave.AddEquipment(new EquipElement("W1", EquipType.kWeapone));
		SaveLoadManager.Instance.GameData.userSave.AddEquipment(new EquipElement("A1", EquipType.kArmor));
		SaveLoadManager.Instance.GameData.userSave.AddBlockInvetory(0, "A2");
		SaveLoadManager.Instance.GameData.userSave.AddItem("HP1", 1);
		Application.Quit();
	}

	public void OnClickBtRound(string roundName)
	{
		//Debug.Log("Stage : " + stageName);
		GameManager.Instance.ChangeState (GameState.kPlay, roundName);
	}

	public void UpdateGold()
	{
		m_txGold.text = "Gold : ";
		m_txGold.text += (SaveLoadManager.Instance.GameData.userSave.Gold).ToString();
	}

	public void UpdateHP()
	{
		m_txHP.text = "HP : ";
		m_txHP.text += (SaveLoadManager.Instance.GameData.heroSave.m_CurrentHP).ToString();
		m_txHP.text += "/";
		m_txHP.text += (SaveLoadManager.Instance.GameData.heroSave.m_MaxHP).ToString();
	}

	public void UpdateLevel()
	{
		m_txLevel.text = "LV : ";
		//m_txLevel.text += (SaveLoadManager.Instance.GameData.heroSave.).ToString();
	}
}
