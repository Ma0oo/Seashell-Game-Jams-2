using System;
using System.Diagnostics;
using System.Runtime;
using HabObjects.Actors.Component.DoorsAndOtherTransitBeh;
using Infrastructure.GameStateMachines.States;
using Infrastructure.ScenesServices.Lobby.Signals;
using Mechanics;
using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using UnityEditor.Experimental;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace HabObjects.Dungeons.Component
{
    [CustomizableComponent("настройка спауна", 0)]
    public class TransferPlayerToNextRoom : MonoBehaviour
    {
        [SerializeField] private Dungeon _dungeon;
        [SerializeField] private SpawnerNewRoomDungeon _spawnerNewRoomDungeon;

        [TRangeInt("Безопастная комната каждая", 1, 10, new[] {1})][SerializeField] 
        
        [DI] private Curtain _curtain;
        [DI(RandomDungeon.IdChanelLevel)] private EventChanel _chanelLevel;
        [DI] private DataDungeon _dataDungeon;

        private Actor _player;
        private int _currentRoom = 1;
        
        [DIC]
        private void Init() => _chanelLevel.AddListen<PlayerSpawned>(e => _player = e.Actor);


        private void Awake()
        {
            _dungeon.BloodSystem.Track<TransitPlayerToNewRoom>(OnTransitPlayer);
        }

        private void OnTransitPlayer(TransitPlayerToNewRoom obj)
        {
            _curtain.Transit(() =>
            {
                GCSettings.LatencyMode = GCLatencyMode.Batch;
                _spawnerNewRoomDungeon.CurrentRoom.BloodSystem.Fire(new StopRoom());
                Destroy(_spawnerNewRoomDungeon.CurrentRoom.gameObject);
                if (_currentRoom % (_dataDungeon.SafeRoomEach+1) == 0)
                    _spawnerNewRoomDungeon.SpawnSafeRoom(Vector3.zero);
                else
                    _spawnerNewRoomDungeon.SpawnRoom(Vector3.zero);
                _player.transform.position = _spawnerNewRoomDungeon.CurrentRoom.GeneralContainer.GetOrNull<PointsEnterPlayer>().RandomPoint.position;
                _spawnerNewRoomDungeon.CurrentRoom.BloodSystem.Fire(new StartRoom());
                _currentRoom++;
                GCSettings.LatencyMode = GCLatencyMode.Interactive;
            });
        }
    }
}