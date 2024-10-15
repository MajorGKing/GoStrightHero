using System.Collections.Generic;

public class TStateController<T> where T : struct
{
	private Dictionary<T, IStateProcessor> m_stateProcessor;
	private T m_currentState;
	public T CurrentState
	{
		get
		{
			return m_currentState;
		}
	}

	private readonly T m_kInvalidState;

	public TStateController()
	{
		m_stateProcessor = new Dictionary<T, IStateProcessor>();
		m_kInvalidState = (T)System.Enum.Parse (typeof(T), "0");
		m_currentState = m_kInvalidState;
	}

	public void AddState(T state, IStateProcessor processor)
	{
		m_stateProcessor.Add (state, processor);
	}

	public void RemoveState(T state)
	{
		if (true == m_stateProcessor.ContainsKey(state))
		{
			m_stateProcessor.Remove (state);
		}
	}

	public IStateProcessor ChangeState(T state)
	{
		if (!m_currentState.Equals(m_kInvalidState))
		{
			m_stateProcessor[m_currentState].End();
		}

		m_currentState = state;
		m_stateProcessor [m_currentState].Begin ();

		return m_stateProcessor [m_currentState];
	}

	public void Update(float dt)
    {
        m_stateProcessor[m_currentState].Update(dt);
    }
}
