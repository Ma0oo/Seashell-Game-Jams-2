using System;
using System.Reflection;
using Plugins.HabObject.Customizable.Attributs;
using UnityEditor;
using UnityEngine;

namespace Plugins.HabObject.Customizable.ViewAttribute
{
    public class ViewInt : ViewFieldCustomizableComponent
    {
        private readonly TField _attribute;
        private readonly FieldInfo _fieldInfo;
        private readonly MonoBehaviour _instance;

        public ViewInt(TField attribute, FieldInfo fieldInfo, MonoBehaviour instance)
        {
            if(!(attribute is TInt) && !(attribute is TRangeInt))
                throw new Exception("Wrong TField");
            _attribute = attribute;
            _fieldInfo = fieldInfo;
            _instance = instance;
        }

        public override void OnGUI()
        {
            if (_attribute is TInt)
                DrawInt(_attribute as TInt);
            else
                DrawIntRange(_attribute as TRangeInt);
        }

        private void DrawInt(TInt attribute)
        {
            DrawBase(attribute.Name, attribute.Step);
        }

        private void DrawIntRange(TRangeInt attribute)
        {
            DrawBase(attribute.Name, attribute.Step);
            GUILayout.Label($"=== Min:{attribute.Min} ===   === Max:{attribute.Max} ===");
            var value = (int)_fieldInfo.GetValue(_instance);
            value = Mathf.Clamp(value, attribute.Min, attribute.Max);
            _fieldInfo.SetValue(_instance, value);
        }

        private void DrawBase(string name, int[] step)
        {
            GUILayout.Label(name + " / Int" );
            _fieldInfo.SetValue(_instance,  EditorGUILayout.IntField("Slide by me",(int)_fieldInfo.GetValue(_instance)));
            GUILayout.BeginHorizontal();
            for (int i = step.Length-1; i >= 0; i--)
            {
                if(GUILayout.Button("-"+step[i]))
                    _fieldInfo.SetValue(_instance, (int)_fieldInfo.GetValue(_instance)-step[i]);
            }
            for (int i = 0; i < step.Length; i++)
            {
                if(GUILayout.Button("+"+step[i]))
                    _fieldInfo.SetValue(_instance, (int)_fieldInfo.GetValue(_instance)+step[i]);
            }
            GUILayout.EndHorizontal();
        }
    }
}