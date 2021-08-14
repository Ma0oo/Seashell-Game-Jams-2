    using Factorys;
using Huds;
using Infrastructure.ScenesServices.Lobby.Signals;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using UnityEngine;

namespace Infrastructure.ScenesServices.RandomDungeon
{
    public class InitRandomDungeon : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
    
        [DI] private HudFactory _hudFactory;
        [DI] private PlayerFactory _playerFactory;
        [DI] private CameraServicesFactory _cameraServicesFactory;
        [DI(GameStateMachines.States.RandomDungeon.IdChanelLevel)] private EventChanel _chanel;

        private void Awake()
        {
            _playerFactory.TryCreate(out var player, _spawnPoint.position);
            DiServices.MainContainer.RegisterSingle(player, DIConstID.PlayerId);
            _hudFactory.Crete<PlayerHud>();
            _cameraServicesFactory.Create();
            _chanel.Fire(new PlayerSpawned(player));
        }
    }
}