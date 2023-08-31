public class Singleton<T> where T : new()
{
    private static T instance;
    private static readonly object lockObject = new object();

    protected Singleton()
    {
        // This constructor is protected to prevent instantiation from outside.
    }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
}
