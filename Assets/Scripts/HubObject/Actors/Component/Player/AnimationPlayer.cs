using UnityEngine;

namespace HubObject.Actors.Component.Player
{
    public class AnimationPlayer : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private void Update()
        {
            var velocity = _rigidbody2D.velocity;
            _spriteRenderer.flipX = velocity.x < 0;
            _animator.SetFloat(Speed, velocity.magnitude);
        }
    }
}