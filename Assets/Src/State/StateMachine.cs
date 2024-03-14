public abstract class StateMachine
{
    public virtual IHandler Handler { get; private set; }

    public virtual bool ChangeState(State newState) { return true; }
    public virtual void Initialize(State startState) {}

    public virtual bool ChangeState(MovementState newState) { return true; }
    public virtual void Initialize(MovementState startState) { }
}