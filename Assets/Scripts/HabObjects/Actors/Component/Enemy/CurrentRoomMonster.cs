using System;
using Factorys;
using UnityEngine;

namespace HabObjects.Actors.Component.Enemy
{
    public class CurrentRoomMonster : MonoBehaviour
    {
        public Room Value { get; private set; }

        [SerializeField] private Actor _actor;

        private void Awake() => _actor.BloodSystem.Track<MonsterAddToRoom>(x=>Value=x.Room);
    }
}