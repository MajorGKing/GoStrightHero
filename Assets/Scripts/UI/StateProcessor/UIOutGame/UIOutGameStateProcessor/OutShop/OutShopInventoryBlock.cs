using UnityEngine;
using System.Collections;

public class OutShopInventoryBlock : MonoBehaviour 
{

	public void ShowInfo()
	{
		OutShopShows parent = (OutShopShows)gameObject.transform.parent.GetComponent(typeof(OutShopShows));
		parent.ShowThisItemInfo(gameObject);
	}
}
