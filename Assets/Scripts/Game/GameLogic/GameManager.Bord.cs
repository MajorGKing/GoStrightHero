using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public partial class GameManager : TMonoSingleton<GameManager>, IStatable<GameState>, IDisposable
{
	public void SetBoardInfo(List<string> bordName)
	{
		m_totalBlockValue = 0;
		m_bordName.Clear();
		m_bordValue.Clear();

		TextAsset BlockDB = Resources.Load("DB/Block/BlockSpace", typeof(TextAsset)) as TextAsset;

		// Must change, after 
		int slotSize = 4;
		// Importent

		string[] dbInfo;

		for(int i = 0; i < slotSize; i++)
		{
			string readLine;
			string sep = "\t";

			StringReader BlockDBFind = new StringReader(BlockDB.text);


			while((readLine = BlockDBFind.ReadLine()) != null)
			{
				dbInfo = readLine.Split(sep.ToCharArray());
			
				if(dbInfo[0] == bordName[i])
				{
					m_bordName.Add(bordName[i]);

					m_bordValue.Add(int.Parse(dbInfo[1]));

					break;
				}
			}
		}

		foreach (var item in m_bordValue)
		{
			m_totalBlockValue += item;
		}

		//Debug.Log("m_totalBlockValue : " + m_totalBlockValue);
	}

	public int GetBlockValue()
	{
		int value = UnityEngine.Random.Range(0, m_totalBlockValue);
		

		//Debug.Log("m_bordValue[0] : " + m_bordValue[0]);
		//Debug.Log("m_bordValue[1] : " + m_bordValue[1]);
		//Debug.Log("m_bordValue[2] : " + m_bordValue[2]);
		//Debug.Log("m_bordValue[3] : " + m_bordValue[3]);

		if(value < m_bordValue[0])
		{
			return 0;
		}
		else if(value < m_bordValue[0] + m_bordValue[1])
		{
			return 1;
		}
		else if(value < m_bordValue[0] + m_bordValue[1] + m_bordValue[2])
		{
			return 2;
		}
		else if(value < m_bordValue[0] + m_bordValue[1] + m_bordValue[2] + m_bordValue[3])
		{
			return 3;
		}

		return -1;
	}

	public void SendBordInfo()
	{
		UIManager.Instance.UIGame.SetBord(m_bordName);
	}
}
