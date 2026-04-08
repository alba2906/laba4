namespace Compiler.Search
{
    public class SearchResult
    {
        public string FoundText { get; set; } = string.Empty;
        public int StartIndex { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public int Length { get; set; }

        public string PositionDisplay => $"Строка {Line}, Символ {Column}";
    }
}