using System.Net.Http.Headers;
using System.Text.Json;
using WataniFTTH.Models;

namespace WataniFTTH.Services;

public class SubscriptionService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;
    private const string BaseUrl = "https://admin.ftth.iq/api/subscriptions";
    private const int PageSize = 150;
    private const int MaxRetries = 3;

    public SubscriptionService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<Subscription>> FetchAllAsync(
        string? status,
        DateTime? fromDate,
        DateTime? toDate,
        IProgress<(int current, int total, string message)>? progress = null,
        CancellationToken ct = default)
    {
        var allItems = new List<Subscription>();
        int pageNumber = 1;
        int totalCount = 0;
        int totalPages = 1;

        do
        {
            ct.ThrowIfCancellationRequested();
            await _authService.EnsureTokenValidAsync();

            var url = BuildUrl(pageNumber, status, fromDate, toDate);

            progress?.Report((pageNumber, totalPages,
                $"جاري تحميل الصفحة {pageNumber}..."));

            SubscriptionPage? page = null;
            string lastError = "";

            for (int retry = 0; retry <= MaxRetries; retry++)
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", _authService.AccessToken);

                    var response = await _httpClient.SendAsync(request, ct);

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await _authService.EnsureTokenValidAsync();
                        continue;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        lastError = $"HTTP {(int)response.StatusCode}: {await response.Content.ReadAsStringAsync(ct)}";
                        if (retry < MaxRetries)
                        {
                            await Task.Delay((int)Math.Pow(2, retry) * 1000, ct);
                            continue;
                        }
                        break;
                    }

                    var json = await response.Content.ReadAsStringAsync(ct);
                    page = JsonSerializer.Deserialize<SubscriptionPage>(json);
                    break;
                }
                catch (OperationCanceledException) { throw; }
                catch (Exception ex) when (retry < MaxRetries)
                {
                    lastError = ex.Message;
                    await Task.Delay((int)Math.Pow(2, retry) * 1000, ct);
                }
                catch (Exception ex)
                {
                    lastError = ex.Message;
                }
            }

            if (page == null)
                throw new Exception($"فشل تحميل الصفحة {pageNumber}: {lastError}");

            totalCount = page.TotalCount;
            allItems.AddRange(page.Items);
            totalPages = Math.Max(1, (int)Math.Ceiling((double)totalCount / PageSize));

            progress?.Report((pageNumber, totalPages,
                $"تم تحميل الصفحة {pageNumber} من {totalPages} ({allItems.Count}/{totalCount})"));

            pageNumber++;
        }
        while (allItems.Count < totalCount);

        return allItems;
    }

    public static string BuildUrl(int pageNumber, string? status, DateTime? fromDate, DateTime? toDate)
    {
        var url = $"{BaseUrl}?pageSize={PageSize}&pageNumber={pageNumber}" +
                  "&sortCriteria.property=expires&sortCriteria.direction=asc";

        if (!string.IsNullOrEmpty(status))
            url += $"&status={status}";

        if (fromDate.HasValue)
        {
            var utcFrom = fromDate.Value.Date.AddHours(-3);
            url += $"&fromExpirationDate={utcFrom:yyyy-MM-ddTHH:mm:ss.fffZ}";
        }

        if (toDate.HasValue)
        {
            var utcTo = toDate.Value.Date.AddDays(1).AddHours(-3).AddMilliseconds(-1);
            url += $"&toExpirationDate={utcTo:yyyy-MM-ddTHH:mm:ss.fffZ}";
        }

        url += "&hierarchyLevel=0";
        return url;
    }
}
