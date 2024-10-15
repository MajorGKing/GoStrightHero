using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public partial class GameManager : TMonoSingleton<GameManager>, IStatable<GameState>, IDisposable
{
	private int m_selectedBordType;
	private BlockManager m_block;
	private List<GameObject> m_selectedBords;


	public void CheckClikedBord(GameObject bordObj)
	{
		// during game play
		m_block = (BlockManager)bordObj.GetComponent(typeof(BlockManager));

		if(m_selectedBordType == -1)
		{
			m_selectedBordType = m_block.BordType;
			m_block.BlockSelected();
			m_selectedBords.Add(bordObj);
		}
		else if(m_selectedBordType != -1)
		{
			int checkNum = m_block.BordType;

			if(m_selectedBordType == checkNum)
			{
				if(m_selectedBords.Contains(bordObj) == false)
				{
					m_block.BlockSelected();
					m_selectedBords.Add(bordObj);
				}
			}
			else if(m_selectedBordType != checkNum)
			{
				if(m_selectedBords.Count != 0)
				{
					foreach (var item in m_selectedBords)
					{
						m_block = (BlockManager)item.GetComponent(typeof(BlockManager));
						m_block.BlockSelectCancle();
					}
					m_selectedBords.Clear();
					m_selectedBordType = -1;
				}
			}
		}
	}

	public void BordGo()
	{
		if(m_selectedBords.Count != 0)
		{
			foreach (var item in m_selectedBords)
			{
				m_block = (BlockManager)item.GetComponent(typeof(BlockManager));
				m_block.BlockClear();
			}
			m_GoHero();
			m_selectedBords.Clear();
			m_selectedBordType = -1;
		}
	}

	private void m_GoHero()
	{
		m_heroManager.BlockValue(m_selectedBordType, m_selectedBords.Count);
	}
}
