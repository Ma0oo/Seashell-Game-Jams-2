using Infrastructure.Data;
using Infrastructure.Services;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Infrastructure.ScenesServices.MainMenuPart.Mono
{
    public class InitProviderMainMenu
    {
        [DI] private DataProvider _data;
        [DI] private ProfileProvider _profile;

        [DIC]
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
            _data.InitIfNotExist<DataControl>();
            _data.InitIfNotExist<DataProgressGame>();
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