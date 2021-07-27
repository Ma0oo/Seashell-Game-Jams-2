using System;
using UnityEngine;

namespace Plugins.HubObject.GlobalSystem
{
    public class InjectDI : MonoBehaviour
    {
        private void Awake()
        {
            foreach (var beh in FindObjectsOfType<MonoBehaviour>()) ServicesLocator.MainContainer.InjectSingle(beh);
        }
    }
}