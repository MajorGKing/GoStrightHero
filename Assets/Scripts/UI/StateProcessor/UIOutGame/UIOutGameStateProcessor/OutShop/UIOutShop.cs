using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class UIOutShop : UIBase
{
	private UIOutShopType m_currentState;
	private bool m_buy;

	private GameObject m_shows;
	private OutShopShows m_showsScript;
	private Text m_infoText;

	private Text m_name;
	private Text m_count;
	private Text m_price;
	private GameObject m_up;
	private GameObject m_down;

	private int m_itemCount;
	private int m_itemCountMax;
	private int m_setPrice;
	private int m_totalPrice;


	private string m_fileName;

	public override void Initialize ()
	{
		m_currentState = UIOutShopType.kBlock;
		m_infoText = gameObject.transform.Find("PlShowInfo").Find("InfoText").GetComponent<Text>();

		m_shows = gameObject.transform.Find("PlShopShow").Find("Shows").gameObject;
		m_showsScript = (OutShopShows)m_shows.transform.GetComponent(typeof(OutShopShows));

		m_showsScript.Init();

		GameObject shopbutton = gameObject.transform.Find("PlShopButtons").gameObject;
		m_name = shopbutton.transform.Find("TxItemName").GetComponent<Text>();
		m_count = shopbutton.transform.Find("TxItemCount").GetComponent<Text>();
		m_price = shopbutton.transform.Find("TxPrice").GetComponent<Text>();

		m_up = shopbutton.transform.Find("BtUp").gameObject;
		m_down = shopbutton.transform.Find("BtDown").gameObject;

		
		m_isInitialized = true;

		

		m_Reset();
		m_OpenShop();
		
	}

	public override void Open()
	{
		CheckInitialize ();
		m_Reset();
		m_currentState = UIOutShopType.kBlock;
		m_OpenShop();
		Debug.Log("Shop In");
		gameObject.SetActive (true);
	}

	public override void Close()
	{
		Debug.Log("Shop out");
		m_Reset();
		gameObject.SetActive (false);
	}

	private void m_Reset()
	{
		m_buy = true;
		m_infoText.text = "";
		m_name.text = "";
		m_count.text = "";
		m_price.text = "";

		m_itemCount = 0;
		m_itemCountMax = 0;
		m_setPrice = 0;
		m_totalPrice = 0;

		m_fileName = "";

		m_up.SetActive(false);
		m_down.SetActive(false);
	}

	private void m_OpenShop()
	{
		//Debug.Log("Open : " + m_currentState + " " + m_buy);
		m_showsScript.ShowBlocks(m_currentState, m_buy);
	}


	public void ShowInfo(string infoStr)
	{
		m_infoText.text = infoStr;
	}

	public void CanageProductName(string name, int price)
	{
		m_name.text = name;
		m_price.text = price.ToString();
		m_setPrice = price;
		m_itemCountMax = 99;
		m_itemCount = 1;

		m_count.text = m_itemCount.ToString();
		m_totalPrice = m_setPrice * m_itemCount;
		m_price.text = m_totalPrice.ToString();
	}

	public void CanageProductName(string name, int price, int maxCount)
	{
		m_name.text = name;
		m_itemCountMax = maxCount;
		m_price.text = price.ToString();
		m_setPrice = price;
		m_itemCount = 1;

		m_count.text = m_itemCount.ToString();
		m_totalPrice = m_setPrice * m_itemCount;
		m_price.text = m_totalPrice.ToString();
	}



	public void OnClickBtShopBlock()
	{
		m_Reset();
		m_currentState = UIOutShopType.kBlock;
		m_OpenShop();

	}

	public void OnClickBtShopEquip()
	{
		m_Reset();
		m_currentState = UIOutShopType.kEquip;
		m_OpenShop();
	}

	public void OnClickBtShopItem()
	{
		m_Reset();
		m_currentState = UIOutShopType.kItem;
		//m_count.text = "0";
		m_up.SetActive(true);
		m_down.SetActive(true);
		m_OpenShop();
	}

	public void OnClickBtBuy()
	{
		m_Reset();
		if(m_currentState == UIOutShopType.kItem)
		{
			m_up.SetActive(true);
			m_down.SetActive(true);
		}
		m_OpenShop();
	}
	
	public void OnClickBtSell()
	{
		m_Reset();
		if(m_currentState == UIOutShopType.kItem)
		{
			m_up.SetActive(true);
			m_down.SetActive(true);
		}
		m_buy = false;
		m_OpenShop();
	}

	public void OnClickBtUp()
	{
		if(m_itemCount < m_itemCountMax)
		{
			m_itemCount++;
			m_count.text = m_itemCount.ToString();

			m_totalPrice = m_setPrice * m_itemCount;

			m_price.text = m_totalPrice.ToString();
		}
	}

	public void OnClickBtDown()
	{
		if(m_itemCount > 0)
		{
			m_itemCount--;
			m_count.text = m_itemCount.ToString();

			m_totalPrice = m_setPrice * m_itemCount;

			m_price.text = m_totalPrice.ToString();
		}
	}

	public int ClickOk(string filename)
	{
		if(m_name.text == "")
		{
			return -1;
		}

		m_fileName = filename;
		int returnValue = -1;

		if(m_buy == true)
		{
			returnValue = m_BuyProcess();
		}
		else if(m_buy == false)
		{
			returnValue = m_SellProcess();
		}

		UIOutShopType tempState = m_currentState;
		bool tempBuy = m_buy;

		m_Reset();
		m_currentState = tempState;
		m_buy = tempBuy;
		m_OpenShop();


		return returnValue;
	}

	private int m_BuyProcess()
	{
		// Step1. Check gold
		if(m_totalPrice > SaveLoadManager.Instance.GameData.userSave.Gold)
		{
			return 1;
		}
		// Step2. Check Inventory space
		string[] dbInfo;

		string readLine;
		string sep = "\t";

		if(m_currentState == UIOutShopType.kBlock)
		{
			int slotNum = -1;

			TextAsset InventoryDB = Resources.Load("DB/Block/BlockAbilityValue", typeof(TextAsset)) as TextAsset;

			StringReader InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[0] == m_fileName)
				{
					slotNum = int.Parse(dbInfo[1]);
				
					break;
				}
			}

			if(slotNum == -1)
			{
				return 2;
			}

			SaveLoadManager.Instance.GameData.userSave.Gold = SaveLoadManager.Instance.GameData.userSave.Gold - m_totalPrice;
			SaveLoadManager.Instance.GameData.userSave.AddBlockInvetory(slotNum -1, m_fileName);
		}
		else if(m_currentState == UIOutShopType.kEquip)
		{
			int type = -1;

			TextAsset InventoryDB = Resources.Load("DB/Equip/EquipInventoryValue", typeof(TextAsset)) as TextAsset;

			StringReader InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[1] == m_fileName)
				{
					type = int.Parse(dbInfo[2]);
					break;
				}
			}

			if(type == -1)
			{
				return 2;
			}

			SaveLoadManager.Instance.GameData.userSave.Gold = SaveLoadManager.Instance.GameData.userSave.Gold - m_totalPrice;
			SaveLoadManager.Instance.GameData.userSave.AddEquipment(new EquipElement(m_fileName,(EquipType)type));
		}
		else if(m_currentState == UIOutShopType.kItem)
		{
			SaveLoadManager.Instance.GameData.userSave.Gold = SaveLoadManager.Instance.GameData.userSave.Gold - m_totalPrice;
			SaveLoadManager.Instance.GameData.userSave.AddItem(m_fileName, m_itemCount);
		}


		return 0;
	}

	private int m_SellProcess()
	{
		string[] dbInfo;

		string readLine;
		string sep = "\t";

		if(m_currentState == UIOutShopType.kBlock)
		{
			int slotNum = -1;

			TextAsset InventoryDB = Resources.Load("DB/Block/BlockAbilityValue", typeof(TextAsset)) as TextAsset;

			StringReader InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[0] == m_fileName)
				{
					slotNum = int.Parse(dbInfo[1]);
				
					break;
				}
			}

			if(slotNum == -1)
			{
				return 2;
			}

			SaveLoadManager.Instance.GameData.userSave.Gold = SaveLoadManager.Instance.GameData.userSave.Gold + m_totalPrice;
			SaveLoadManager.Instance.GameData.userSave.RemoveBlockInvetory(slotNum -1, m_fileName);
		}
		else if(m_currentState == UIOutShopType.kEquip)
		{
			int type = -1;

			TextAsset InventoryDB = Resources.Load("DB/Equip/EquipInventoryValue", typeof(TextAsset)) as TextAsset;

			StringReader InventoryDBFind = new StringReader(InventoryDB.text);

			while((readLine = InventoryDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				if(dbInfo[1] == m_fileName)
				{
					type = int.Parse(dbInfo[2]);
					break;
				}
			}

			if(type == -1)
			{
				return 2;
			}

			SaveLoadManager.Instance.GameData.userSave.Gold = SaveLoadManager.Instance.GameData.userSave.Gold + m_totalPrice;
			SaveLoadManager.Instance.GameData.userSave.DeleteEquipment(new EquipElement(m_fileName,(EquipType)type));
		}
		else if(m_currentState == UIOutShopType.kItem)
		{
			SaveLoadManager.Instance.GameData.userSave.Gold = SaveLoadManager.Instance.GameData.userSave.Gold + m_totalPrice;
			SaveLoadManager.Instance.GameData.userSave.RemoveItem(m_fileName, m_itemCount);
		}



		return 0;
	}
}
