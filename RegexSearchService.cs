using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Compiler.Search
{
    public static class RegexSearchService
    {
        public static List<SearchResult> FindAll(string text, SearchType searchType)
        {
            if (string.IsNullOrEmpty(text))
                return new List<SearchResult>();

            return searchType switch
            {
                SearchType.HexColor => FindByRegex(text, RegexPatterns.HexColor),
                SearchType.AcronymRegex => FindByRegex(
    text,
    @"(?<![#A-Za-z0-9_])(?:[A-Z]{2,}|[A-Z](?:\.[A-Z])+\.(?![A-Z]))(?![A-Za-z0-9_])"
),
                SearchType.HtmlTagWithAttributes => FindByRegex(text, RegexPatterns.HtmlTagWithAttributes),
                SearchType.AcronymAutomaton => AcronymAutomatonSearch.FindAll(text),
                _ => new List<SearchResult>()
            };
        }

        public static List<SearchResult> FindByRegex(string text, string pattern)
        {
            var results = new List<SearchResult>();

            foreach (Match match in Regex.Matches(text, pattern))
            {
                var (line, column) = GetLineColumn(text, match.Index);

                results.Add(new SearchResult
                {
                    FoundText = match.Value,
                    StartIndex = match.Index,
                    Line = line,
                    Column = column,
                    Length = match.Length
                });
            }

            return results;
        }

        public static (int line, int column) GetLineColumn(string text, int index)
        {
            int line = 1;
            int column = 1;

            for (int i = 0; i < index && i < text.Length; i++)
            {
                if (text[i] == '\n')
                {
                    line++;
                    column = 1;
                }
                else
                {
                    column++;
                }
            }

            return (line, column);
        }
    }
}