using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class UIOutGame : UIBase
{
	private void m_BottomMenuInit()
	{

	}

	public void OnClickBtStage()
	{
		ChangeState(UIOutGameType.kStage);	
	}

	public void OnClickBtHeroSetting()
	{
		ChangeState(UIOutGameType.kHeroSetting);	
	}

	public void OnClickBtShop()
	{
		ChangeState(UIOutGameType.kShop);	
	}
}
