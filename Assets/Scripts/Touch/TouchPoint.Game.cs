using UnityEngine;
using System.Collections;

public partial class TouchPoint : TMonoSingleton<TouchPoint> 
{
	void OnTriggerEnter2D(Collider2D other)
    {
		if(other.tag == "Block")
		{
        	GameManager.Instance.CheckClikedBord(other.gameObject);
		}
		else if(other.tag == "Go")
		{
			GameManager.Instance.BordGo();
		}
		else if(other.tag == "ShowBlockType")
		{
			//UIManager.Instance.ShowInfomation(other.gameObject, other.tag);
			OutHeroSetBlock block = (OutHeroSetBlock)other.gameObject.GetComponent(typeof(OutHeroSetBlock));
			block.ShowInfo();
		}
		else if(other.tag == "BlockInventory")
		{
			Debug.Log("Enter block");
			OutHeroInventoryBlock block = (OutHeroInventoryBlock)other.gameObject.GetComponent(typeof(OutHeroInventoryBlock));
			block.ShowInfo();
		}
		else if(other.tag == "EquipInventory")
		{
			Debug.Log("Enter Equip");
			OutHeroInventoryEquip equip = (OutHeroInventoryEquip)other.gameObject.GetComponent(typeof(OutHeroInventoryEquip));
			equip.ShowInfo();
		}
		else if(other.tag == "ShowEquip")
		{
			Debug.Log("Enter Equip");
			OutHeroSetEquip equip = (OutHeroSetEquip)other.gameObject.GetComponent(typeof(OutHeroSetEquip));
			equip.ShowInfo();
		}
		else if(other.tag == "ItemInventory")
		{
			Debug.Log("Enter Item");
			OutHeroinventoryItem item = (OutHeroinventoryItem)other.gameObject.GetComponent(typeof(OutHeroinventoryItem));
			item.ShowInfo();
		}
		else if(other.tag == "ShowInventory")
		{
			Debug.Log("Enter Show");
			OutShopInventoryBlock item = (OutShopInventoryBlock)other.gameObject.GetComponent(typeof(OutShopInventoryBlock));
			item.ShowInfo();
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{

	}
}
