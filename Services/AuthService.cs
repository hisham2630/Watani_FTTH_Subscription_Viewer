using System.Net.Security;
using System.Text.Json;
using WataniFTTH.Models;

namespace WataniFTTH.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private const string TokenUrl = "https://admin.ftth.iq/api/auth/Contractor/token";

    private string _accessToken = string.Empty;
    private DateTime _tokenObtainedAt;
    private string _currentUsername = string.Empty;
    private string _currentPassword = string.Empty;
    private string _lastError = string.Empty;

    public string AccessToken => _accessToken;
    public bool IsAuthenticated => !string.IsNullOrEmpty(_accessToken);
    public string LastError => _lastError;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static HttpClient CreateHttpClient()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
        };
        var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(30) };
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        return client;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        _currentUsername = username;
        _currentPassword = password;
        _lastError = string.Empty;

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("scope", "openid profile"),
            new KeyValuePair<string, string>("client_id", ""),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        });

        try
        {
            var response = await _httpClient.PostAsync(TokenUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                _lastError = $"HTTP {(int)response.StatusCode}: {errorBody}";
                return false;
            }

            var json = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(json);

            if (authResponse == null || string.IsNullOrEmpty(authResponse.AccessToken))
            {
                _lastError = "الرد لا يحتوي على رمز وصول صالح";
                return false;
            }

            _accessToken = authResponse.AccessToken;
            _tokenObtainedAt = DateTime.UtcNow;
            return true;
        }
        catch (HttpRequestException ex)
        {
            _lastError = $"خطأ في الاتصال: {ex.Message}";
            return false;
        }
        catch (TaskCanceledException)
        {
            _lastError = "انتهت مهلة الاتصال بالخادم";
            return false;
        }
        catch (Exception ex)
        {
            _lastError = $"خطأ غير متوقع: {ex.Message}";
            return false;
        }
    }

    public async Task<bool> EnsureTokenValidAsync()
    {
        if (string.IsNullOrEmpty(_accessToken))
            return false;

        if ((DateTime.UtcNow - _tokenObtainedAt).TotalSeconds > 3500)
        {
            return await LoginAsync(_currentUsername, _currentPassword);
        }

        return true;
    }
}
