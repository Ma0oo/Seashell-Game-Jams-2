using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.HabObject.DIContainer
{
    public class BindDIScene : MonoBehaviour
    {
        [SerializeField] private  List<ObjectToDi> _objects = new List<ObjectToDi>();
        [SerializeField] private ManualBindDi[] _manualBind;
        
        private void Awake()
        {
            foreach (var manual in _manualBind) manual.Create(DiServices.MainContainer);
            foreach (var obj in _objects) DiServices.MainContainer.RegisterSingleType(obj.Instance, obj.id);
        }

        private void OnDestroy()
        {
            foreach (var manual in _manualBind) manual.DestroyDi(DiServices.MainContainer);
            foreach (var obj in _objects)
                if (obj.IsUnbind)
                    DiServices.MainContainer.RemoveSingelType(obj.Instance.GetType(), obj.id);
        }

        [System.Serializable]
        private class ObjectToDi
        {
            public bool IsUnbind;
            public string id = "";
            public Component Instance;
        }
    }
}