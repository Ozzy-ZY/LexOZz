namespace LexOZ;

public enum TokenType
{
    // Keywords - language reserved words
    IF,
    ELSE,
    WHILE,
    FUNCTION,
    RETURN,
    VAR,
    
    // Literals - values and identifiers
    IDENTIFIER,
    NUMBER,
    STRING,
    TRUE,
    FALSE,
    
    // Operators - mathematical and comparison operations
    PLUS,
    MINUS,
    MULTIPLY,
    DIVIDE,
    ASSIGN,
    EQUALS,
    NOT_EQUALS,
    LESS_THAN,
    GREATER_THAN,
    AND,
    OR,        
    NOT,        
    
    // Delimiters - structural elements that define code blocks and statements
    LPAREN,
    RPAREN,
    LBRACE,
    RBRACE,
    SEMICOLON,
    COMMA,
    
    // Special tokens - control and meta tokens
    EOF,        // End of file marker
    ILLEGAL,    // Invalid or unexpected character
    COMMENT     // Source code comment
}