using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class OutShopShows : MonoBehaviour 
{
	//TODO
	int textSize = 9;
	private List<GameObject> m_shows;
	private bool init = false;

	private int m_nowIndex;

	private int m_currentSelect;
	private int m_lastSelect;

	private UIOutShopType m_currentState;
	private bool m_buy;

	private GameObject m_shop;
	private UIOutShop m_shopScript;

	private string m_fileName;

	public void Init()
	{
		m_nowIndex = 0;
		if(init == false)
		{
			Debug.Log("Init Show");
			m_shows = new List<GameObject>();

			for(int i = 1; ; i++)
			{
				string objName = "ImShopShowCase"+i.ToString();

				if(gameObject.transform.Find(objName) == true)
				{
					m_shows.Add(gameObject.transform.Find(objName).gameObject);
					//Debug.Log(m_shows[i-1].name);
				}
				else if(gameObject.transform.Find(objName) == false)
				{
					break;
				}
				
			}

			m_shop = gameObject.transform.parent.gameObject;
			m_shop = m_shop.transform.parent.gameObject;

			m_shopScript = (UIOutShop)m_shop.transform.GetComponent(typeof(UIOutShop));

			init = true;
		}
			
		m_currentState = UIOutShopType.kInvalid;
		m_buy = false;
		m_currentSelect = -1;
		m_lastSelect = -1;
		m_nowIndex = -1;
		m_fileName = "";
	}

	public void ShowBlocks(UIOutShopType showType, bool buy)
	{
		foreach(var show in m_shows)
		{
			show.transform.Find("Count").GetComponent<Text>().text = "";
			show.SetActive(false);
		}

		m_currentState = showType;
		m_buy = buy;

		if(m_buy == true)
		{
			m_ShowBuyItmes();
		}
		else if(m_buy == false)
		{
			m_ShowSellItmes();
		}
			/*
			for(int i = 0; i < SaveLoadManager.Instance.GameData.userSave.BlockInventorySize; i++)
			{
				m_shows[i].SetActive(true);
				m_shows[i].transform.FindChild("ImBlock").GetComponent<Image>().sprite = null;
			
			}

		
		List<string> blockList = SaveLoadManager.Instance.GameData.userSave.GetBlockInventory(index);
		
		int j = 0;
		foreach(var str in blockList)
		{
			string imgStr = "BlockIcon/" + str;
			m_shows[j].transform.FindChild("ImBlock").GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(imgStr);
			j++;
		}
		 */
		m_lastSelect = -1;
	}

	private void m_ShowBuyItmes()
	{
		string[] dbInfo;

		string readLine;
		string sep = "\t";

		if(m_currentState == UIOutShopType.kBlock)
		{
			TextAsset BlockDB = Resources.Load("DB/Block/BlcokInventoryValue", typeof(TextAsset)) as TextAsset;
			StringReader BlockDBFind = new StringReader(BlockDB.text);

			int index = 0;

			while((readLine = BlockDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[4] == "-100" || dbInfo[0] == "Name")
				{
					continue;
				}

				string imgStr = "BlockIcon/" + dbInfo[1];

				m_shows[index].transform.Find("ImBlock").GetComponent<Image>().sprite  = (Sprite)Resources.Load<Sprite>(imgStr);

				m_shows[index].SetActive(true);

				index++;
			}
		}
		else if(m_currentState == UIOutShopType.kEquip)
		{
			TextAsset EquipDB = Resources.Load("DB/Equip/EquipInventoryValue", typeof(TextAsset)) as TextAsset;
			StringReader EquipDBind = new StringReader(EquipDB.text);

			int index = 0;

			while((readLine = EquipDBind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[4] == "-100" || dbInfo[0] == "Name")
				{
					continue;
				}

				string imgStr = "EquipmentIcon/" + dbInfo[1];

				m_shows[index].transform.Find("ImBlock").GetComponent<Image>().sprite  = (Sprite)Resources.Load<Sprite>(imgStr);

				m_shows[index].SetActive(true);

				index++;
			}
		}
		else if(m_currentState == UIOutShopType.kItem)
		{
			TextAsset ItemDB = Resources.Load("DB/Item/ItemInventoryValue", typeof(TextAsset)) as TextAsset;
			StringReader ItemDBind = new StringReader(ItemDB.text);

			int index = 0;

			while((readLine = ItemDBind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[4] == "-100" || dbInfo[0] == "Name")
				{
					continue;
				}

				string imgStr = "ItemIcon/" + dbInfo[1];

				m_shows[index].transform.Find("ImBlock").GetComponent<Image>().sprite  = (Sprite)Resources.Load<Sprite>(imgStr);

				m_shows[index].SetActive(true);

				index++;
			}
		}
	}

	private void m_ShowSellItmes()
	{
		int j = 0;
		if(m_currentState == UIOutShopType.kBlock)
		{
			for(int i = 0; i < SaveLoadManager.Instance.GameData.userSave.SlotSize(); i++)
			{
				List<string> blockList = SaveLoadManager.Instance.GameData.userSave.GetBlockInventory(i);
		
			
				foreach(var str in blockList)
				{
					string imgStr = "BlockIcon/" + str;
					m_shows[j].transform.Find("ImBlock").GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(imgStr);
					

					m_shows[j].SetActive(true);

					j++;
				}
			}
		}
		else if(m_currentState == UIOutShopType.kEquip)
		{
			List<EquipElement> inventoryList = SaveLoadManager.Instance.GameData.userSave.EquipmentInventory;
		
			foreach(var str in inventoryList)
			{
				string imgStr = "EquipmentIcon/" + str.equipName;
				m_shows[j].transform.Find("ImBlock").GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(imgStr);

				m_shows[j].SetActive(true);
				
				j++;
			}
		}
		else if(m_currentState == UIOutShopType.kItem)
		{
			List<ItemElement> inventoryList = SaveLoadManager.Instance.GameData.userSave.ItemInventory;
		
		
			foreach(var item in inventoryList)
			{
				string itemname = item.name;
			
				string imgStr = "ItemIcon/" + item.name;
				m_shows[j].transform.Find("ImBlock").GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(imgStr);
				m_shows[j].transform.Find("Count").GetComponent<Text>().text = item.count.ToString();
				
				m_shows[j].SetActive(true);

				j++;
			}
		}
	}

	public void ShowThisItemInfo(GameObject obj)
	{
		int price = 0;
		int count = 0;

		if(obj.transform.Find("ImBlock").GetComponent<Image>().sprite == null)
		{
			m_fileName = "";
			return;
		}

		m_fileName = obj.transform.Find("ImBlock").GetComponent<Image>().sprite.name;

		int index = 0;
		foreach(var itemInventory in m_shows)
		{
			if(obj == itemInventory)
			{
				//Debug.Log("index : " + index);
				break;
			}

			index++;
		}


		string[] dbInfo;

		string readLine;
		string sep = "\t";

		string infoStr = "";

		Sprite objSprite = m_shows[index].transform.Find("ImBlock").GetComponent<Image>().sprite;

		if(m_currentState == UIOutShopType.kBlock)
		{
			TextAsset InventoryDB = Resources.Load("DB/Block/BlockInfo", typeof(TextAsset)) as TextAsset;

			StringReader InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[1] == objSprite.name)
				{
					int i = 0;
					foreach(var st in dbInfo)
					{
						if(i == 1)
						{
							i++;
							continue;
						}	
						infoStr += st;
						//Debug.Log(st);
						infoStr += "\n";

						i++;
					}
				
					break;
				}
			}

			InventoryDB = Resources.Load("DB/Block/BlcokInventoryValue", typeof(TextAsset)) as TextAsset;

			InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[1] == objSprite.name)
				{
					if(m_buy == true)
					{
						price = int.Parse(dbInfo[4]);
						m_shopScript.CanageProductName(dbInfo[0], price);
					}
					else if(m_buy == false)
					{
						price = int.Parse(dbInfo[5]);
						m_shopScript.CanageProductName(dbInfo[0], price);
					}

					break;
				}
			}
		}
		else if(m_currentState == UIOutShopType.kEquip)
		{
			TextAsset InventoryDB = Resources.Load("DB/Equip/Equip", typeof(TextAsset)) as TextAsset;

			StringReader InventoryDBFind = new StringReader(InventoryDB.text);

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
							continue;
						}
						else if("0" != dbInfo[i])
						{
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

			InventoryDB = Resources.Load("DB/Equip/EquipInventoryValue", typeof(TextAsset)) as TextAsset;

			InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[1] == objSprite.name)
				{
					if(m_buy == true)
					{
						price = int.Parse(dbInfo[4]);
						m_shopScript.CanageProductName(dbInfo[0], price);
					}
					else if(m_buy == false)
					{
						price = int.Parse(dbInfo[5]);
						m_shopScript.CanageProductName(dbInfo[0], price);
					}

					break;
				}
			}
		}
		else if(m_currentState == UIOutShopType.kItem)
		{
			TextAsset InventoryDB = Resources.Load("DB/Item/ItemInfo", typeof(TextAsset)) as TextAsset;

			StringReader InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[1] == objSprite.name)
				{
					infoStr += dbInfo[0];
					infoStr += "\n";
					infoStr += dbInfo[2];

					break;
				}
			}

			InventoryDB = Resources.Load("DB/Item/ItemInventoryValue", typeof(TextAsset)) as TextAsset;

			InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[1] == objSprite.name)
				{
					if(m_buy == true)
					{
						price = int.Parse(dbInfo[4]);
						m_shopScript.CanageProductName(dbInfo[0], price);
					}
					else if(m_buy == false)
					{
						price = int.Parse(dbInfo[5]);
						List<ItemElement> items = SaveLoadManager.Instance.GameData.userSave.ItemInventory;

						foreach(var item in items)
						{
							if(objSprite.name == item.name)
							{
								count = item.count;
								break;
							}
						}

						m_shopScript.CanageProductName(dbInfo[0], price, count);
					}

					break;
				}
			}
		}

		Debug.Log(infoStr);

		m_shopScript.ShowInfo(infoStr);

		

		


		//m_outHeroScript.ShowInfo(infoStr);

		//m_heroItemScript.CheckItemUse(obj);
	}


	public void OnClickBtOk()
	{

		m_shopScript.ClickOk(m_fileName);


	}

}
