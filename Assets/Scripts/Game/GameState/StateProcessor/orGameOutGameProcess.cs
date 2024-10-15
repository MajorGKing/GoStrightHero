using UnityEngine;
using System.Collections;

public class GameOutGameProcessor : IStateProcessor
{
	public void Begin()
	{
		Debug.Log("outgame");
		UIManager.Instance.Open (UIType.kOutGame);
		
		HeroManager heroManager = (HeroManager)UserManager.Instance.Hero.GetComponent(typeof(HeroManager));

		heroManager.HeroDataLoad();
	}
	
	public void Update(float dt)
	{
		
	}
	
	public void End()
	{
		//SaveLoadManager.Instance.Save();
		UIManager.Instance.Close (UIType.kOutGame);
	}
}
