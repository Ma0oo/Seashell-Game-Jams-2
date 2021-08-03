using Infrastructure.ScenesServices.Lobby;
using Infrastructure.Services;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Infrastructure.ScenesServices
{
    public class FakeDiSceneLobby : ManualBindDi
    {
        private DataPlayerProvider _dataPlayerProvider;
        private ProfileProvider _provider;
        
        public override void Create(DiServices container)
        {
            _dataPlayerProvider = new DataPlayerProvider();
            container.RegisterSingle(_dataPlayerProvider);
            _provider = new ProfileProvider();
            container.RegisterSingle(_provider);
            
            container.InjectSingle(_dataPlayerProvider);
        }

        public override void DestroyDi(DiServices container)
        {
            container.RemoveSingel<DataPlayerProvider>();
            container.RemoveSingel<ProfileProvider>();
        }
    }
}