using System.Text.Json.Serialization;

namespace WataniFTTH.Models;

public class AuthResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public double ExpiresIn { get; set; }

    [JsonPropertyName("refresh_expires_in")]
    public double RefreshExpiresIn { get; set; }

    [JsonPropertyName("second_factor_required")]
    public bool SecondFactorRequired { get; set; }
}
