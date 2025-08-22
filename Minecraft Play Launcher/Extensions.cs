namespace Minecraft_Play_Launcher;

public static class Extensions
{
    public static void Let<T>(this T? obj, Action<T?> action)
    {
        action(obj);
    }
    

    public static void Or<T>(this T? obj, Action<T> action) where T : class
    {
        if (obj == default)
        {
            return;
        }

        action(obj);
    }

    public static T? Apply<T>(this T? obj, Action action) where T : class
    {
        action();
        return obj;
    }

    public static T OrElse<T>(this T? obj, T defaultValue) where T : class
    {
        return obj ?? defaultValue;
    }

    public static R? Try<T, R>(this T? obj, Func<R> func) where T : class
    {
        try
        {
            return func();
        }
        catch (Exception)
        {
            return default;
        }
    }
    
}