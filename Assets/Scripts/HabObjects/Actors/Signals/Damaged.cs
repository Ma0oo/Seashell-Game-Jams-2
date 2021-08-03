using System;

namespace HabObjects.Actors.Signals
{
    public class Damaged
    {
        public float Value { get; }

        public Damaged(float damageValue)
        {
          if(damageValue<0)
             throw new Exception("Damage can't be less 0");
          Value = damageValue;
        }
    }
}