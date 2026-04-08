using System.Collections.Generic;

namespace Compiler.Search
{
    public static class AcronymAutomatonSearch
    {
        public static List<SearchResult> FindAll(string text)
        {
            var results = new List<SearchResult>();

            if (string.IsNullOrEmpty(text))
                return results;

            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsUpper(text[i]))
                    continue;

                int dottedLength = TryReadDottedAcronym(text, i);
                if (dottedLength > 0)
                {
                    AddResult(results, text, i, dottedLength);
                    i += dottedLength - 1;
                    continue;
                }

                int plainLength = TryReadPlainAcronym(text, i);
                if (plainLength > 0)
                {
                    AddResult(results, text, i, plainLength);
                    i += plainLength - 1;
                }
            }

            return results;
        }

        private static void AddResult(List<SearchResult> results, string text, int start, int length)
        {
            var (line, column) = RegexSearchService.GetLineColumn(text, start);

            results.Add(new SearchResult
            {
                FoundText = text.Substring(start, length),
                StartIndex = start,
                Line = line,
                Column = column,
                Length = length
            });
        }

        private static int TryReadPlainAcronym(string text, int start)
        {
            if (!char.IsUpper(text[start]))
                return 0;

            if (start > 0 && IsWordChar(text[start - 1]))
                return 0;

            int i = start;
            while (i < text.Length && char.IsUpper(text[i]))
                i++;

            int length = i - start;

            if (length < 2)
                return 0;

            if (i < text.Length && IsWordChar(text[i]))
                return 0;

            return length;
        }

        private static int TryReadDottedAcronym(string text, int start)
        {
            if (!char.IsUpper(text[start]))
                return 0;

            if (start > 0 && IsWordChar(text[start - 1]))
                return 0;

            int i = start;
            int pairs = 0;

            while (i + 1 < text.Length && char.IsUpper(text[i]) && text[i + 1] == '.')
            {
                pairs++;
                i += 2;
            }

            if (pairs < 2)
                return 0;

            if (i < text.Length && IsWordChar(text[i]))
                return 0;

            return i - start;
        }

        private static bool IsWordChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }
    }
}