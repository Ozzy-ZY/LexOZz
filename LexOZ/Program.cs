using System;
using System.IO;

namespace LexOZ;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: LexOZ <file.loz>");
            Console.WriteLine("Please provide a path to a .loz source file.");
            return;
        }

        string filePath = args[0];
        
        // Validate file extension
        if (!filePath.EndsWith(".Loz", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Error: File must have a .Loz extension.");
            return;
        }

        try
        {
            // Check if file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' not found.");
                return;
            }

            // Read the source code from the file
            string source = File.ReadAllText(filePath);
            Console.WriteLine($"Lexing file: {filePath}");
            Console.WriteLine("====================");
            
            // Create lexer and tokenize the source
            var lexer = new Lexer(source);
            Token token;
            int tokenCount = 0;
            
            do
            {
                token = lexer.NextToken();
                Console.WriteLine(token);
                tokenCount++;
            } while (token.Type != TokenType.EOF);
            
            Console.WriteLine("====================");
            Console.WriteLine($"Tokenization complete. Found {tokenCount} tokens.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}