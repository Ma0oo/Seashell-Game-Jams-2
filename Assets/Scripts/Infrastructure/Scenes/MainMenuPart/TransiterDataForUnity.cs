using System;
using Infrastructure.Data;
using Infrastructure.Services;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    public class TransiterDataForUnity
    {
        [DI] private DataProvider _dataProvider;
        [DI] private ProfileProvider _profileProvider;

        [DIC]
        private void Init()
        {
            _profileProvider.NewProfileSelected += OnNewProfileSelected;
            _dataProvider.DataUpdated += OnDataUpdate;
        }

        private void OnDataUpdate(IData obj)
        {
            if(obj is DataSound) SetSound(obj as DataSound);
            else if(obj is GraphicsData) SetGraphic(obj as GraphicsData);
            else throw new Exception("Unknow data");
        }

        private void OnNewProfileSelected()
        {
            SetSound(_dataProvider.Get<DataSound>());
            SetGraphic(_dataProvider.Get<GraphicsData>());
        }

        private void SetSound(DataSound sound)
        {
            Debug.Log("set Sound =)");
        }

        private void SetGraphic(GraphicsData sound)
        {
            Debug.Log("set graphics =)");
        }
    }
}