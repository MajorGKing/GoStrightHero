using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class UIManager : TMonoSingleton<UIManager>
{
	private Dictionary<UIType, UIBase> m_uiList;

	private UIGame m_uiGame;
	public UIGame UIGame
    {
        get
        {
            return m_uiGame;
        }
    }

	private UIOutGame m_uiOutGame;
	public UIOutGame UIOutGame
    {
        get
        {
            return m_uiOutGame;
        }
    }

	private float m_uiWidthScale;
	public float UiWidthScale
	{
		get
		{
			return m_uiWidthScale;
		}
	}
	private float m_canvasWidthSize;
	public float CanvasWidth
	{
		get
		{
			return m_canvasWidthSize;
		}
	}
	private float m_canvasHeightSize;

	private GameObject m_infoPop;

	public override void Initialize ()
	{
		gameObject.name = "UIManager";

		GameObject uiRoot = GameObject.Find ("Canvas");

		m_uiList = new Dictionary<UIType, UIBase> ();
		m_uiList.Add (UIType.kMain, uiRoot.transform.Find ("UIMain").GetComponent<UIMain> ());
		m_uiList.Add (UIType.kOutGame, uiRoot.transform.Find ("UIOutGame").GetComponent<UIOutGame> ());
		m_uiList.Add (UIType.kGame, uiRoot.transform.Find ("UIGame").GetComponent<UIGame> ());
		m_uiList.Add (UIType.kResult, uiRoot.transform.Find ("UIResult").GetComponent<UIResult> ());

		m_uiGame = uiRoot.transform.Find("UIGame").GetComponent<UIGame>();
		m_uiOutGame = uiRoot.transform.Find("UIOutGame").GetComponent<UIOutGame>();

		//m_canvasWidthSize = uiRoot.GetComponent<RectTransform>().rect.width * uiRoot.GetComponent<RectTransform>().localScale.x;
		//m_canvasHeightSize = uiRoot.GetComponent<RectTransform>().rect.height * uiRoot.GetComponent<RectTransform>().localScale.y;

		m_canvasWidthSize = uiRoot.GetComponent<RectTransform>().sizeDelta.x;

		m_uiWidthScale = uiRoot.GetComponent<RectTransform>().localScale.x;;

		//m_infoPop = uiRoot.transform.FindChild("PlShowInfo").gameObject;
	}

	public override void Destroy()
	{
	}

	public void Open(UIType type)
	{
		m_uiList [type].Open ();
	}

	public void Close(UIType type)
	{
		m_uiList [type].Close ();
	}

	public void HideInfomation()
	{
		m_infoPop.SetActive(false);
	}
	public void ShowInfomation(GameObject obj, string tagName)
	{
		Text infoText = m_infoPop.transform.Find("InfoText").GetComponent<Text>();

		if(tagName == "ShowBlockType")
		{
			Sprite objSprite = obj.GetComponent<Image>().sprite;

			TextAsset BlockDB = Resources.Load("DB/Block/BlockInfo", typeof(TextAsset)) as TextAsset;

			string[] dbInfo;

			string readLine;
			string sep = "\t";

			StringReader BlockDBFind = new StringReader(BlockDB.text);

			while((readLine = BlockDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());

				Debug.Log("objSprite.name" + objSprite.name);
				Debug.Log("dbInfo[0]" + dbInfo[0]);
			
				if(dbInfo[0] == objSprite.name)
				{
					string infoStr = "";
					foreach(var st in dbInfo)
					{
						infoStr += st;
						Debug.Log(st);
						infoStr += "\n";
					}

					infoText.text = infoStr;
					Debug.Log(infoStr);
					break;
				}
			}
		}

		m_infoPop.SetActive(true);
	}
}
