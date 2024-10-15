using UnityEngine;
using System.Collections;

public class UIMain : UIBase
{
	public override void Initialize ()
	{
		m_isInitialized = true;
	}

	public void OnClickBtStart()
	{
		GameManager.Instance.ChangeState (GameState.kOutGame);
	}
}
