using System;
using System.IO;
using Infrastructure.Data;
using Newtonsoft.Json;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Infrastructure.Services
{
    public class DataProvider
    {
        public event Action<IData> DataUpdated; 
        
        [DI] private ProfileProvider _profileProvider;

        private string PathData = Application.dataPath + "/Data";

        [DIC]
        private void Init()
        {
            _profileProvider.ProfileCreated += OnProfileCreated;
            _profileProvider.ProfileDeleted += OnProfileDeleted;
        }
        
        public DataProvider()
        {
            if (!Directory.Exists(PathData))
                Directory.CreateDirectory(PathData);
        }

        public void InitIfNotExist<T>() where T : IData, new()
        {
            T data = new T();
            string path = GetPath(data);
            if(!File.Exists(path))
                Save(data);
        }
        
        public T Get<T>() where T : IData, new()
        {
            T defaultData = new T();
            string path = GetPath(defaultData);
            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }
            else
            {
                Save(defaultData);
                return defaultData;
            }
        }

        public void Save<T>(T instance) where T : IData
        {
            string saveStringJson = JsonConvert.SerializeObject(instance, Formatting.Indented);
            string path = GetPath(instance);
            if (File.Exists(path))
            {
                File.Delete(path);
                File.AppendAllText(path,saveStringJson);
            }
            else
            {
                File.AppendAllText(path, saveStringJson);
            }
            DataUpdated?.Invoke(instance);
        }

        private void OnProfileDeleted(string name) => Directory.Delete(PathData + "/" + name, true);

        private void OnProfileCreated(string name) => Directory.CreateDirectory(PathData + "/" + name);

        private string GetPath<T>(T defaultData) where T : IData
        {
            string result = PathData + "/" + _profileProvider.CurrentProfile;
            if (!Directory.Exists(result))
                Directory.CreateDirectory(result);
            result += "/" + defaultData.Name + ".txt";
            return result;
        }
    }
}