using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class OutHeroItemInventory : MonoBehaviour 
{
	GameObject m_heroSetting;
	UIOutHero m_outHeroScript;

	GameObject m_heroItem;
	UIOutHeroItem m_heroItemScript;
	private bool init = false;

	private List<GameObject> m_items;
	public void Init()
	{
		if(init == false)
		{
			Debug.Log("Init Item");
			m_items = new List<GameObject>();

			for(int i = 1; ; i++)
			{
				string objName = "ImItemCase"+i.ToString();

				if(gameObject.transform.Find(objName) == true)
				{
					m_items.Add(gameObject.transform.Find(objName).gameObject);
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

			m_heroItem = gameObject.transform.parent.gameObject;
			m_heroItem = m_heroItem.transform.parent.gameObject;

			m_heroItemScript = (UIOutHeroItem)m_heroItem.gameObject.GetComponent(typeof(UIOutHeroItem));

			init = true;
		}
	}

	public void ShowInventories()
	{
		foreach(var block in m_items)
		{
			block.SetActive(false);
		}
		for(int i = 0; i < SaveLoadManager.Instance.GameData.userSave.EquipmentInventorySize; i++)
		{
			m_items[i].SetActive(true);
			m_items[i].transform.Find("ImBlock").GetComponent<Image>().sprite = null;
			m_items[i].transform.Find("Count").GetComponent<Text>().text = null;
		}

		List<ItemElement> inventoryList = SaveLoadManager.Instance.GameData.userSave.ItemInventory;
		
		int j = 0;
		foreach(var item in inventoryList)
		{
			string itemname = item.name;
			Debug.Log(itemname + " : " + item.count);
			
			string imgStr = "ItemIcon/" + item.name;
			m_items[j].transform.Find("ImBlock").GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(imgStr);
			m_items[j].transform.Find("Count").GetComponent<Text>().text = item.count.ToString();
			j++;
		}
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ShowThisItemInfo(GameObject obj)
	{
		if(obj.transform.Find("ImBlock").GetComponent<Image>().sprite == null)
		{
			return;
		}

		int index = 0;
		foreach(var itemInventory in m_items)
		{
			if(obj == itemInventory)
			{
				Debug.Log("index : " + index);
				break;
			}

			index++;
		}

		TextAsset InventoryDB = Resources.Load("DB/Item/ItemInfo", typeof(TextAsset)) as TextAsset;

		string[] dbInfo;

		string readLine;
		string sep = "\t";

		string infoStr = "";

		StringReader InventoryDBFind = new StringReader(InventoryDB.text);

		Sprite objSprite = m_items[index].transform.Find("ImBlock").GetComponent<Image>().sprite;

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
		
		Debug.Log(infoStr);
		m_outHeroScript.ShowInfo(infoStr);

		m_heroItemScript.CheckItemUse(obj);
	}
}
