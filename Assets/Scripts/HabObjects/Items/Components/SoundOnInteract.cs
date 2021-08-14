using HabObjects.Actors.Signals;
using Plugins.HabObject;
using Plugins.HabObject.DIContainer;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;

namespace HabObjects.Items.Components
{
    public class SoundOnInteract : MonoBehaviour
    {
        [SerializeField] private HabObject _actor;
        [SerializeField] private Sound2DSO _sound;

        [DI] private SoundSystem _soundSystem;
        
        private void Awake() => _actor.BloodSystem.Track<Interact>(x=>_soundSystem.Play(_sound));
    }
}