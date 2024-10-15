using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class OutHeroEquipInventory : MonoBehaviour 
{
	//TODO
	int textSize = 9;
	private List<GameObject> m_inventories;

	GameObject m_heroSetting;
	UIOutHero m_outHeroScript;

	GameObject m_heroEquip;
	UIOutHeroEquip m_heroEquipScript;


	private bool init = false;
	public void Init()
	{
		if(init == false)
		{
			Debug.Log("Init Inv");
			m_inventories = new List<GameObject>();

			for(int i = 1; ; i++)
			{
				string objName = "ImEquipCase"+i.ToString();

				if(gameObject.transform.Find(objName) == true)
				{
					m_inventories.Add(gameObject.transform.Find(objName).gameObject);
					//Debug.Log(m_inventories[i-1].name);
				}
				else if(gameObject.transform.Find(objName) == false)
				{
					break;
				}
				
			}

			m_heroSetting = gameObject.transform.parent.gameObject;
			m_heroSetting = m_heroSetting.transform.parent.gameObject;
			m_heroSetting = m_heroSetting.transform.parent.gameObject;

			m_outHeroScript = (UIOutHero)m_heroSetting.gameObject.GetComponent(typeof(UIOutHero));

			m_heroEquip = gameObject.transform.parent.gameObject;
			m_heroEquip = m_heroEquip.transform.parent.gameObject;

			m_heroEquipScript = (UIOutHeroEquip)m_heroEquip.gameObject.GetComponent(typeof(UIOutHeroEquip));

			init = true;
		}

	}

	public void ShowInventories()
	{
		foreach(var block in m_inventories)
		{
			block.SetActive(false);
		}
		for(int i = 0; i < SaveLoadManager.Instance.GameData.userSave.EquipmentInventorySize; i++)
		{
			m_inventories[i].SetActive(true);
			m_inventories[i].transform.Find("ImBlock").GetComponent<Image>().sprite = null;
		}

		List<EquipElement> inventoryList = SaveLoadManager.Instance.GameData.userSave.EquipmentInventory;
		
		int j = 0;
		foreach(var str in inventoryList)
		{
			string imgStr = "EquipmentIcon/" + str.equipName;
			m_inventories[j].transform.Find("ImBlock").GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(imgStr);
			j++;
		}
	}

	public void ShowThisEquipInfo(GameObject obj)
	{
		if(obj.transform.Find("ImBlock").GetComponent<Image>().sprite == null)
		{
			return;
		}


		int index = 0;
		foreach(var listInventory in m_inventories)
		{
			if(obj == listInventory)
			{
				Debug.Log("index : " + index);
				break;
			}

			index++;
		}

		TextAsset InventoryDB = Resources.Load("DB/Equip/Equip", typeof(TextAsset)) as TextAsset;

		string[] dbInfo;

		string readLine;
		string sep = "\t";

		string infoStr = "";

		StringReader InventoryDBFind = new StringReader(InventoryDB.text);

		Sprite objSprite = m_inventories[index].transform.Find("ImBlock").GetComponent<Image>().sprite;

		while((readLine = InventoryDBFind.ReadLine()) != null)
		{
			dbInfo = readLine.Split(sep.ToCharArray());

			//Debug.Log("objSprite.name" + objSprite.name);
			//Debug.Log("dbInfo[0]" + dbInfo[0]);
			
			if(dbInfo[1] == objSprite.name)
			{

				for(int i = 0; i < textSize; i++)
				 {
					if(i == (int)EquipInfoType.kName)
					{
						infoStr += dbInfo[0];
					
						infoStr += "\n";
						continue;
					}
					else if(i == (int)EquipInfoType.kFileName)
					{
						continue;
					}
					else if(i == (int)EquipInfoType.kType)
					{
						if("0" == dbInfo[i])
						{
							infoStr += "Weapone";
					
							infoStr += "\n";
						}
						else if("1" == dbInfo[i])
						{
							infoStr += "Armor";
					
							infoStr += "\n";
						}
						else if("2" == dbInfo[i])
						{
							infoStr += "Sub Armor";
					
							infoStr += "\n";
						}
						else if("3" == dbInfo[i])
						{
							infoStr += "Accessory";
					
							infoStr += "\n";
						}
						continue;
					}

					if("0" == dbInfo[i])
					{
						m_heroEquipScript.ShowStatus((EquipInfoType)i, 0);
						continue;
					}
					else if("0" != dbInfo[i])
					{
						m_heroEquipScript.ShowStatus((EquipInfoType)i, int.Parse(dbInfo[i]));
						if(i == (int)EquipInfoType.kHP)
						{
							infoStr += "HP : ";
							infoStr += dbInfo[i];
						}
						else if(i == (int)EquipInfoType.kArmor)
						{
							infoStr += "Armor : ";
							infoStr += dbInfo[i];
						}
						else if(i == (int)EquipInfoType.kAvoid)
						{
							infoStr += "Avoid : ";
							infoStr += dbInfo[i];
						}
						else if(i == (int)EquipInfoType.kPow)
						{
							infoStr += "Pow : ";
							infoStr += dbInfo[i];
						}
						else if(i == (int)EquipInfoType.kASucess)
						{
							infoStr += "Attack Sucess : ";
							infoStr += dbInfo[i];
						}
						else if(i == (int)EquipInfoType.KASpeed)
						{
							infoStr += "Attack Speed : ";
							infoStr += dbInfo[i];
						}
						infoStr += "\n";
					}
				 }
				
				break;
			}
		}
		Debug.Log(infoStr);
		m_outHeroScript.ShowInfo(infoStr);

		m_heroEquipScript.CheckEquipChange(obj);
	}
}
