using UnityEngine;
using System.Collections;

public class OutHeroinventoryItem : MonoBehaviour 
{

	public void ShowInfo()
	{
		OutHeroItemInventory parent = (OutHeroItemInventory)gameObject.transform.parent.GetComponent(typeof(OutHeroItemInventory));
		parent.ShowThisItemInfo(gameObject);
	}
}
