﻿using System;
using Infrastructure.Data;
using Infrastructure.Services;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;

namespace Infrastructure.Scenes.MainMenuPart.Mono
{
    public class InitProviderMainMenu : MonoBehaviour
    {
        [DI] private DataProvider _data;
        [DI] private ProfileProvider _profile;

        private void Start()
        {
            InitProfile();
            InitData();
            _profile.ProfileCreated += OnProfileCreated;
        }

        private void OnProfileCreated(string obj) => InitData();

        private void InitData()
        {
            _data.InitIfNotExist<DataSound>();
            _data.InitIfNotExist<GraphicsData>();
        }

        private void InitProfile()
        {
            if (_profile.Has(ProfileProvider.DefaultProfile))
                _profile.Choise(ProfileProvider.DefaultProfile);
            else
            {
                _profile.Create(ProfileProvider.DefaultProfile);
                _profile.Choise(ProfileProvider.DefaultProfile);
            }
        }
    }
}