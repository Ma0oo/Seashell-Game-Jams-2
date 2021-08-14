using System;
using HabObjects;
using Huds.Inventorys;
using Infrastructure;
    using Plugins.HabObject.DIContainer;

namespace Huds
{
    public class PlayerHud : Hud
    {
        [DI(DIConstID.PlayerId)] private Actor _player;

        private void Awake()
        {
            foreach (var initComponent in GetComponentsInChildren<IActorInit>(true))
            {
                initComponent.Init(_player);
            }
        }
    }
}