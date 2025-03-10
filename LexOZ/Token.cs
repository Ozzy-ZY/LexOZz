namespace LexOZ;

public class Token
{
    public TokenType Type { get; }
    public string Literal { get; }
    public int Line { get; }
    public int Column { get; }
    
    public Token(TokenType type, string literal, int line, int column)
    {
        Type = type;
        Literal = literal;
        Line = line;
        Column = column;
    }
    
    public override string ToString() =>
        $"Token({Type}, '{Literal}', Line: {Line}, Column: {Column})";
}