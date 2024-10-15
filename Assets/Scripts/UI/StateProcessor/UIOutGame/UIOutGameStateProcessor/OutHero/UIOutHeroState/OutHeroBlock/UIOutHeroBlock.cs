using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIOutHeroBlock : UIBase
{
	GameObject m_blockSetObj;

	GameObject m_blockInvObj;
	OutHeroBlockInventory m_blockInv;
	private List<Image> m_playerBlockSet;

	// TODO chage after
	int blockSize = 4;
	public override void Initialize ()
	{
		Debug.Log("Init hero Block");
		m_blockInvObj = transform.Find("PlBlockShow").Find("Blocks").gameObject;

		m_blockInv = (OutHeroBlockInventory)m_blockInvObj.gameObject.GetComponent(typeof(OutHeroBlockInventory));
		m_blockInv.Init();
		m_blockInv.ShowBlocks(0);

		m_blockSetObj = gameObject.transform.Find("PlBlockSet").gameObject;
		m_playerBlockSet = new List<Image>();
		
		for(int i = 1; i < blockSize + 1; i++)
		{
			string name = "ImBlock" + i.ToString();
			//Debug.Log(name);
			Image addImg = m_blockSetObj.transform.Find(name).GetComponent<Image>();

			m_playerBlockSet.Add(addImg);
		}

		ShowBlockSet();
		

		m_isInitialized = true;
		
	}

	public override void Open()
	{
		CheckInitialize ();
		Debug.Log("UIOutHeroBlock In");
		gameObject.SetActive (true);
		m_blockInv.ShowBlocks(0);
	}

	public override void Close()
	{
		Debug.Log("UIOutHeroBlock out");
		

		GameObject outHero = gameObject.transform.parent.gameObject;
			
		UIOutHero m_outHeroScript = (UIOutHero)outHero.GetComponent(typeof(UIOutHero));

		if(outHero.activeSelf == true)
		{
			m_outHeroScript.ShowInfo("");
		}

		gameObject.SetActive (false);
	}

	public void ShowBlockInventory(int index)
	{
		m_blockInv.ShowBlocks(index);
	}

	public void ShowBlockSet()
	{
		if(SaveLoadManager.Instance.GameData.blockSave.blockSaveInit == true)
		{
			int i = 0;
			foreach(var ima in m_playerBlockSet)
			{
				
				if(SaveLoadManager.Instance.GameData.blockSave.blockName[i] == "")
				{
					break;
				}
				ima.sprite = (Sprite)Resources.Load<Sprite>("BlockIcon/" + SaveLoadManager.Instance.GameData.blockSave.blockName[i]);
				i++;
			}
		}
		else if(SaveLoadManager.Instance.GameData.blockSave.blockSaveInit == false)
		{
			Debug.Log("Block Init value");
			m_playerBlockSet[0].sprite = (Sprite)Resources.Load<Sprite>("BlockIcon/A1");
			SaveLoadManager.Instance.GameData.blockSave.blockName[0] = "A1";

			m_playerBlockSet[1].sprite = (Sprite)Resources.Load<Sprite>("BlockIcon/B1");
			SaveLoadManager.Instance.GameData.blockSave.blockName[1] = "B1";

			m_playerBlockSet[2].sprite = (Sprite)Resources.Load<Sprite>("BlockIcon/C1");
			SaveLoadManager.Instance.GameData.blockSave.blockName[2] = "C1";

			m_playerBlockSet[3].sprite = (Sprite)Resources.Load<Sprite>("BlockIcon/D1");
			SaveLoadManager.Instance.GameData.blockSave.blockName[3] = "D1";

			SaveLoadManager.Instance.GameData.blockSave.blockSaveInit = true;

			//SaveLoadManager.Instance.Save();
		}
	}
}