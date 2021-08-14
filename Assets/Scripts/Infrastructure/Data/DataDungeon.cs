using System.Collections.Generic;
using Factorys;
using HabObjects;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    [CreateAssetMenu]
    public class DataDungeon : ScriptableObject
    {
        public string NameLevel => _nameLevel;
        [Min(1)] public int SafeRoomEach = 3;
        public Room SafeRoom => _safeRoom;
        public List<Room> PlayRooms;
        public List<DataEnemy> EnemyOnLevel;

        [SerializeField] private Room _safeRoom;
        [SerializeField] private string _nameLevel;
    }
}