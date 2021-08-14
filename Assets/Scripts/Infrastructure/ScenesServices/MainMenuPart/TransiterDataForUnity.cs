using System;
using Extension;
using Infrastructure.Data;
using Infrastructure.Services;
using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.ScenesServices.MainMenuPart
{
    public class TransiterDataForUnity
    {
        [DI] private DataProvider _dataProvider;
        [DI] private ProfileProvider _profileProvider;
        [DI] private ConfigMixerGroup _groups;
        [DI] private AudioMixer _mixer;
        [DI] private IInput _input;

        [DIC]
        private void Init()
        {
            _profileProvider.NewProfileSelected += OnNewProfileSelected;
            _dataProvider.DataUpdated += OnDataUpdate;
            _input.InitData(_dataProvider.Get<DataControl>());
        }

        private void OnDataUpdate(IData obj)
        {
            if(obj is DataSound) SetSound(obj as DataSound);
            else if(obj is GraphicsData) SetGraphic(obj as GraphicsData);
            else if (obj is DataControl) SetControl(obj as DataControl);
            else if(obj is DataProgressGame) return;
            else throw new Exception($"Unknow data - {obj.GetType()}");
        }

        private void SetControl(DataControl dataControl) => _input.InitData(dataControl);

        private void OnNewProfileSelected()
        {
            SetSound(_dataProvider.Get<DataSound>());
            SetGraphic(_dataProvider.Get<GraphicsData>());
        }

        private void SetSound(DataSound sound)
        {
            //_mixer.SetFloat(_groups.Effect, StandartValue(sound.Effect));
            //_mixer.SetFloat(_groups.Music, StandartValue(sound.Music));
            //_mixer.SetFloat(_groups.UI, StandartValue(sound.UI));
            //_mixer.SetFloat("Master", StandartValue(sound.Master));

            _mixer.SetFloat(_groups.Effect, sound.Effect);
            _mixer.SetFloat(_groups.Music, sound.Music);
            _mixer.SetFloat(_groups.UI, sound.UI);
            _mixer.SetFloat("Master", sound.Master);
        }

        private static float StandartValue(float normalValue) => -80-normalValue*-80;

        private void SetGraphic(GraphicsData sound)
        {
            Debug.Log("set graphics =)");
        }
    }
}