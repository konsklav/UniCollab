using System.Text.RegularExpressions;

namespace Saas.Domain.Common;

internal static partial class SlugHelper
{
    public static string Get(string text)
    {
        var lower = text.ToLowerInvariant().Trim();
        lower = SpecialCharsRegex().Replace(lower, string.Empty);
        lower = WhitespaceRegex().Replace(lower, "-");

        return lower;
    }

    [GeneratedRegex(@"[^a-z0-9\s-]")]
    private static partial Regex SpecialCharsRegex();
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
}