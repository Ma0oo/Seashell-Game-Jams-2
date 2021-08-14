using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;

namespace LuaFramework.Tests
{
	public class LuaFrameworkDemo : MonoBehaviour
	{
		public Text console;
		public Text consoleRight;

		public List<BuiltInTypesObject> builtInTypesObjects = new List<BuiltInTypesObject>();
		public Dictionary<string, Enemy> enemies = new Dictionary<string, Enemy>();
		public List<UnityTypesObject> unityTypesObjects = new List<UnityTypesObject>();

		private void Start()
		{
			// Create the lua state
			var lua = new Script();

			// READER TEST
			
			// Register our define functions in the Lua state
			lua.Globals["defineBuiltInTypesObject"] = (Action<DynValue>) DefineBuiltInTypesObject;
			lua.Globals["defineUnityTypesObject"] = (Action<DynValue>) DefineUnityTypesObject;
			lua.Globals["defineEnemies"] = (Action<DynValue>) DefineEnemies;

			// Load lua file from Resources file - File is named itemDefinitions.lua.txt, because
			// Unity does not support the .lua extension for files included in builds.
			var luaFile = Resources.Load<TextAsset>("tests.lua");

			// Execute file inside the Lua state.
			lua.DoString(luaFile.text);

			// Test the definitions
			console.text = "#### CLR objects read from Lua file: ####\n\n";
			foreach (BuiltInTypesObject obj in builtInTypesObjects) console.text += obj.Debug() + "\n";
			foreach (UnityTypesObject obj in unityTypesObjects) console.text += obj.Debug() + "\n";
			foreach (var enemy in enemies) console.text += "Enemy: " + enemy.Key + "\n" + enemy.Value.Debug() + "\n";

			// WRITER TEST

			string luaScript;
			// Create a Lua script from CLR object
			using (var luaWriter = new LuaWriter())
			{
				luaWriter.Write("defineEnemies");
				luaWriter.WriteObject(enemies);
				luaScript = luaWriter.ToString();
			}

			// Test the script
			consoleRight.text = "#### Lua script generated from the above CLR object: ####\n\n" + luaScript;

			// Clear the collection
			enemies = new Dictionary<string, Enemy>();

			// Exectue the script inside the Lua State
			lua.DoString(luaScript);

			// Test the result
			consoleRight.text += "\n\n#### CLR object generated again from the above script, should be equal to the one on the left: ####\n\n";
			foreach (var enemy in enemies) consoleRight.text += "Enemy: " + enemy.Key + "\n" + enemy.Value.Debug() + "\n";

		    // Test custom reader

            LuaReader.AddCustomReader(typeof(Attack), value =>
            {
                var table = value.Table;
                return new Attack()
                {
                    power = LuaReader.Read<int>(table.Get(1)),
                    cooldown = LuaReader.Read<float>(table.Get(2))
                };
            });

		    lua.DoString("attack = {5, 3.2}");
		    var attack = LuaReader.Read<Attack>(lua.Globals.Get("attack"));
		    consoleRight.text += "\n\n#### CLR object generated with custom reader: ####\n\nLua:\nattack = {5, 3.2}\n\nResult:\n" + attack.Debug();

		}

		public void DefineBuiltInTypesObject(DynValue luaTable)
		{
			var builtInTypesObject = LuaReader.Read<BuiltInTypesObject>(luaTable);
			builtInTypesObjects.Add(builtInTypesObject);
		}

		public void DefineUnityTypesObject(DynValue luaTable)
		{
			var unityTypesObject = LuaReader.Read<UnityTypesObject>(luaTable);
			unityTypesObjects.Add(unityTypesObject);
		}

		public void DefineEnemies(DynValue luaTable)
		{
			enemies = LuaReader.Read<Dictionary<string, Enemy>>(luaTable);
		}
	}
}