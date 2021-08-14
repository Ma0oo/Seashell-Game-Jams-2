using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LuaFramework.Tests
{
	public class BuiltInTypesObject
	{
		public bool myBool { get; set; }
		public int myInt { get; set; }
		public float myFloat { get; set; }
		public string myString { get; set; }
		public byte myByte { get; set; }
		public decimal myDecimal { get; set; }
		
		public int[] myIntArray { get; set; }
		public Dictionary<string, List<int[]>> myDictionaryListArray { get; set; }
	}

	public class UnityTypesObject
	{
		public Color myColor { get; set; }
		public Color32 myColor32 { get; set; }
		public Rect myRect { get; set; }
		public Vector2 myVector2 { get; set; }
		public Vector3 myVector3 { get; set; }
		public Vector4 myVector4 { get; set; }

		public Vector3[] myVector3Array { get; set; }
		public List<Color> myColorList { get; set; }
		public Dictionary<string, Rect> myRectDictionary { get; set; }
	}

	public class Enemy
	{
		public int health { get; set; }
		public bool flying { get; set; }
		public Dictionary<string, Attack> attacks { get; set; }
	}

	public class Attack
	{
		public float cooldown { get; set; }
		public int power { get; set; }
	}

	public static class TestsDebug
	{
		public static string Debug(this BuiltInTypesObject o)
		{
			string s = "Built-in types test object:\n";
			s += "myBool: " + o.myBool + "\n";
			s += "myInt: " + o.myInt + "\n";
			s += "myFloat: " + o.myFloat + "\n";
			s += "myString: " + o.myString + "\n";
			s += "myByte: " + o.myByte + "\n";
			s += "myDecimal: " + o.myDecimal + "\n";
			s += "myIntArray: " + o.myIntArray.Debug() + "\n";
			if (o.myDictionaryListArray != null)
			{
				s += "myDictionaryListArray:\n";
				foreach (var pair in o.myDictionaryListArray)
				{
					s += "  Key: " + pair.Key + ", Values List: \n";
					foreach (var intArray in pair.Value)
					{
						s += "    Array: " + intArray.Debug() +",\n";
					}
				}
			}
			return s;
		}

		public static string Debug(this UnityTypesObject o)
		{
			string s = "Unity types test object:\n";
			s += "myColor: " + o.myColor + "\n";
			s += "myColor32: " + o.myColor32 + "\n";
			s += "myRect: " + o.myRect + "\n";
			s += "myVector2: " + o.myVector2 + "\n";
			s += "myVector3: " + o.myVector3 + "\n";
			s += "myVector4: " + o.myVector4 + "\n";
			if (o.myVector3Array != null)
			{
				s += "myVector3Array:\n";
				s = o.myVector3Array.Aggregate(s, (current, vector3) => current + "  " + vector3 + "\n");
			}
			if (o.myColorList != null)
			{
				s += "myColorList:\n";
				s = o.myColorList.Aggregate(s, (current, color) => current + "  " + color + "\n");
			}
			if (o.myRectDictionary != null)
			{
				s += "myRectDictionary:\n";
				s = o.myRectDictionary.Aggregate(s, (current, pair) => current + "  " + pair.Key + ": " + pair.Value + "\n");
			}
			return s;
		}

		public static string Debug(this Enemy enemy)
		{
			string s = "";
			s += "  Health: " + enemy.health + "\n";
			s += "  Flying: " + enemy.flying + "\n";
			if (enemy.attacks != null)
			{
				s += "  Attacks:\n";
				s = enemy.attacks.Aggregate(s, (s1, pair) => s1 + "    " + pair.Key + ": " + pair.Value.Debug() + ",\n");
			}
			return s;
		}

		public static string Debug(this Attack attack)
		{
			string s = "{";
			s += "Power: " + attack.power + ", Cooldown: " + attack.cooldown + "}";
			return s;
		}

		public static string Debug(this int[] array)
		{
			string s = "{";
			for (int i = 0; i < array.Length; i++)
			{
				s += array[i];
				if (i < array.Length - 1) s += ", ";
			}
			s += "}";
			return s;
		}
	}
}