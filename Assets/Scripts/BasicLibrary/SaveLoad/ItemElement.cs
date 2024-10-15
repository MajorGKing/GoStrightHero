using UnityEngine;
using System.IO;

[System.Serializable]



public class ItemElement
{
	public int count = 0;
	public string name = "";

	public ItemType itemType = ItemType.kNull;

	public ItemElement()
	{

	}

	public ItemElement(string newName, int newCount, ItemType type)
	{
		name = newName;
		count = newCount;
		itemType = type;
	}

	public ItemElement(string newName, int newCount)
	{
		name = newName;
		count = newCount;
		
		TextAsset InventoryDB = Resources.Load("DB/Item/Item", typeof(TextAsset)) as TextAsset;

		string[] dbInfo;

		string readLine;
		string sep = "\t";

		StringReader InventoryDBFind = new StringReader(InventoryDB.text);

		while((readLine = InventoryDBFind.ReadLine()) != null)
		{
			dbInfo = readLine.Split(sep.ToCharArray());

			if(dbInfo[0] == newName)
			{
				int type = int.Parse(dbInfo[2]);
				itemType = (ItemType)type;

				break;
			}
		}
	}

}
