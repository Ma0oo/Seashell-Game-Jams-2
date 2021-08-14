using System.Reflection;
using Plugins.HabObject.Customizable.Attributs;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plugins.HabObject.Customizable.ViewAttribute
{
    public class ViewObject : ViewFieldCustomizableComponent
    {
        private readonly TObject _attribute;
        private readonly FieldInfo _fieldInfo;
        private readonly MonoBehaviour _instance;

        public ViewObject(TObject attribute, FieldInfo fieldInfo, MonoBehaviour instance)
        {
            _attribute = attribute;
            _fieldInfo = fieldInfo;
            _instance = instance;
        }

        public override void OnGUI()
        {
            GUILayout.Label(_attribute.Name + " / " + _fieldInfo.FieldType.ToString().DeleteBeginCharFromEnd('.'));
            Object obj = _fieldInfo.GetValue(_instance) as Object;
            var type = obj.GetType();
            var objToSet = EditorGUILayout.ObjectField(obj, type, true);
            _fieldInfo.SetValue(_instance, objToSet);
        }
    }
}