using Infrastructure.GameStateMachines.States;
using Infrastructure.ScenesServices.Lobby.Signals;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;

namespace Mechanics
{
    public class LobbyEscControlMenu : EscControlMenu
    {
        [DI(Lobby.ChanelLobbyId)] private EventChanel _chanel;

        private void Awake()
        {
            enabled = false;
            _chanel.AddListen<PlayerSpawned>(e=>enabled = true);
        }
    }
}