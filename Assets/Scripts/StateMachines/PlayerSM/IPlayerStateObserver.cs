namespace StateMachines.PlayerSM
{
    public interface IPlayerStateObserver
    {
        void OnPlayerStateChanged(BaseState newState);
    }
}