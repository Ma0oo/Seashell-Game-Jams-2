namespace Infrastructure.GameStateMachines.Interfaces
{
    public interface IPayLoadedState<TPay> : IExitableState
    {
        void Enter(TPay payLoaded);
    }
}