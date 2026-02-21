using System.Text.Json;
using WataniFTTH.Models;

namespace WataniFTTH.Services;

public class CredentialManager
{
    private readonly string _filePath;
    private List<Credential> _credentials = new();

    public IReadOnlyList<Credential> Credentials => _credentials.AsReadOnly();

    public CredentialManager(string filePath)
    {
        _filePath = filePath;
        Load();
    }

    public void Load()
    {
        if (!File.Exists(_filePath))
        {
            _credentials = new List<Credential>();
            return;
        }

        var json = File.ReadAllText(_filePath);
        _credentials = JsonSerializer.Deserialize<List<Credential>>(json) ?? new List<Credential>();
    }

    public void Save()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(_credentials, options);
        File.WriteAllText(_filePath, json);
    }

    public void Add(Credential credential)
    {
        _credentials.Add(credential);
        Save();
    }

    public void Update(int index, Credential credential)
    {
        if (index >= 0 && index < _credentials.Count)
        {
            _credentials[index] = credential;
            Save();
        }
    }

    public void Delete(int index)
    {
        if (index >= 0 && index < _credentials.Count)
        {
            _credentials.RemoveAt(index);
            Save();
        }
    }
}
