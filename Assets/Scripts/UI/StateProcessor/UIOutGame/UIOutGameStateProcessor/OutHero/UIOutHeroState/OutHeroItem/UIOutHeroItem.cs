using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIOutHeroItem : UIBase
{
	private GameObject m_plItems;
	OutHeroItemInventory m_itemInventories;

	GameObject m_lastObj;

	public override void Initialize ()
	{
		m_plItems = transform.Find("PlItemShow").Find("Items").gameObject;

		m_itemInventories = (OutHeroItemInventory)m_plItems.gameObject.GetComponent(typeof(OutHeroItemInventory));
		m_itemInventories.Init();
		m_itemInventories.ShowInventories();


		m_isInitialized = true;
		m_lastObj = null;
	}

	public override void Open()
	{
		CheckInitialize ();
		Debug.Log("UIOutHeroItem In");
		gameObject.SetActive (true);
		m_itemInventories.Init();
		m_itemInventories.ShowInventories();
		m_lastObj = null;
		
	}

	public override void Close()
	{
		Debug.Log("UIOutHeroItem out");

		GameObject outHero = gameObject.transform.parent.gameObject;
		UIOutHero m_outHeroScript = (UIOutHero)outHero.GetComponent(typeof(UIOutHero));

		if(outHero.activeSelf == true)
		{
			m_outHeroScript.ShowInfo("");
		}

		gameObject.SetActive (false);
	}

	public void CheckItemUse(GameObject checkObj)
	{
		if(checkObj != m_lastObj)
		{
			m_lastObj = checkObj;
		}
		else if(checkObj == m_lastObj)
		{
			m_lastObj = null;
			
			string spriteName = checkObj.transform.Find("ImBlock").GetComponent<Image>().sprite.name;

			SaveLoadManager.Instance.GameData.userSave.UseItem(spriteName);

			m_itemInventories.ShowInventories();
		}

				
	}
}