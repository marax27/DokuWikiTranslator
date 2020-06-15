namespace DokuWikiTranslator.Application.Scanner
{
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
            => $"<Token({Type}: '{Value}')>";
    }

    public enum TokenType
    {
        Marker,
        Text,
        Special,
        NewLine,
        Url,
        LineStart
    }
}
