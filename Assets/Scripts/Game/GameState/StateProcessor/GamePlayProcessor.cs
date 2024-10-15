using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GamePlayProcessor : IStateProcessor
{
	private float m_time;
	public void Begin()
	{
		SaveLoadManager.Instance.Save();

		UserManager.Instance.SendBordInfo();
		GameManager.Instance.SendBordInfo();

		GameManager.Instance.GamePlay();


		UIManager.Instance.Open (UIType.kGame);

		HeroManager heroManager = (HeroManager)UserManager.Instance.Hero.GetComponent(typeof(HeroManager));
		heroManager.GamePlay();

		m_time = GameManager.Instance.StageTime;
	}
	
	public void Update(float dt)
	{
		m_time -= dt;

		if(m_time >= 0)
		{
			UIManager.Instance.UIGame.SetTime(m_time);
		}
		if(m_time < 0)
		{
			GameManager.Instance.StageDraw();
			End();
		}
	}
	
	public void End()
	{
		SaveLoadManager.Instance.Save();
		UIManager.Instance.Close (UIType.kGame);	
	}
}
