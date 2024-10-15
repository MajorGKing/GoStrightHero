using UnityEngine;
using System.Collections;

public class GameIdleProcessor : IStateProcessor
{
	public void Begin()
	{
		UIManager.Instance.Open (UIType.kMain);
	}

	public void Update(float dt)
	{
	}

	public void End()
	{
		UIManager.Instance.Close (UIType.kMain);
	}
}
