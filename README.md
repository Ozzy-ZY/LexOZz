# LexOZ Lexer

LexOZ is a lexical analyzer (lexer) for a simple programming language. It transforms source code text into a sequence of tokens that can be used by a parser to build an abstract syntax tree.

## Features

- Tokenizes source code into meaningful tokens
- Tracks line and column numbers for error reporting
- Supports various token types including keywords, operators, and literals
- Handles comments and string literals with escape sequences

## Usage

### Command Line

You can use the LexOZ lexer from the command line to tokenize a source file:

```bash
dotnet run --project LexOz path/to/file.loz
```

This will output all tokens found in the file along with their positions.

### Programmatic Usage

To use the lexer in your own code:

```csharp
using LexOZ;

// Create a lexer with your source code
string sourceCode = "var x = 10; function test() { return x + 5; }";
var lexer = new Lexer(sourceCode);

// Retrieve tokens one by one
Token token;
do {
    token = lexer.NextToken();
    Console.WriteLine(token);
} while (token.Type != TokenType.EOF);
```

## Language Syntax

The LexOZ lexer recognizes the following elements:

### Keywords

- `if` - Conditional statements
- `else` - Alternative branch for conditionals
- `while` - Loop construct
- `function` - Function declaration
- `return` - Return statement
- `var` - Variable declaration

### Operators

- `+` - Addition
- `-` - Subtraction
- `*` - Multiplication
- `/` - Division
- `=` - Assignment
- `==` - Equality comparison
- `!=` - Inequality comparison
- `<` - Less than
- `>` - Greater than

### Delimiters

- `(` and `)` - Parentheses for grouping expressions and function calls
- `{` and `}` - Braces for code blocks
- `;` - Statement terminator
- `,` - Separator for function arguments and other lists

### Literals

- Identifiers - Names starting with a letter or underscore, can contain letters, digits, and underscores
- Numbers - Integer literals
- Strings - Text enclosed in double quotes with support for escaped quotes (`\"`) 

### Comments

- Line comments start with `//` and continue to the end of the line

## Example Code

```
// This is a sample LexOZ language file

function factorial(n) {
    if (n == 0) {
        return 1;
    } else {
        return n * factorial(n - 1);
    }
}

function main() {
    // Calculate factorial of 5
    var result = factorial(5);
    
    // Print the result
    while (result > 0) {
        result = result - 1;
    }
    
    // Test string literals
    var message = "Hello, LexOZ!";
    
    return 0;
}
```

## Error Handling

The lexer provides detailed error information through the `ILLEGAL` token type. When an unrecognized character is encountered, an illegal token is generated with the line and column information to help locate the error in the source code.

## License

This project is available for educational and personal use.
