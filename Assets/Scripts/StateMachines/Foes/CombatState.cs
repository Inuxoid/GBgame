using StateMachines;

public abstract class CombatState<T> : BaseState where T : BaseFoeSm<T>
{
    protected CombatState(string stateName, BaseFoeSm<T> stateMachine) : base(stateName, stateMachine)
    {
    }

    public abstract void Hit();
    public abstract void RangeHit();
    public abstract void GetDamage(int damage);
}