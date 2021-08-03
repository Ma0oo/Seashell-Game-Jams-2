using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Infrastructure.Services
{
    public class ProfileProvider
    {
        public event Action NewProfileSelected;
        public event Action<string> ProfileDeleted;
        public event Action<string> ProfileCreated;
        public string CurrentProfile { get; private set; } = string.Empty;

        private const string PrefProfile = "profiles";
        public const string DefaultProfile = "Default";

        public List<string> GetAllProfiles() => GetProfileNameFromPref();

        public bool Has(string name) => GetAllProfiles().Contains(name);

        public bool Remove(string name)
        {
            var listName = GetAllProfiles();
            bool result = listName.Remove(name);
            SetProfileNamesToPref(listName);
            
            ProfileDeleted?.Invoke(name);

            if (name == DefaultProfile)
            {
                Create(DefaultProfile);
                Choise(DefaultProfile);
            }

            return result;
        }

        public bool Create(string name)
        {
            if (name.Length <= 3)
                return false;
            
            var names = GetAllProfiles();
            
            if (GetAllProfiles().Contains(name))
                return false;
            
            names.Add(name);
            SetProfileNamesToPref(names);
            ProfileCreated?.Invoke(name);
            
            return true;
        }

        public bool Choise(string name)
        {
            if (GetAllProfiles().Contains(name))
            {
                CurrentProfile = name;
                NewProfileSelected?.Invoke();
                return true;
            }
            return false;
        }

        private static List<string> GetProfileNameFromPref()
        {
            var profilesJson = PlayerPrefs.GetString(PrefProfile, JsonConvert.SerializeObject(new List<string>()));
            var profileName = JsonConvert.DeserializeObject<List<string>>(profilesJson);
            return profileName;
        }

        private static void SetProfileNamesToPref(List<string> names)
        {
            var jsonFile = JsonConvert.SerializeObject(names);
            PlayerPrefs.SetString(PrefProfile, jsonFile);
        }
    }
}