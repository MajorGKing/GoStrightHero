using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class UIOutHeroEquip : UIBase
{
	private List<Image> m_playerEquipSet;
	private int m_kindOfEquip = 4;
	private int m_kindofStatus = 6;

	private GameObject m_equipSet;

	private GameObject m_plCompare;
	private Text[] m_compareText;

	private GameObject m_plEquips;
	OutHeroEquipInventory m_equipInventories;

	GameObject m_lastObj;

	public override void Initialize ()
	{
		Debug.Log("equip menu init");
		m_playerEquipSet = new List<Image>();

		GameObject m_equipSet = gameObject.transform.Find("PlEquipSet").gameObject;

		string name = "Weapone";
		Image addImg = m_equipSet.transform.Find(name).GetComponent<Image>();
		m_playerEquipSet.Add(addImg);

		name = "Armor";
		addImg = m_equipSet.transform.Find(name).GetComponent<Image>();
		m_playerEquipSet.Add(addImg);

		name = "SubArmor";
		addImg = m_equipSet.transform.Find(name).GetComponent<Image>();
		m_playerEquipSet.Add(addImg);

		name = "Accessory";
		addImg = m_equipSet.transform.Find(name).GetComponent<Image>();
		m_playerEquipSet.Add(addImg);


		m_plCompare = gameObject.transform.Find("PlEquipCompare").gameObject;
		
		m_compareText = new Text[m_kindofStatus];

		for(int i = 0; i < m_kindofStatus; i++)
		{
			string strName = "CompareText";
			strName += (i+1).ToString();

			m_compareText[i] = m_plCompare.transform.Find(strName).GetComponent<Text>();
		}


		m_plEquips = transform.Find("PlEquipShow").Find("Equips").gameObject;

		m_equipInventories = (OutHeroEquipInventory)m_plEquips.gameObject.GetComponent(typeof(OutHeroEquipInventory));
			

		m_isInitialized = true;


		
		//m_ShowEquipSet();


	}

	public override void Open()
	{
		CheckInitialize ();
		Debug.Log("UIOutHeroEquip In");
		gameObject.SetActive (true);
		m_ShowEquipSet();
		m_equipInventories.Init();
		m_equipInventories.ShowInventories();
		m_lastObj = null;

		//m_ShowEquipSet();
		//ShowEquipInventory();
	}

	public override void Close()
	{
		Debug.Log("UIOutHeroEquip out");
		GameObject outHero = gameObject.transform.parent.gameObject;
			
		UIOutHero m_outHeroScript = (UIOutHero)outHero.GetComponent(typeof(UIOutHero));

		if(outHero.activeSelf == true)
		{
			m_outHeroScript.ShowInfo("");
		}

		gameObject.SetActive (false);
	}


	private void m_ShowEquipSet()
	{
		if(SaveLoadManager.Instance.GameData.equipSave.equipSaveInit == true)
		{
			int i = 0;
			foreach(var ima in m_playerEquipSet)
			{
				
				if(SaveLoadManager.Instance.GameData.equipSave.GetEquipElement((EquipType)i).equipType == EquipType.kNull)
				{
					ima.sprite = null;
				}
				else if((SaveLoadManager.Instance.GameData.equipSave.GetEquipElement((EquipType)i).equipType != EquipType.kNull))
				{
					ima.sprite = (Sprite)Resources.Load<Sprite>("EquipmentIcon/" + SaveLoadManager.Instance.GameData.equipSave.GetEquipElement((EquipType)i).equipName);
				}
				i++;
			}
		}
		else if(SaveLoadManager.Instance.GameData.equipSave.equipSaveInit == false)
		{
			Debug.Log("Equip Init value");
			m_playerEquipSet[0].sprite = (Sprite)Resources.Load<Sprite>("EquipmentIcon/W1");
			SaveLoadManager.Instance.GameData.equipSave.SetEquip(EquipType.kWeapone, "W1");

			m_playerEquipSet[1].sprite = (Sprite)Resources.Load<Sprite>("EquipmentIcon/A1");
			SaveLoadManager.Instance.GameData.equipSave.SetEquip(EquipType.kArmor, "A1");

			m_playerEquipSet[2].sprite = (Sprite)Resources.Load<Sprite>("EquipmentIcon/SA1");
			SaveLoadManager.Instance.GameData.equipSave.SetEquip(EquipType.kSubArmor, "SA1");

			m_playerEquipSet[3].sprite = (Sprite)Resources.Load<Sprite>("EquipmentIcon/AC1");
			SaveLoadManager.Instance.GameData.equipSave.SetEquip(EquipType.kAccessory, "AC1");

			SaveLoadManager.Instance.GameData.equipSave.equipSaveInit = true;

			//SaveLoadManager.Instance.Save();
		}
	}

	public void ShowStatus(EquipInfoType comeIndex, int value)
	{
		int index = (int)comeIndex - 3;

		string str = "";

		if(comeIndex == EquipInfoType.kHP)
		{
			str += "HP : ";
			str += value.ToString();
		}
		else if(comeIndex == EquipInfoType.kArmor)
		{
			str += "Armor : ";
			str += value.ToString();
		}
		else if(comeIndex == EquipInfoType.kAvoid)
		{
			str += "Avoid : ";
			str += value.ToString();
		}
		else if(comeIndex == EquipInfoType.kPow)
		{
			str += "Pow : ";
			str += value.ToString();
		}
		else if(comeIndex == EquipInfoType.kASucess)
		{
			str += "Attack Sucess : ";
			str += value.ToString();
		}
		else if(comeIndex == EquipInfoType.KASpeed)
		{
			str += "Attack Speed : ";
			str += value.ToString();
		}

		if(value < 0)
		{
			m_compareText[index].color = Color.red;
		}
		else if(value == 0)
		{
			m_compareText[index].color = Color.black;
		}
		else if(value > 0)
		{
			m_compareText[index].color = Color.blue;
		}

		m_compareText[index].text = str;
	}

	public void CheckEquipChange(GameObject checkObj)
	{
		if(checkObj != m_lastObj)
		{
			m_lastObj = checkObj;
		}
		else if(checkObj == m_lastObj)
		{
			m_lastObj = null;

			if(checkObj.tag == "ShowEquip")
			{
				if("Weapone" == checkObj.name)
				{
					EquipElement temp = new EquipElement();
					//temp.equipName = SaveLoadManager.Instance.GameData.equipSave.GetEquipElement(EquipType.kWeapone).equipName;
					//temp.equipType = SaveLoadManager.Instance.GameData.equipSave.GetEquipElement(EquipType.kWeapone).equipType;
					temp = SaveLoadManager.Instance.GameData.equipSave.GetEquipElement(EquipType.kWeapone);
					SaveLoadManager.Instance.GameData.equipSave.SetUnEquip(EquipType.kWeapone);
					SaveLoadManager.Instance.GameData.userSave.AddEquipment(temp);
				}
				else if("Armor" == checkObj.name)
				{
					EquipElement temp = new EquipElement();
					temp = SaveLoadManager.Instance.GameData.equipSave.GetEquipElement(EquipType.kArmor);
					SaveLoadManager.Instance.GameData.equipSave.SetUnEquip(EquipType.kArmor);
					SaveLoadManager.Instance.GameData.userSave.AddEquipment(temp);
				}
				else if("SubArmor" == checkObj.name)
				{
					EquipElement temp = new EquipElement();
					temp = SaveLoadManager.Instance.GameData.equipSave.GetEquipElement(EquipType.kSubArmor);
					SaveLoadManager.Instance.GameData.equipSave.SetUnEquip(EquipType.kSubArmor);
					SaveLoadManager.Instance.GameData.userSave.AddEquipment(temp);
				}
				else if("Accessory" == checkObj.name)
				{
					EquipElement temp = new EquipElement();
					temp = SaveLoadManager.Instance.GameData.equipSave.GetEquipElement(EquipType.kAccessory);
					SaveLoadManager.Instance.GameData.equipSave.SetUnEquip(EquipType.kAccessory);
					SaveLoadManager.Instance.GameData.userSave.AddEquipment(temp);
				}
			}
			else if(checkObj.tag == "EquipInventory")
			{
				Debug.Log("Samve Inv");
				string[] dbInfo;

				string readLine;
				string sep = "\t";

				TextAsset InventoryDB = Resources.Load("DB/Equip/Equip", typeof(TextAsset)) as TextAsset;

				StringReader InventoryDBFind = new StringReader(InventoryDB.text);

				Sprite objSprite = checkObj.transform.Find("ImBlock").GetComponent<Image>().sprite;

				while((readLine = InventoryDBFind.ReadLine()) != null)
				{
					dbInfo = readLine.Split(sep.ToCharArray());

					if(dbInfo[(int)EquipInfoType.kFileName] == objSprite.name)
					{
						EquipType thisType = (EquipType)int.Parse(dbInfo[(int)EquipInfoType.kType]);
						string str = objSprite.name;

						EquipElement temp = new EquipElement();
						temp = SaveLoadManager.Instance.GameData.equipSave.GetEquipElement(thisType);

						if(temp.equipType != EquipType.kNull)
						{
							SaveLoadManager.Instance.GameData.equipSave.SetUnEquip(thisType);
							SaveLoadManager.Instance.GameData.userSave.AddEquipment(temp);
						}

						SaveLoadManager.Instance.GameData.userSave.DeleteEquipment(new EquipElement(str, thisType));
						SaveLoadManager.Instance.GameData.equipSave.SetEquip(new EquipElement(str, thisType));
						

						break;
					}
				}
			}

			m_ShowEquipSet();
			m_equipInventories.ShowInventories();
		}

	}
}