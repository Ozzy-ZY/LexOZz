namespace LexOZ;

public class Lexer
{
    private readonly string _input;
    private int _position;
    private int _readPosition;
    private char _ch;
    private int _line = 1;
    private int _column = 0;
    
    public Lexer(string input)
    {
        _input = input;
        ReadChar(); // Initialize first character
    }
    
    private void ReadChar()
    {
        if (_readPosition >= _input.Length)
        {
            _ch = '\0';
        }
        else
        {
            _ch = _input[_readPosition];
        }
        
        _position = _readPosition;
        _readPosition++;
        _column++;
        
        if (_ch == '\n')
        {
            _line++;
            _column = 0;
        }
    }
    
    private char PeekChar() =>
        _readPosition >= _input.Length ? '\0' : _input[_readPosition];
    
    private void SkipWhitespace()
    {
        while (char.IsWhiteSpace(_ch))
        {
            ReadChar();
        }
    }
    
    private string ReadIdentifier()
    {
        var startPosition = _position;
        while (char.IsLetter(_ch) || _ch == '_' || char.IsDigit(_ch)) // Allow digits in identifiers after first char
        {
            ReadChar();
        }
        return _input[startPosition.._position];
    }
    
    private string ReadNumber()
    {
        var startPosition = _position;
        while (char.IsDigit(_ch))
        {
            ReadChar();
        }
        return _input[startPosition.._position];
    }
    
    private string ReadString()
    {
        ReadChar(); // Skip the opening quote
        var startPosition = _position;
        
        while (_ch != '"' && _ch != '\0')
        {
            // Handle escape sequences
            if (_ch == '\\' && PeekChar() == '"')
            {
                ReadChar(); // Skip the backslash
            }
            ReadChar();
        }
        
        var str = _input[startPosition.._position];
        ReadChar(); // Skip the closing quote
        return str;
    }
    
    private Token ReadComment()
    {
        var startPosition = _position;
        ReadChar(); // Skip the second slash
        
        while (_ch != '\n' && _ch != '\0')
        {
            ReadChar();
        }
        
        var comment = _input[startPosition.._position];
        return new Token(TokenType.COMMENT, comment, _line, _column);
    }
    
    public Token NextToken()
    {
        Token token;
        
        SkipWhitespace();
        
        switch (_ch)
        {
            case '=':
                if (PeekChar() == '=')
                {
                    var ch = _ch;
                    ReadChar();
                    token = new Token(TokenType.EQUALS, $"{ch}{_ch}", _line, _column);
                }
                else
                {
                    token = new Token(TokenType.ASSIGN, _ch.ToString(), _line, _column);
                }
                break;
            case '+':
                token = new Token(TokenType.PLUS, _ch.ToString(), _line, _column);
                break;
            case '-':
                token = new Token(TokenType.MINUS, _ch.ToString(), _line, _column);
                break;
            case '*':
                token = new Token(TokenType.MULTIPLY, _ch.ToString(), _line, _column);
                break;
            case '/':
                if (PeekChar() == '/')
                {
                    return ReadComment();
                }
                else
                {
                    token = new Token(TokenType.DIVIDE, _ch.ToString(), _line, _column);
                }
                break;
            case '!':
                if (PeekChar() == '=')
                {
                    var ch = _ch;
                    ReadChar();
                    token = new Token(TokenType.NOT_EQUALS, $"{ch}{_ch}", _line, _column);
                }
                else
                {
                    token = new Token(TokenType.ILLEGAL, _ch.ToString(), _line, _column);
                }
                break;
            case '<':
                token = new Token(TokenType.LESS_THAN, _ch.ToString(), _line, _column);
                break;
            case '>':
                token = new Token(TokenType.GREATER_THAN, _ch.ToString(), _line, _column);
                break;
            case '(':
                token = new Token(TokenType.LPAREN, _ch.ToString(), _line, _column);
                break;
            case ')':
                token = new Token(TokenType.RPAREN, _ch.ToString(), _line, _column);
                break;
            case '{':
                token = new Token(TokenType.LBRACE, _ch.ToString(), _line, _column);
                break;
            case '}':
                token = new Token(TokenType.RBRACE, _ch.ToString(), _line, _column);
                break;
            case ';':
                token = new Token(TokenType.SEMICOLON, _ch.ToString(), _line, _column);
                break;
            case ',':
                token = new Token(TokenType.COMMA, _ch.ToString(), _line, _column);
                break;
            case '"':
                var stringLiteral = ReadString();
                return new Token(TokenType.STRING, stringLiteral, _line, _column);
            case '\0':
                token = new Token(TokenType.EOF, "", _line, _column);
                break;
            default:
                if (char.IsLetter(_ch) || _ch == '_')
                {
                    var literal = ReadIdentifier();
                    var type = GetKeywordType(literal);
                    return new Token(type, literal, _line, _column);
                }
                else if (char.IsDigit(_ch))
                {
                    var literal = ReadNumber();
                    return new Token(TokenType.NUMBER, literal, _line, _column);
                }
                else
                {
                    token = new Token(TokenType.ILLEGAL, _ch.ToString(), _line, _column);
                }
                break;
        }
        
        ReadChar();
        return token;
    }
    
    private static TokenType GetKeywordType(string identifier) => identifier switch
    {
        "if" => TokenType.IF,
        "else" => TokenType.ELSE,
        "while" => TokenType.WHILE,
        "function" => TokenType.FUNCTION,
        "return" => TokenType.RETURN,
        "var" => TokenType.VAR,
        _ => TokenType.IDENTIFIER
    };
}