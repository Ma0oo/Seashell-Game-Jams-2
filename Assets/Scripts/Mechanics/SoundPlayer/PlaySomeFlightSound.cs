using Plugins.HabObject.DIContainer;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;

namespace Mechanics
{
    public class PlaySomeFlightSound : MonoBehaviour
    {
        [SerializeField] private Sound2DSO _sound;
        [SerializeField] private Transform _pointSound;
        
        [DI] private SoundSystem _soundSystem;

        public void Play()
        {
            Transform source = _soundSystem.Play(_sound).transform;
            source.position = transform.position;
            if (_pointSound) source.position = _pointSound.position;
        }
    }
}