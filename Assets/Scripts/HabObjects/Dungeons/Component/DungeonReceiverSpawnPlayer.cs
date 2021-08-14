using Infrastructure.GameStateMachines.States;
using Infrastructure.ScenesServices.Lobby.Signals;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using UnityEngine;

namespace HabObjects.Dungeons.Component
{
    public class DungeonReceiverSpawnPlayer : MonoBehaviour
    {
        [SerializeField] private Dungeon _dungeon;
        [SerializeField] private SpawnerNewRoomDungeon _spawnerNewRoom;
        
        [DI(RandomDungeon.IdChanelLevel)] private EventChanel _eventChanel;

        [DIC]
        private void Init() => _eventChanel.AddListen<PlayerSpawned>(OnSpawnPlayer);

        private void OnSpawnPlayer(PlayerSpawned obj)
        {
            _eventChanel.RemoveListen<PlayerSpawned>(OnSpawnPlayer);
            SpawnSafeRoom(obj.Actor);
        }


        private void SpawnSafeRoom(Actor player)
        {
            var room = _spawnerNewRoom.SpawnSafeRoom(Vector3.zero);
            player.transform.position = room.GeneralContainer.GetOrNull<PointsEnterPlayer>().RandomPoint.transform.position;
            room.BloodSystem.Fire(new StartRoom());
        }
    }
}