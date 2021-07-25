using UnityEngine;

namespace HubObject.Actors.Component.Interfaces
{
    public abstract class HealthAbs : MonoBehaviour
    {
       public abstract void Damage(float damage);
       public abstract void Recovery(float recovery);
    }
}