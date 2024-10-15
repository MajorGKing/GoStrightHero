using UnityEngine;
using System.Collections;

public class ApplicationMain : MonoBehaviour
{
	void Awake ()
	{
		//int height = Screen.height;
		//int width = Screen.width;

		//width = height/16 * 9;

		//Screen.SetResolution(width, height, true, 60);
		Screen.SetResolution(720, 1280, true, 60);

		SaveLoadManager.Create();
		UIManager.Create ();
		GameManager.Create ();
		TouchPoint.Create();
		UserManager.Create();
		
	}
}
