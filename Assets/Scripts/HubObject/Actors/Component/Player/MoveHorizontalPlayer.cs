using System;
using Plugins.HubObject.GlobalSystem;
using Services.Interfaces;
using UnityEngine;

namespace HubObject.Actors.Component.Player
{
    public class MoveHorizontalPlayer : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private SpeedPlayer _speedPlayer;
        private IInput _input;

        private void Awake()
        {
            _input = ServicesLocator.MainContainer.ResolveSingle<IInput>();
            _speedPlayer = _actor.ComponentShell.Get<SpeedPlayer>();
        }

        private void FixedUpdate() => _rigidbody2D.velocity = _input.AxisMove.normalized * _speedPlayer.Value;
    }
}