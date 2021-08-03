using System;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(Animator))]
    public class OneLoopAnimation : MonoBehaviour
    {
        public event Action EndAnimation;
        
        private void End()
        {
            EndAnimation?.Invoke();
            Destroy(gameObject);
        }
    }
}