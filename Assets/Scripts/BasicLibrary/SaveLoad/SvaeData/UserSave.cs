using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class UserSave
{
	// TODO change
	int slotIndex = 6;

	public int SlotSize()
	{
		return slotIndex;
	}

	public bool savefileMaked = false;

	private List<string>[] blockInventory;
	public List<string> GetBlockInventory(int index)
	{
		return blockInventory[index];
	}
	public void AddBlockInvetory(int index, string name)
	{
		blockInventory[index].Add(name);
		foreach(var str in blockInventory[index])
		{
			Debug.Log("test : " + str);
		}
		SaveLoadManager.Instance.Save();
	}

	public void RemoveBlockInvetory(int index, string name)
	{
		blockInventory[index].Remove(name);
		foreach(var str in blockInventory[index])
		{
			Debug.Log("test : " + str);
		}
		SaveLoadManager.Instance.Save();
	}

	private int blockInvetorySize;
	public int BlockInventorySize
	{
		get
		{
			return blockInvetorySize;
		}
	}

	private List<ItemElement> itemInventory;
	public List<ItemElement> ItemInventory
	{
		get
		{
			return itemInventory;
		}
	}

	public bool AddItem(List<ItemElement> newItem)
	{
		//if(itemInventory.TryGetValue(newItem.Keys, )

		return false;
	}

	public bool AddItem(string name, int count)
	{
		foreach(var item in itemInventory)
		{
			if(item.name == name)
			{
				item.count += count;
				SaveLoadManager.Instance.Save();
				return true;
			}
		}

		itemInventory.Add(new ItemElement(name, count));
		m_SortingItemInvetory();
		SaveLoadManager.Instance.Save();
		return true;

		/*
		int temp;
		if(itemInventory.TryGetValue(name, out temp) == true)
		{
			itemInventory[name] += count;

			SaveLoadManager.Instance.Save();

			return true;
		}
		else if(itemInventory.TryGetValue(name, out temp) != true)
		{
			itemInventory.Add(name, count);

			SaveLoadManager.Instance.Save();

			return true;
		}
		*/
		
	}

	public void RemoveItem(string name, int count)
	{
		foreach(var item in itemInventory)
		{
			if(item.name == name)
			{
				if(item.count == count)
				{
					itemInventory.Remove(item);
					m_SortingItemInvetory();
				}
				else if(item.count > count)
				{
					item.count -= count;
				}
				break;
			}
		}
		SaveLoadManager.Instance.Save();
	}
	
	public void UseItem(string name)
	{
		foreach(var item in itemInventory)
		{
			if(item.name == name)
			{
				if(item.count == 1)
				{
					itemInventory.Remove(item);
					m_SortingItemInvetory();
				}
				else if(item.count > 1)
				{
					item.count--;
				}
				break;
			}
		}
		SaveLoadManager.Instance.Save();
		/*
		int temp;
		if(itemInventory.TryGetValue(name, out temp) == true)
		{
			if(temp > 1)
			{
				itemInventory[name]--;
			}
			else if(temp == 1)
			{
				itemInventory.Remove(name);
			}

			SaveLoadManager.Instance.Save();
		}
		 */
	}

	private int itemInventorySize;
	private int itemInventoryEachSzie;
	private List<EquipElement> equipmentInventory;

	public List<EquipElement> EquipmentInventory
	{
		get
		{
			return equipmentInventory;
		}
	}
	private int equipmentInventorySize;
	public int EquipmentInventorySize
	{
		get
		{
			return equipmentInventorySize;
		}
	}

	public void AddEquipment(EquipElement equip)
	{
		equipmentInventory.Add(equip);

		m_SortingEquipmentInvetory();	

		SaveLoadManager.Instance.Save();
	}

	public void DeleteEquipment(EquipElement equip)
	{
		int i = 0;
		foreach(var delEquip in equipmentInventory)
		{
			if(equip.equipType == delEquip.equipType && equip.equipName == delEquip.equipName)
			{
				break;
			}
			i++;
		}

		equipmentInventory.RemoveAt(i);
	
		m_SortingEquipmentInvetory();	

		SaveLoadManager.Instance.Save();
	}

	private void m_SortingEquipmentInvetory()
	{
		equipmentInventory.Sort(delegate(EquipElement a, EquipElement b)
		{
			if(a.equipName.CompareTo(b.equipName) == 1)
			{
				return -1;
			}
			else if(a.equipName.CompareTo(b.equipName) == -1)
			{
				return 1;
			}
			return 0;
		});

		equipmentInventory.Sort(delegate(EquipElement a, EquipElement b)
		{
			if(a.equipType < b.equipType)
			{
				return -1;
			}
			else if(a.equipType > b.equipType)
			{
				return 1;
			}
			return 0;
		});



		foreach(var elemnt in equipmentInventory)
		{
			Debug.Log("element type : " + elemnt.equipType);
			Debug.Log("element name : " + elemnt.equipName);
		}
	}

	private void m_SortingItemInvetory()
	{
		itemInventory.Sort(delegate(ItemElement a, ItemElement b)
		{
			if(a.name.CompareTo(b.name) == 1)
			{
				return -1;
			}
			else if(a.name.CompareTo(b.name) == -1)
			{
				return 1;
			}
			return 0;
		});

		itemInventory.Sort(delegate(ItemElement a, ItemElement b)
		{
			if(a.itemType < b.itemType)
			{
				return -1;
			}
			else if(a.itemType > b.itemType)
			{
				return 1;
			}
			return 0;
		});



		foreach(var elemnt in equipmentInventory)
		{
			Debug.Log("element type : " + elemnt.equipType);
			Debug.Log("element name : " + elemnt.equipName);
		}
	}

	private int gold;
	public int Gold
	{
		get
		{
			return gold;
		}
		set
		{
			gold = value;
			SaveLoadManager.Instance.Save();
			Debug.Log(gold);
			UIManager.Instance.UIOutGame.UpdateGold();
		}
	}
	

	public UserSave()
	{
		savefileMaked = false;
		
		blockInventory = new List<string>[slotIndex];
		for(int i = 0; i < slotIndex; i++)
		{
			blockInventory[i] = new List<string>();
		}

		blockInvetorySize = 10;

		itemInventory = new List<ItemElement>();
		itemInventorySize = 10;
		itemInventoryEachSzie = 99;

		equipmentInventory = new List<EquipElement>();
		equipmentInventorySize = 10;

		gold = 10000;

		equipmentInventory.Add(new EquipElement("W1", EquipType.kWeapone));
		equipmentInventory.Add(new EquipElement("A1", EquipType.kArmor));
		blockInventory[0].Add("A2");
		itemInventory.Add(new ItemElement("HP2", 3));

		//AddEquipment(new EquipElement("W1", EquipType.kWeapone));
		//AddEquipment(new EquipElement("A1", EquipType.kArmor));
		//AddBlockInvetory(0, "A2");
		//AddItem("HP2", 3);
	}
}
