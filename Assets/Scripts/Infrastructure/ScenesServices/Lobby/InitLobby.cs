using System;
using Factorys;
using HabObjects;
using Huds;
using Infrastructure.ScenesServices.Lobby.Signals;
using Mechanics;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.ScenesServices.Lobby
{
    public class InitLobby : MonoBehaviour
    {
        [SerializeField] private Transform _position;
        [SerializeField] private OneLoopAnimation _animationSpawn;
        [SerializeField] private Sound2DSO _soundSpawn;

        [DI] private PlayerFactory _playerFactory;
        [DI] private DataPlayerCreator _dataPlayerCreator;
        [DI] private HudFactory _hudFactory;
        [DI] private CameraServicesFactory _cameraServicesFactory;
        [DI] private SoundSystem _soundSystem;
        [DI(GameStateMachines.States.Lobby.ChanelLobbyId)] private EventChanel _chanelLobby;
        
        private void Awake()
        {
            Actor player;
            if (!_playerFactory.TryCreate(out player, new Vector3(100,100,0)))
            {
                ChoiseClassHud hudClass = (ChoiseClassHud)_hudFactory.Crete<ChoiseClassHud>();
                _dataPlayerCreator.DataCreated += OnCreatedDataPlayer;
                return;
            }

            var animation = Instantiate(_animationSpawn, _position.position, Quaternion.identity);
            animation.EndAnimation += () => FinalSpawnPlayer(player);
            
        }

        private void OnCreatedDataPlayer()
        {
            _dataPlayerCreator.DataCreated -= OnCreatedDataPlayer;
            
            Actor player;
            if (!_playerFactory.TryCreate(out player, new Vector3(100,100,0)))
                throw new Exception("Error on create Player");
            
            var animation = Instantiate(_animationSpawn, _position.position, Quaternion.identity);
            animation.EndAnimation += () => FinalSpawnPlayer(player);
        }

        private void FinalSpawnPlayer(Actor player)
        {
            player.transform.position = _position.position;
            _soundSystem.Play(_soundSpawn).transform.position = player.transform.position;
            RegisterPlayer(player);
            CreatePlayerHud();
            CreateCamera();
            _chanelLobby.Fire(new PlayerSpawned(player));
            _chanelLobby.Fire(new LobbyFinishEnter());
        }

        private void CreateCamera() => _cameraServicesFactory.Create();

        private void CreatePlayerHud() => _hudFactory.Crete<PlayerHud>();

        private static void RegisterPlayer(Actor player) => DiServices.MainContainer.RegisterSingle<Actor>(player, DIConstID.PlayerId);
    }
}