using UnityEngine;
using System.Collections;

public class OutHeroInventoryEquip : MonoBehaviour 
{
	public void ShowInfo()
	{
		OutHeroEquipInventory parent = (OutHeroEquipInventory)gameObject.transform.parent.GetComponent(typeof(OutHeroEquipInventory));
		parent.ShowThisEquipInfo(gameObject);
	}
}
