using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text.Json;
using WataniFTTH.Models;

namespace WataniFTTH.Services;

public class CustomerService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;
    private readonly ConcurrentDictionary<string, string> _phoneCache = new();
    private readonly SemaphoreSlim _semaphore = new(2);
    private const string BaseUrl = "https://admin.ftth.iq/api/customers";

    public CustomerService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<string> GetPhoneAsync(string customerId, CancellationToken ct = default)
    {
        if (_phoneCache.TryGetValue(customerId, out var cached))
            return cached;

        await _semaphore.WaitAsync(ct);
        try
        {
            if (_phoneCache.TryGetValue(customerId, out cached))
                return cached;

            await _authService.EnsureTokenValidAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{customerId}");
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _authService.AccessToken);

            var response = await _httpClient.SendAsync(request, ct);
            if (!response.IsSuccessStatusCode)
            {
                _phoneCache[customerId] = string.Empty;
                return string.Empty;
            }

            var json = await response.Content.ReadAsStringAsync(ct);
            var detail = JsonSerializer.Deserialize<CustomerDetailResponse>(json);

            var phone = detail?.Model?.PrimaryContact?.Mobile;
            if (string.IsNullOrWhiteSpace(phone))
                phone = detail?.Model?.PrimaryContact?.SecondaryPhone;

            phone = phone?.Trim() ?? string.Empty;
            _phoneCache[customerId] = phone;
            return phone;
        }
        catch (OperationCanceledException) { throw; }
        catch
        {
            _phoneCache[customerId] = string.Empty;
            return string.Empty;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void ClearCache() => _phoneCache.Clear();
}
