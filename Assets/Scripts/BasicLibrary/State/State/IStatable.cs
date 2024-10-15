public interface IStatable<T> where T : struct
{
	T GetState();
	IStateProcessor ChangeState(T state);
	TStateController<T> GetStateController();
}