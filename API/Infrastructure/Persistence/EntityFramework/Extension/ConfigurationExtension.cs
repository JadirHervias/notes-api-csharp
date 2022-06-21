using System.Globalization;

namespace API.Infrastructure.Persistence.EntityFramework.Extension;

public static class ConfigurationExtension
{
    private static readonly Func<char, string> AddUnderscoreBeforeCapitalLetter =
        x => char.IsUpper(x) ? "_" + x : x.ToString(CultureInfo.InvariantCulture);

    public static string ToDatabaseFormat(this string value)
    {
        return string.Concat(value.Select(AddUnderscoreBeforeCapitalLetter))[1..].ToLowerInvariant();
    }
}
