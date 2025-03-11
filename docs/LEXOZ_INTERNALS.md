# LexOZ Lexer Internals

This document provides a technical overview of the LexOZ lexer implementation, explaining its internal architecture, algorithms, and design decisions.

## Architecture Overview

The LexOZ lexer is implemented in C# and follows a character-by-character scanning approach to tokenize source code. The lexer consists of three main components:

1. **Lexer Class** - The main class that performs lexical analysis
2. **Token Class** - Represents individual tokens with type, literal value, and position information
3. **TokenType Enum** - Defines all possible token types recognized by the lexer

## Lexical Analysis Process

The lexer works by reading input characters one at a time and converting them into tokens. The process follows these steps:

1. Initialize the lexer with input text
2. Read the first character
3. For each token to be recognized:
   - Skip whitespace
   - Identify the token type based on the current character
   - Read additional characters if needed (for multi-character tokens)
   - Create and return a token
4. Continue until the end of input is reached (EOF token)

## State Tracking

The lexer maintains several state variables to track its position in the input:

- `_input` - The complete input string
- `_position` - Current position (points to the character we just read)
- `_readPosition` - Position for the next character to be read
- `_ch` - Current character under examination
- `_line` - Current line number (for error reporting)
- `_column` - Current column number (for error reporting)

## Character Reading

The lexer uses two main methods for character handling:

- `ReadChar()` - Advances to the next character in the input, updating position tracking
- `PeekChar()` - Looks ahead at the next character without advancing the position

These methods handle boundary conditions like the end of input by returning the null character (`\0`).

## Token Recognition

The `NextToken()` method is the core of the lexer. It uses a switch statement to identify tokens based on the current character:

- Single-character tokens (like `+`, `-`, `*`) are recognized directly
- Multi-character tokens (like `==`, `!=`) use `PeekChar()` to look ahead
- Keywords and identifiers are recognized by reading a sequence of letters/digits
- Numbers are recognized by reading a sequence of digits
- Strings are recognized by reading characters between quotes
- Comments are recognized by detecting `//` and reading until end of line

### Identifier and Keyword Recognition

The lexer reads identifiers using the `ReadIdentifier()` method, which collects characters that are valid in an identifier (letters, digits, underscores). After reading an identifier, the lexer checks if it's a keyword using the `GetKeywordType()` method, which maps known keywords to their respective token types.

### Number Recognition

The `ReadNumber()` method reads a sequence of digits to form a number literal. The current implementation only supports integer literals.

### String Recognition

The `ReadString()` method reads characters between double quotes, handling escape sequences for embedded quotes (`\"`).

### Comment Recognition

The `ReadComment()` method reads characters after a `//` sequence until the end of the line, creating a COMMENT token.

## Error Handling

The lexer handles invalid or unexpected characters by creating ILLEGAL tokens with position information. This allows the parser or other consumers to report detailed error messages.

## Position Tracking

The lexer tracks line and column numbers to provide accurate position information for each token. This is especially useful for error reporting. The position is updated in the `ReadChar()` method, with special handling for newline characters to reset the column counter and increment the line counter.

## Performance Considerations

The lexer is designed for simplicity and clarity rather than maximum performance. Some performance optimizations that could be applied include:

- Using a buffer for reading characters instead of direct string indexing
- Implementing a more efficient keyword lookup (e.g., using a hash table)
- Adding specialized methods for common token sequences

## Extension Points

The lexer can be extended in several ways:

1. **Adding new token types** - Update the TokenType enum and add corresponding recognition logic
2. **Supporting new keywords** - Add entries to the GetKeywordType method
3. **Adding more complex tokens** - Implement new specialized reading methods
4. **Supporting additional literal types** - Add methods for reading different literal formats (e.g., floating-point numbers)

## Implementation Details

### Character Reading and Position Management

```csharp
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
```

This method advances the lexer's position in the input, handling the end of input and tracking line/column information.

### Token Creation

The lexer creates tokens with position information, allowing for precise error reporting:

```csharp
token = new Token(TokenType.IDENTIFIER, literal, _line, _column);
```

## Conclusion

The LexOZ lexer provides a clean, maintainable implementation of lexical analysis for a simple programming language. Its design prioritizes clarity and extensibility, making it suitable for educational purposes and as a foundation for more complex language processing tools.