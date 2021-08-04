using System.IO;
using HabObjects;
using Infrastructure.Data;
using Infrastructure.Data.Interface;
using Infrastructure.Services;
using Newtonsoft.Json;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Infrastructure.ScenesServices.Lobby
{
    public class DataPlayerProvider
    {
        [DI] private ProfileProvider _profileProvider;

        private string _baseDataPath = Application.dataPath + "/Data";

        [DIC]
        private void Construct()
        {
            CheckPath(_baseDataPath);
            CheckPath(GetPathProfileDirectory());
        }

        public bool GetData(out DataPlayer data)
        {
            string pathToData = GetPathToData();
            if (File.Exists(pathToData))
            {
                data = JsonConvert.DeserializeObject<DataPlayer>(File.ReadAllText(pathToData));
                return true;
            }

            data = null;
            return false;
        }

        public void SaveData(DataPlayer dataPlayer)
        {
            string pathToData = GetPathToData();
            string jsonText = JsonConvert.SerializeObject(dataPlayer, Formatting.Indented);
            
            if (File.Exists(pathToData))
                File.Delete(pathToData);
            File.AppendAllText(pathToData, jsonText);
        }

        private void CheckPath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private string GetPathToData() => GetPathProfileDirectory() + "/" + new DataPlayer().Name + ".txt";

        private string GetPathProfileDirectory() => _baseDataPath + "/" + _profileProvider.CurrentProfile;

        public void SavePlayer(Actor Player)
        {
            DataPlayer data;
            GetData(out data);
            data.PathItemsPrefab = new string[0];
            foreach (var partData in Player.ComponentShell.GetAll<ISaveDataPlayer>()) partData.Save(data);
            SaveData(data);
        }
    }
}