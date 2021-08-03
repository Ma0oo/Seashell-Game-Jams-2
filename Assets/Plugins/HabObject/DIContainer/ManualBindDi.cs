using UnityEngine;

namespace Plugins.HabObject.DIContainer
{
    public abstract class ManualBindDi : MonoBehaviour
    {
        public abstract void Create(DiServices container);

        public abstract void DestroyDi(DiServices container);
    }
}