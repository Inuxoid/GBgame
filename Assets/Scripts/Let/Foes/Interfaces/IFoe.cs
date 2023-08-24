namespace Let.Foes.Interfaces
{
    public interface IFoe
    {
        enum FoeState { Idle, Death, Seek, Move, Fight }
        enum FoeMovementState { MoveTo, MoveFrom, DontMove }
        public void DefaultBehaviourState();
        public void SeekState();
        public void FightState();
        public void StateMachine();
        public void GetDamage();
        public void PostDamage();
        public void Death();
    }
}