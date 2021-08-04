using Infrastructure.GameStateMachines.States;
using Plugins.HabObject.Property;
using UnityEngine;

namespace HabObjects.Dungeons.Data
{
    public class PlaceDataDungeon : DataProperty
    {
        public DataDungeon Data;

        public void Init(DataDungeon data) => Data = data;
    }
}