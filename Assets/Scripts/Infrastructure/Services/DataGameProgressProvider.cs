using System.IO;
using Infrastructure.Data;
using Newtonsoft.Json;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Infrastructure.Services
{
    public class DataGameProgressProvider
    {
        [DI] private ProfileProvider _profileProvider;

        private string _baseDataPath = Application.dataPath + "/Data";

        [DIC]
        private void Init()
        {
            InitDataIfNotExist();
            _profileProvider.NewProfileSelected += OnNewSelectedProfile;
        }

        public DataProgressGame Get()
        {
            string path = GetPathToData();
            if(!File.Exists(path))
                return  new DataProgressGame();

            var result = JsonConvert.DeserializeObject<DataProgressGame>(File.ReadAllText(path));
            return result;
        }

        public void Save(DataProgressGame data)
        {
            string pathToData = GetPathToData();
            string jsonText = JsonConvert.SerializeObject(data, Formatting.Indented);
            
            if (File.Exists(pathToData))
                File.Delete(pathToData);
            File.AppendAllText(pathToData, jsonText);
        }

        private void OnNewSelectedProfile() => InitDataIfNotExist();

        public void InitDataIfNotExist() => Save(Get());

        private string GetPathToData() => GetPathProfileDirectory() + "/" + new DataProgressGame().Name + ".txt";

        private string GetPathProfileDirectory() => _baseDataPath + "/" + _profileProvider.CurrentProfile;
    }
}