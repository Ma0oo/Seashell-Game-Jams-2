using System;
using System.Collections;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;
using UnityEngine.AI;

namespace HubObject.Actors.Component.Enemy
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private NavMeshAgent _meshAgent;
        
        private Actor _player;
        private Coroutine _actionSetDestination;

        private void Awake()
        {
            _meshAgent.updateRotation = false;
            _meshAgent.updateUpAxis = false;
        }

        private void OnEnable()
        {
            _player = ServicesLocator.MainContainer.ResolveSingle<Actor>(BootStrapGameScene.PlayerId);
            if(_player == _actor)
                throw new Exception("Player controled by mesh agent");
            _actionSetDestination = StartCoroutine(UpdatePosition());
            _meshAgent.enabled = true;
        }

        private void OnDisable()
        {
            if(_actionSetDestination!=null)
                StopCoroutine(_actionSetDestination);
            _meshAgent.enabled = false;
        }

        private IEnumerator UpdatePosition()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                _meshAgent.SetDestination(_player.transform.position);
            }
        }
    }
}
