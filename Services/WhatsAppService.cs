using WataniFTTH.Helpers;

namespace WataniFTTH.Services;

public class WhatsAppSendResult
{
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}

public class SendTarget
{
    public string CustomerName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string BundleName { get; set; } = string.Empty;
    public DateTime ExpiresLocal { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class WhatsAppService
{
    private readonly HttpClient _httpClient;

    public WhatsAppService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WhatsAppSendResult> SendAsync(
        string apiUrlTemplate,
        string phoneNumber,
        string customerName,
        string messageTemplate,
        string bundleName,
        DateTime expiresLocal,
        string? location = null,
        CancellationToken ct = default)
    {
        var result = new WhatsAppSendResult
        {
            CustomerName = customerName,
            Phone = phoneNumber
        };

        var formatted = PhoneHelper.FormatPhone(phoneNumber);
        if (string.IsNullOrEmpty(formatted))
        {
            result.Success = false;
            result.ErrorMessage = "رقم الهاتف غير متوفر";
            return result;
        }

        var message = BuildMessage(messageTemplate, customerName, bundleName, expiresLocal);
        var encodedMessage = Uri.EscapeDataString(message);
        var url = apiUrlTemplate
            .Replace("{phone}", formatted)
            .Replace("{message}", encodedMessage);

        if (!string.IsNullOrWhiteSpace(location))
            url += "&location=" + Uri.EscapeDataString(location);

        try
        {
            var response = await _httpClient.GetAsync(url, ct);
            result.Success = response.IsSuccessStatusCode;
            if (!result.Success)
                result.ErrorMessage = $"HTTP {(int)response.StatusCode}";
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public static string BuildMessage(
        string template, string customerName, string bundleName, DateTime expiresLocal)
    {
        var now = DateTime.UtcNow.AddHours(3);
        var diff = (expiresLocal - now).Duration();

        return template
            .Replace("%CustomerName%", customerName)
            .Replace("%Expiration%", "\u200E" + expiresLocal.ToString("yyyy-MM-dd hh:mm tt") + "\u200E")
            .Replace("%BundleName%", bundleName)
            .Replace("%يوم%", ((int)diff.TotalDays).ToString())
            .Replace("%ساعة%", diff.Hours.ToString())
            .Replace("%دقيقة%", diff.Minutes.ToString());
    }
}
