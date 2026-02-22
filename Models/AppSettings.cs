using System.Text.Json.Serialization;

namespace WataniFTTH.Models;

public class AppSettings
{
    [JsonPropertyName("whatsapp_api")]
    public string WhatsAppApi { get; set; } = "http://localhost:3111/send?number={phone}&message={message}";

    [JsonPropertyName("template_active")]
    public string TemplateActive { get; set; } = string.Empty;

    [JsonPropertyName("template_expired")]
    public string TemplateExpired { get; set; } = string.Empty;

    [JsonPropertyName("last_credential")]
    public string LastCredential { get; set; } = string.Empty;

    [JsonPropertyName("location_enabled")]
    public bool LocationEnabled { get; set; } = true;

    [JsonPropertyName("location_coords")]
    public string LocationCoords { get; set; } = string.Empty;

    [JsonPropertyName("allowed_suffixes")]
    public List<string> AllowedSuffixes { get; set; } = new()
    {
        "NOMMOR", "NOMOO", "NOMOON", "NOMOOR", "NOMOORD", "NOMOR"
    };

    [JsonPropertyName("blocked_suffixes")]
    public List<string> BlockedSuffixes { get; set; } = new()
    {
        "ALZAEIM", "ALZAIEM", "ALZIAEM", "DUKAN", "GNEL",
        "GNRL", "MUS", "OM", "TMT", "VAIO", "ZAIN"
    };

    [JsonPropertyName("filter_tab_index")]
    public int FilterTabIndex { get; set; } = 1;
}
