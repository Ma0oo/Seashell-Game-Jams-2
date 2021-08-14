using System;
using HabObjects.Rooms.Signals;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HabObjects.Rooms.Component.Recievers
{
    public class ReceiverRoomCleaned : MonoBehaviour
    {
        [SerializeField] private Room _room;
        [SerializeField] private CounterMonsterInRoom counterMonster;

        public UnityEvent OnMonstersDead;

        private void Awake() => _room.BloodSystem.Track<RoomCleaned>(x => OnMonstersDead?.Invoke());
    }
}