using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class OutHeroSetBlock : MonoBehaviour 
{
	GameObject m_heroSetting;
	UIOutHero m_outHeroScript;

	GameObject m_settingBlock;
	UIOutHeroBlock m_settingBlockScript;
	// Use this for initialization
	void Start ()
	{
		m_heroSetting = gameObject.transform.parent.gameObject;
		m_heroSetting = m_heroSetting.transform.parent.gameObject;
		m_heroSetting = m_heroSetting.transform.parent.gameObject;

		m_settingBlock = gameObject.transform.parent.gameObject;
		m_settingBlock = m_settingBlock.transform.parent.gameObject;

		m_outHeroScript = (UIOutHero)m_heroSetting.gameObject.GetComponent(typeof(UIOutHero));
		m_settingBlockScript = (UIOutHeroBlock)m_settingBlock.gameObject.GetComponent(typeof(UIOutHeroBlock));
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ShowInfo()
	{
		Sprite objSprite = gameObject.GetComponent<Image>().sprite;

		TextAsset BlockDB = Resources.Load("DB/Block/BlockInfo", typeof(TextAsset)) as TextAsset;

		string[] dbInfo;

		string readLine;
		string sep = "\t";

		string infoStr = "";

		StringReader BlockDBFind = new StringReader(BlockDB.text);
			
		while((readLine = BlockDBFind.ReadLine()) != null)
		{
			dbInfo = readLine.Split(sep.ToCharArray());

			//Debug.Log("objSprite.name" + objSprite.name);
			//Debug.Log("dbInfo[0]" + dbInfo[0]);
			
			if(dbInfo[1] == objSprite.name)
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
		Debug.Log(infoStr);
		m_outHeroScript.ShowInfo(infoStr);

		if(gameObject.name == "ImBlock1")
		{
			m_settingBlockScript.ShowBlockInventory(0);
		}
		else if(gameObject.name == "ImBlock2")
		{
			m_settingBlockScript.ShowBlockInventory(1);
		}
		else if(gameObject.name == "ImBlock3")
		{
			m_settingBlockScript.ShowBlockInventory(2);
		}
		else if(gameObject.name == "ImBlock4")
		{
			m_settingBlockScript.ShowBlockInventory(3);
		}
		else if(gameObject.name == "ImBlock5")
		{
			m_settingBlockScript.ShowBlockInventory(4);
		}
		else if(gameObject.name == "ImBlock6")
		{
			m_settingBlockScript.ShowBlockInventory(5);
		}
	}
}
