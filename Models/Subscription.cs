using System.Text.Json.Serialization;

namespace WataniFTTH.Models;

public class SubscriptionPage
{
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("items")]
    public List<Subscription> Items { get; set; } = new();
}

public class Subscription
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("autoRenew")]
    public bool AutoRenew { get; set; }

    [JsonPropertyName("partner")]
    public IdDisplayValue? Partner { get; set; }

    [JsonPropertyName("salesType")]
    public EntityRef? SalesType { get; set; }

    [JsonPropertyName("services")]
    public List<ServiceItem> Services { get; set; } = new();

    [JsonPropertyName("customer")]
    public IdDisplayValue? Customer { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("bundleId")]
    public string BundleId { get; set; } = string.Empty;

    [JsonPropertyName("bundle")]
    public IdDisplayValue? Bundle { get; set; }

    [JsonPropertyName("zone")]
    public IdDisplayValue? Zone { get; set; }

    [JsonPropertyName("expires")]
    public DateTime Expires { get; set; }

    [JsonPropertyName("commitmentPeriod")]
    public int CommitmentPeriod { get; set; }

    [JsonPropertyName("hasActiveSession")]
    public bool HasActiveSession { get; set; }

    [JsonPropertyName("macAddress")]
    public string? MacAddress { get; set; }

    [JsonPropertyName("ipAddress")]
    public string? IpAddress { get; set; }

    [JsonPropertyName("isSuspended")]
    public bool IsSuspended { get; set; }

    [JsonPropertyName("suspensionReason")]
    public string? SuspensionReason { get; set; }

    [JsonPropertyName("lineOfBusiness")]
    public EntityRef? LineOfBusiness { get; set; }

    [JsonPropertyName("startedAt")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("activeSession")]
    public ActiveSession? ActiveSession { get; set; }

    [JsonPropertyName("self")]
    public IdDisplayValue? Self { get; set; }

    // UTC+3 conversion
    [JsonIgnore]
    public DateTime ExpiresLocal => Expires.AddHours(3);

    // Base service name (e.g., "FIBER 35")
    [JsonIgnore]
    public string BaseServiceName =>
        Services.FirstOrDefault(s => s.ProductType?.DisplayValue == "Base")?.DisplayValue ?? string.Empty;

    // Phone number (populated after customer API call)
    [JsonIgnore]
    public string PhoneNumber { get; set; } = string.Empty;
}

public class IdDisplayValue
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("displayValue")]
    public string DisplayValue { get; set; } = string.Empty;
}

public class EntityRef
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("entityType")]
    public string EntityType { get; set; } = string.Empty;

    [JsonPropertyName("displayValue")]
    public string DisplayValue { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

public class ServiceItem
{
    [JsonPropertyName("type")]
    public EntityRef? Type { get; set; }

    [JsonPropertyName("productType")]
    public EntityRef? ProductType { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("displayValue")]
    public string DisplayValue { get; set; } = string.Empty;
}

public class ActiveSession
{
    [JsonPropertyName("startedAt")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("sessionTimeInSeconds")]
    public long SessionTimeInSeconds { get; set; }

    [JsonPropertyName("ipAddress")]
    public string? IpAddress { get; set; }

    [JsonPropertyName("macAddress")]
    public string? MacAddress { get; set; }
}
