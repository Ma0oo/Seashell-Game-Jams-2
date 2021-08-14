using System.Reflection;
using Plugins.HabObject.Customizable.Attributs;
using UnityEditor;
using UnityEngine;

namespace Plugins.HabObject.Customizable.ViewAttribute
{
    public class ViewString : ViewFieldCustomizableComponent
    {
        private readonly TString _attribute;
        private readonly FieldInfo _fieldInfo;
        private readonly MonoBehaviour _instance;

        public ViewString(TString attribute, FieldInfo fieldInfo, MonoBehaviour instance)
        {
            _attribute = attribute;
            _fieldInfo = fieldInfo;
            _instance = instance;
        }

        public override void OnGUI()
        {
            GUILayout.Label(_attribute.Name);
            _fieldInfo.SetValue(_instance, EditorGUILayout.TextArea(_fieldInfo.GetValue(_instance) as string, GUILayout.Height(_attribute.Space)));
        }
    }
}