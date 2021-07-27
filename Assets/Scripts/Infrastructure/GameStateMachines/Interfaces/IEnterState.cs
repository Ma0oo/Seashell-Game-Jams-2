namespace Infrastructure.GameStateMachines.Interfaces
{
    public interface IEnterState : IExitableState
    {
        void Enter();
    }
}