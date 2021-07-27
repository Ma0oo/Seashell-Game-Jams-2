using System;
using HubObject.Rooms.Signals;
using UnityEngine;

namespace HubObject.Rooms.Component
{
    public class SpawnEnemyOnRoomClean : MonoBehaviour
    {
        [SerializeField] private Room _room;

        private void OnEnable() => _room.BloodSystem.Track<RoomCleaned>(OnRoomCleaned);

        private void OnDisable() => _room.BloodSystem.Untrack<RoomCleaned>(OnRoomCleaned);

        private void OnRoomCleaned(RoomCleaned obj) => _room.BloodSystem.Fire(new StartSpawnEnemy());
    }
}