namespace LexOZ;

public enum TokenType
{
    // Keywords
    IF,
    ELSE,
    WHILE,
    FUNCTION,
    RETURN,
    VAR,
    
    // Literals
    IDENTIFIER,
    NUMBER,
    STRING,
    
    // Operators
    PLUS,
    MINUS,
    MULTIPLY,
    DIVIDE,
    ASSIGN,
    EQUALS,
    NOT_EQUALS,
    LESS_THAN,
    GREATER_THAN,
    
    // Delimiters
    LPAREN,
    RPAREN,
    LBRACE,
    RBRACE,
    SEMICOLON,
    COMMA,
    
    // Special tokens
    EOF,
    ILLEGAL,
    COMMENT
}