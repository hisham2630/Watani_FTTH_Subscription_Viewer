namespace WataniFTTH.Helpers;

public static class PhoneHelper
{
    public static string FormatPhone(string? rawPhone)
    {
        if (string.IsNullOrWhiteSpace(rawPhone))
            return string.Empty;

        var phone = rawPhone.Trim();

        if (phone.StartsWith("964"))
            return phone;

        if (phone.StartsWith("0"))
            phone = phone[1..];

        return "964" + phone;
    }
}
