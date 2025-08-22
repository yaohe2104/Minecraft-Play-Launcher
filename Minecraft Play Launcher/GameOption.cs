using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Play_Launcher
{
    internal class GameOption
    {
        public string Name { get; set; }
        public string JavaPath { get; set; }

        public string MaxRuntimeMemory { get; set; }


        public static GameOption ReadFromJson(string jsonPath)
        {
            return JsonConvert.DeserializeObject<GameOption>(File.ReadAllText(jsonPath));
        }

        public static GameOption CreateEmptyOption()
        {
            return new GameOption();
        }

        public void Set(string name, string javaPath, string maxRuntimeMemory)
        {
            Name = name;
            JavaPath = javaPath;
            MaxRuntimeMemory = maxRuntimeMemory;
        }

        public void Store(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
    }
}
