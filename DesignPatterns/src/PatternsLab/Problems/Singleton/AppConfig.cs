namespace PatternsLab.Problems.Singleton;

public class AppConfig
{
    private static AppConfig? _instance;

    private static readonly object _lock = new();

    public static AppConfig Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new AppConfig();
                }

                return _instance;
            }
        }
    }

    public static int LoadCount;

    public string DbConnection { get; set; }
    public string Theme { get; set; }

    private AppConfig()
    {
        LoadCount++;

        Console.WriteLine(
            $"[AppConfig] Loading settings from disk... (load #{LoadCount})");

        Thread.Sleep(300);

        DbConnection = "Server=localhost;Db=School";
        Theme = "Light";
    }
}

public class DatabaseService
{
    public AppConfig Config { get; } = AppConfig.Instance;

    public void Connect() =>
        Console.WriteLine($"Connecting to {Config.DbConnection}");
}

public class UiService
{
    public AppConfig Config { get; } = AppConfig.Instance;

    public void Render() =>
        Console.WriteLine($"UI is using theme: {Config.Theme}");
}