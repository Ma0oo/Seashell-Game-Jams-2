using System;
using HabObjects.Dungeons.Data;
using Infrastructure.GameStateMachines.States;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace HabObjects.Dungeons.Component
{
    public class ReceiverDataDungeon : MonoBehaviour
    {
        [SerializeField] private Dungeon _dungeon;

        [DI] private DataDungeon _data;

        [DIC]
        private void Init()
        {
            _dungeon.GeneralContainer.InitDicIfNotExit();
            _dungeon.GeneralContainer.GetOrNull<PlaceDataDungeon>().Init(_data);
        }
    }
}