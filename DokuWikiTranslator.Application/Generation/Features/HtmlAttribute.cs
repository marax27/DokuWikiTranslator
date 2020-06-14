namespace DokuWikiTranslator.Application.Generation.Features
{
    public readonly struct HtmlAttribute
    {
        public string Key { get; }
        public string? Value { get; }

        public HtmlAttribute(string key, string? value)
        {
            Key = key;
            Value = value;
        }

        public string GenerateCode()
            => $"{Key}" + (Value == null ? "" : $"='{Value}'");
    }
}
