using Infrastructure.Data;
using Infrastructure.Data.Interface;
using UnityEngine;

namespace HabObjects.Actors.Component.Interfaces
{
    public abstract class HealthAbs : MonoBehaviour, ISaveDataPlayer
    {
       public abstract void Damage(float damage);
       public abstract void Recovery(float recovery);
       public abstract void Save(DataPlayer data);

       public abstract void Load(DataPlayer data);
    }
}