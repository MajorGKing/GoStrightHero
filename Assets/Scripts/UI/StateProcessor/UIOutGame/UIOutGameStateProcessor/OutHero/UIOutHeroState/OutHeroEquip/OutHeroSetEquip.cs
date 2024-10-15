using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class OutHeroSetEquip : MonoBehaviour 
{
	//TODO
	int textSize = 9;

	GameObject m_heroSetting;
	UIOutHero m_outHeroScript;

	GameObject m_heroEquip;
	UIOutHeroEquip m_heroEquipScript;

	void Start ()
	{
		m_heroSetting = gameObject.transform.parent.gameObject;
		m_heroSetting = m_heroSetting.transform.parent.gameObject;
		m_heroSetting = m_heroSetting.transform.parent.gameObject;

		m_outHeroScript = (UIOutHero)m_heroSetting.gameObject.GetComponent(typeof(UIOutHero));

		m_heroEquip = gameObject.transform.parent.gameObject;
		m_heroEquip = m_heroEquip.transform.parent.gameObject;

		m_heroEquipScript = (UIOutHeroEquip)m_heroEquip.gameObject.GetComponent(typeof(UIOutHeroEquip));
	}
	public void ShowInfo()
	{
		Sprite objSprite = gameObject.GetComponent<Image>().sprite;

		if(objSprite == null)
		{
			return;
		}

		TextAsset InventoryDB = Resources.Load("DB/Equip/Equip", typeof(TextAsset)) as TextAsset;

		string[] dbInfo;

		string readLine;
		string sep = "\t";

		string infoStr = "";

		StringReader InventoryDBFind = new StringReader(InventoryDB.text);

		while((readLine = InventoryDBFind.ReadLine()) != null)
		{
			dbInfo = readLine.Split(sep.ToCharArray());

			//Debug.Log("objSprite.name" + objSprite.name);
			//Debug.Log("dbInfo[0]" + dbInfo[0]);
			
			if(dbInfo[1] == objSprite.name)
			{
				/*
				foreach(var st in dbInfo)
				{
					infoStr += st;
					//Debug.Log(st);
					infoStr += "\n";


				}
				 */

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

		m_heroEquipScript.CheckEquipChange(gameObject);
	}


}
