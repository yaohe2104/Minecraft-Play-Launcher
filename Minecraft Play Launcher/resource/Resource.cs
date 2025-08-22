using System.IO;
using Newtonsoft.Json;

namespace Minecraft_Play_Launcher.resource;

public class JsonResource
{
    public static T? LoadFromJson<T>(string jsonPath) where T : JsonResource
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPath));
    }

    public static T LoadOrDefault<T>(string jsonPath, T defaultValue) where T : JsonResource
    {
        try
        {
            return LoadFromJson<T>(jsonPath).OrElse(defaultValue);
        }
        catch (Exception e)
        {
            return defaultValue;
        }
    }
    
    public void Store(string path)
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(this));
    }
}

[Serializable]
public class AppInfo : JsonResource {
    public required string BackGround
    {
        get;
        set;
    }

    public void Update(Action<AppInfo> action)
    {
        action(this);
    }
}

public class StartOption : JsonResource
{
    public required string Name { get; set; }
    public required string JavaPath { get; set; }

    public required int MaxRuntimeMemory { get; set; }
    
    public static StartOption CreateEmptyOption()
    {
        return new StartOption
        {
            Name = "",
            JavaPath = "",
            MaxRuntimeMemory = 0
        };
    }

    public void Set(string name, string javaPath, int maxRuntimeMemory)
    {
        Name = name;
        JavaPath = javaPath;
        MaxRuntimeMemory = maxRuntimeMemory;
    }

    
}

[Serializable]
public class Resource : JsonResource
{
    public required AppInfo AppInfo { set; get; }
    public required StartOption StartOption { set; get; }
    
    
}