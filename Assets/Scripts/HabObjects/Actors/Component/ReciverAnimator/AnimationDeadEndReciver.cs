using UnityEngine;
using UnityEngine.Events;

namespace HabObjects.Actors.Component.ReciverAnimator
{
    public class AnimationDeadEndReciver : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onEnd;
        
        private void AnimationDeadEnd() => _onEnd?.Invoke();
    }
}