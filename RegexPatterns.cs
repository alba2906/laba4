namespace Compiler.Search
{
    public static class RegexPatterns
    {
        public const string HexColor =
            @"(?<![A-Za-z0-9_])#[0-9A-Fa-f]{6}(?![A-Za-z0-9_])";

        public const string Acronym =
            @"(?<![#A-Za-z0-9_])(?:[A-Z]{2,}|[A-Z](?:\.[A-Z])+\.(?![A-Z]))(?![A-Za-z0-9_])";

        public const string HtmlTagWithAttributes =
            @"<\s*[A-Za-z][A-Za-z0-9]*" +
            @"(?:\s+[A-Za-z_:][A-Za-z0-9_:\-\.]*\s*=\s*(?:""[^""]*""|'[^']*'))+" +
            @"\s*/?\s*>";
    }
}