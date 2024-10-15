using UnityEngine;
using System.Collections;

public class GameResultProcessor : IStateProcessor
{
	public void Begin()
	{
		UIManager.Instance.Open (UIType.kResult);
	}
	
	public void Update(float dt)
	{
	}
	
	public void End()
	{
		UIManager.Instance.Close (UIType.kResult);	
	}
}
