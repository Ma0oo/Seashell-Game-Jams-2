using HabObjects;
using HabObjects.Items.Components;
using Plugins.HabObject.DIContainer;
using Services.Sound;
using Services.Sound.Sound2Ds;
using UnityEngine;

namespace Mechanics
{
    public class PlaySoundOnBrokeArror : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private Sound2DSO _sound;

        [DI] private SoundSystem _soundSystem;

        private void Awake() => _item.BloodSystem.Track<ArrowHasBroken>(x=>_soundSystem.Play(_sound).transform.position = _item.transform.position);
    }
}