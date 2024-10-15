public interface IStateProcessor
{
	void Begin();
	void Update(float dt);
	void End();
}