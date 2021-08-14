using UnityEngine;

namespace Mechanics.LevelConditionOpen
{
    public class StaticCloseCondition : ConditionOpenDungeon
    {
        public override bool IsOpen() => false;
    }
}