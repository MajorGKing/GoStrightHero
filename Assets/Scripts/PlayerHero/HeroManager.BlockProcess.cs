using UnityEngine;
using System.Collections;

public partial class HeroManager : MonoBehaviour 
{
    public void BlockValue(int name, int numbers)
	{
		//  ㅜ'numbers' of 'name' blocks are actived
		//Debug.Log(m_blockType[name, 0]);
		//Debug.Log(m_blockValue[name, numbers]);

        // temp //
        if(numbers > 10)
        {
            numbers = 10;
        }

		// Block Numbers
		//Debug.Log("Before : " + m_blockType[name, 2]);
		if(m_blockType[name, 1] == (int)BlockStockType.kRenew)
		{
			// m_blockType[name, 2] save active block numbers
			if(m_blockType[name, 2] < numbers)
			{
				m_blockType[name, 2] = numbers;
			}
		}
		else if(m_blockType[name, 1] == (int)BlockStockType.kStock)
		{
			m_blockType[name, 2] += numbers;
		}
        
		//Debug.Log("After : " + m_blockType[name, 2]);

		// Active
		m_BlockProcess(name);
	}

	private void m_BlockProcess(int blockIndex)
    {
        int effectType = m_blockType[blockIndex, 0];

        if(effectType == 10)
        {
            m_attackPowAdd = m_blockValue[blockIndex, m_blockType[blockIndex, 2] - 1] * m_attackPow;
            m_attackPowAdd -= m_attackPow;
        }
        else if(effectType == 20)
        {
            m_attackSucessAdd = m_blockValue[blockIndex, m_blockType[blockIndex, 2] - 1] * m_attackSucess;
            m_attackSucessAdd -= m_attackSucess;
        }
        else if(effectType == 30)
        {
            m_heal = (int)m_blockValue[blockIndex, m_blockType[blockIndex, 2] - 1];
            m_Heal();
        }
        else if(effectType == 40)
        {
            m_avoidAdd = m_blockValue[blockIndex, m_blockType[blockIndex, 2] - 1] * m_avoid;
            m_avoidAdd -= m_avoid;
        }
    }

    private void m_Heal()
    {
        //Debug.Log("Heal : " + m_heal);
        m_currentHP += m_heal;

        if(m_currentHP > m_maxHP)
        {
            m_currentHP = m_maxHP;
        }

        UIManager.Instance.UIGame.SetHPRed(1 - m_currentHP/m_maxHP);

        m_blockType[m_healIndex, 2] = 0;
        m_heal = 0;
    }
}

