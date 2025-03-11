# LexOZ Developer Guide

This guide explains how to extend the LexOZ lexer with new features. It provides step-by-step instructions for implementing new keywords, operators, and other language features. The guide is designed for developers who want to understand the lexer's architecture and contribute to its development.

## Table of Contents

1. [Understanding the Lexer Architecture](#understanding-the-lexer-architecture)
2. [Adding a New Keyword](#adding-a-new-keyword)
3. [Adding a New Operator](#adding-a-new-operator)
4. [Supporting New Literal Types](#supporting-new-literal-types)
5. [Best Practices](#best-practices)

## Understanding the Lexer Architecture

The LexOZ lexer consists of three main components:

1. **TokenType.cs** - An enum that defines all possible token types
2. **Token.cs** - A class that represents individual tokens with type, literal value, and position information
3. **Lexer.cs** - The main class that performs lexical analysis

To extend the lexer, you'll typically need to modify one or more of these files.

## Adding a New Keyword

Let's walk through the process of adding a new keyword, such as `const`, to the lexer.

### Step 1: Add the Keyword to TokenType.cs

First, add a new entry to the `TokenType` enum in `TokenType.cs`:

```csharp
public enum TokenType
{
    // Keywords
    IF,
    ELSE,
    WHILE,
    FUNCTION,
    RETURN,
    VAR,
    CONST,  // New keyword added here
    
    // Rest of the enum...
}
```

### Step 2: Update the Keyword Recognition in Lexer.cs

Next, update the `GetKeywordType` method in `Lexer.cs` to recognize the new keyword:

```csharp
private static TokenType GetKeywordType(string identifier) => identifier switch
{
    "if" => TokenType.IF,
    "else" => TokenType.ELSE,
    "while" => TokenType.WHILE,
    "function" => TokenType.FUNCTION,
    "return" => TokenType.RETURN,
    "var" => TokenType.VAR,
    "const" => TokenType.CONST,  // New keyword added here
    _ => TokenType.IDENTIFIER
};
```

### Step 3: Test the New Keyword

Create a test file (e.g., `test.Loz`) with code that uses your new keyword:

```
const x = 10;
function test() {
    return x + 5;
}
```

Run the lexer on this file and verify that it correctly identifies `const` as a keyword rather than an identifier.

## Adding a New Operator

Let's walk through the process of adding a new operator, such as the modulo operator (`%`).

### Step 1: Add the Operator to TokenType.cs

Add a new entry to the `TokenType` enum in `TokenType.cs`:

```csharp
public enum TokenType
{
    // Keywords...
    
    // Operators
    PLUS,
    MINUS,
    MULTIPLY,
    DIVIDE,
    MODULO,    // New operator added here
    ASSIGN,
    // Rest of the enum...
}
```

### Step 2: Update the Token Recognition in Lexer.cs

Modify the `NextToken` method in `Lexer.cs` to recognize the new operator:

```csharp
public Token NextToken()
{
    Token token;
    
    SkipWhitespace();
    
    switch (_ch)
    {
        // Existing cases...
        
        case '%':
            token = new Token(TokenType.MODULO, _ch.ToString(), _line, _column);
            break;
            
        // Rest of the switch statement...
    }
    
    ReadChar();
    return token;
}
```

### Step 3: Test the New Operator

Create a test file with code that uses your new operator:

```
var x = 10 % 3;
```

Run the lexer on this file and verify that it correctly identifies `%` as a modulo operator.

## Supporting New Literal Types

Let's walk through the process of adding support for a new literal type, such as floating-point numbers.

### Step 1: Add the Literal Type to TokenType.cs

Add a new entry to the `TokenType` enum in `TokenType.cs` if needed (in this case, we might want to distinguish between integer and float literals):

```csharp
public enum TokenType
{
    // Keywords...
    
    // Literals
    IDENTIFIER,
    NUMBER,
    FLOAT,      // New literal type added here
    STRING,
    
    // Rest of the enum...
}
```

### Step 2: Add a Method to Read the New Literal Type

Add a new method to `Lexer.cs` to read the new literal type:

```csharp
private string ReadFloat()
{
    var startPosition = _position;
    
    // Read the integer part
    while (char.IsDigit(_ch))
    {
        ReadChar();
    }
    
    // Read the decimal point
    if (_ch == '.')
    {
        ReadChar();
        
        // Read the fractional part
        while (char.IsDigit(_ch))
        {
            ReadChar();
        }
    }
    
    return _input[startPosition.._position];
}
```

### Step 3: Update the Token Recognition in Lexer.cs

Modify the `NextToken` method in `Lexer.cs` to use the new method:

```csharp
public Token NextToken()
{
    // Existing code...
    
    default:
        if (char.IsLetter(_ch) || _ch == '_')
        {
            // Existing code for identifiers...
        }
        else if (char.IsDigit(_ch))
        {
            // Check if it's a float or an integer
            var startPosition = _position;
            var literal = ReadNumber();
            
            if (_ch == '.')
            {
                // It's a float
                var integerPart = literal;
                ReadChar(); // Skip the decimal point
                
                var fractionalStartPosition = _position;
                while (char.IsDigit(_ch))
                {
                    ReadChar();
                }
                
                var fractionalPart = _input[fractionalStartPosition.._position];
                literal = $"{integerPart}.{fractionalPart}";
                
                return new Token(TokenType.FLOAT, literal, _line, _column);
            }
            
            return new Token(TokenType.NUMBER, literal, _line, _column);
        }
        else
        {
            // Existing code for illegal tokens...
        }
        break;
}
```

### Step 4: Test the New Literal Type

Create a test file with code that uses your new literal type:

```
var x = 10.5;
```

Run the lexer on this file and verify that it correctly identifies `10.5` as a float literal.

## Best Practices

### 1. Maintain Backward Compatibility

When adding new features, ensure that existing code continues to work as expected. This is especially important if your lexer is used in production environments.

### 2. Write Tests

Always write tests for new features to ensure they work correctly and don't break existing functionality. Test both valid and invalid inputs to ensure proper error handling.

### 3. Document Your Changes

Update the documentation to reflect your changes. This includes updating the README.md file and adding comments to your code.

### 4. Follow Coding Conventions

Follow the existing coding conventions in the project to maintain consistency. This includes naming conventions, code formatting, and architectural patterns.

### 5. Keep It Simple
Avoid adding complex logic or features that are not necessary for your language. Keep the lexer simple and maintainable.

## Conclusion

Extending the LexOZ lexer is a straightforward process that involves updating the token types, adding recognition logic, and testing the new features. By following the steps outlined in this guide, you can add new keywords, operators, and literal types to the lexer to support your language's syntax requirements.