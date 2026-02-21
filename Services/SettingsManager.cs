using System.Text.Json;
using WataniFTTH.Models;

namespace WataniFTTH.Services;

public class SettingsManager
{
    private readonly string _filePath;
    public AppSettings Settings { get; private set; } = new();

    public SettingsManager(string filePath)
    {
        _filePath = filePath;
        Load();
    }

    public void Load()
    {
        if (!File.Exists(_filePath))
            return;

        try
        {
            var json = File.ReadAllText(_filePath);
            Settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
        catch
        {
            Settings = new AppSettings();
        }
    }

    public void Save()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(Settings, options);
        File.WriteAllText(_filePath, json);
    }
}
