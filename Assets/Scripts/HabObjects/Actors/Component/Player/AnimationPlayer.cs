using System.Collections;
using HabObjects.Actors.Signals;
using UnityEngine;

namespace HabObjects.Actors.Component.Player
{
    public class AnimationPlayer : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");

        [SerializeField] private Actor _actor;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Coroutine _damagePlay;
        private Camera _camera;
        
        private static readonly int Damage = Animator.StringToHash("Damage");
        private static readonly int Dead = Animator.StringToHash("Dead");

        private void Awake() => _actor.BloodSystem.Track<ActorDeaded>(OnDead);

        private void OnEnable() => _actor.BloodSystem.Track<FinallyDamage>(DamagePlay);

        private void OnDisable() => _actor.BloodSystem.Untrack<FinallyDamage>(DamagePlay);

        private void OnDead(ActorDeaded obj)
        {
            Debug.Log("Dead");
            if(_damagePlay!=null)
                StopCoroutine(_damagePlay);
            enabled = false;
            _animator.SetBool(Dead, true);
        }

        private void DamagePlay(FinallyDamage obj)
        {
            if (_damagePlay == null)
                _damagePlay = StartCoroutine(DamageDelay());
        }

        private IEnumerator DamageDelay()
        {
            yield return new WaitForEndOfFrame();
            _animator.SetTrigger(Damage);
            _damagePlay = null;
        }
        
    
        private void Update()
        {
            CheckCamera();
            var velocity = _rigidbody2D.velocity;
            Vector3 positionMouseInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            FlipSpriteRender(positionMouseInWorld);
            SetPropertySpeed(velocity);
        }

        private void SetPropertySpeed(Vector2 velocity) => _animator.SetFloat(Speed, velocity.magnitude);

        private void FlipSpriteRender(Vector3 positionMouseInWorld) => _spriteRenderer.flipX = _spriteRenderer.transform.position.x > positionMouseInWorld.x;

        private void CheckCamera()
        {
            if (!_camera)
                _camera = Camera.main;
        }
    }
}