using UnityEngine;
using System.Collections;

public class OutHeroInventoryBlock : MonoBehaviour 
{
	public void ShowInfo()
	{
		Debug.Log("Here!");
		OutHeroBlockInventory parent = (OutHeroBlockInventory)gameObject.transform.parent.GetComponent(typeof(OutHeroBlockInventory));
		parent.ShowThisBlockInfo(gameObject);

	}

}
