using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class OutHeroBlockInventory : MonoBehaviour 
{
	private List<GameObject> m_inventories;
	private bool init = false;

	private int m_nowIndex;

	private int m_currentSelect;
	private int m_lastSelect;

	public void Init()
	{
		m_nowIndex = 0;
		if(init == false)
		{
			Debug.Log("Init Inv");
			m_inventories = new List<GameObject>();

			for(int i = 1; ; i++)
			{
				string objName = "ImBlockCase"+i.ToString();

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

			m_currentSelect = -1;
			m_lastSelect = -1;
			m_nowIndex = -1;

			init = true;
		}
	}

	public void ShowBlocks(int index)
	{
		if(m_nowIndex != index)
		{
			foreach(var block in m_inventories)
			{
				block.SetActive(false);
			}
			for(int i = 0; i < SaveLoadManager.Instance.GameData.userSave.BlockInventorySize; i++)
			{
				m_inventories[i].SetActive(true);
				m_inventories[i].transform.Find("ImBlock").GetComponent<Image>().sprite = null;
			
			}
		}

		List<string> blockList = SaveLoadManager.Instance.GameData.userSave.GetBlockInventory(index);
		
		int j = 0;
		foreach(var str in blockList)
		{
			string imgStr = "BlockIcon/" + str;
			m_inventories[j].transform.Find("ImBlock").GetComponent<Image>().sprite = (Sprite)Resources.Load<Sprite>(imgStr);
			j++;
		}

		m_nowIndex = index;
		
		m_lastSelect = -1;
	}
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ShowThisBlockInfo(GameObject block)
	{
		int i = 0;
		foreach(var listBlock in m_inventories)
		{
			if(block == listBlock)
			{
				Debug.Log("i : " + i);
				break;
			}

			i++;
		}

		m_currentSelect = i;

		TextAsset BlockDB = Resources.Load("DB/Block/BlockInfo", typeof(TextAsset)) as TextAsset;

		string[] dbInfo;

		string readLine;
		string sep = "\t";

		string infoStr = "";

		StringReader BlockDBFind = new StringReader(BlockDB.text);


		List<string> nowBlocks = SaveLoadManager.Instance.GameData.userSave.GetBlockInventory(m_nowIndex);
			
		while((readLine = BlockDBFind.ReadLine()) != null)
		{
			dbInfo = readLine.Split(sep.ToCharArray());

			//Debug.Log("objSprite.name" + objSprite.name);
			//Debug.Log("dbInfo[0]" + dbInfo[0]);

			if(i >= nowBlocks.Count)
			{
				break;
			}
			
			if(dbInfo[1] == nowBlocks[i])
			{
				int k = 0;	
				foreach(var st in dbInfo)
				{
					if(k == 1)
					{
						k++;
						continue;
					}

					infoStr += st;
					//Debug.Log(st);
					infoStr += "\n";

					k++;
				}
				
				break;
			}
		}


		if("" != infoStr)
		{
			GameObject outHero = gameObject.transform.parent.gameObject;
			
			outHero = outHero.transform.parent.gameObject;

			UIOutHeroBlock m_outHeroBlockScript = (UIOutHeroBlock)outHero.GetComponent(typeof(UIOutHeroBlock));

			outHero = outHero.transform.parent.gameObject;
			
			UIOutHero m_outHeroScript = (UIOutHero)outHero.GetComponent(typeof(UIOutHero));

			m_outHeroScript.ShowInfo(infoStr);

			if(m_lastSelect == m_currentSelect)
			{
				string temp = "";

				temp = nowBlocks[i];
				nowBlocks[i] = SaveLoadManager.Instance.GameData.blockSave.blockName[m_nowIndex];

				SaveLoadManager.Instance.GameData.blockSave.blockName[m_nowIndex] = temp;

				m_outHeroScript.ShowInfo("");

				m_lastSelect = -1;

				ShowBlocks(m_nowIndex);

				m_outHeroBlockScript.ShowBlockSet();

				SaveLoadManager.Instance.Save();

			}
			else if(m_lastSelect != m_currentSelect)
			{
				m_lastSelect = m_currentSelect;
			}
		}
	}
}
