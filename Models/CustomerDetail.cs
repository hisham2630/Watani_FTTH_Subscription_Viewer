using System.Text.Json.Serialization;

namespace WataniFTTH.Models;

public class CustomerDetailResponse
{
    [JsonPropertyName("model")]
    public CustomerModel? Model { get; set; }
}

public class CustomerModel
{
    [JsonPropertyName("primaryContact")]
    public PrimaryContact? PrimaryContact { get; set; }

    [JsonPropertyName("self")]
    public IdDisplayValue? Self { get; set; }
}

public class PrimaryContact
{
    [JsonPropertyName("self")]
    public IdDisplayValue? Self { get; set; }

    [JsonPropertyName("mobile")]
    public string? Mobile { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("secondaryPhone")]
    public string? SecondaryPhone { get; set; }
}
