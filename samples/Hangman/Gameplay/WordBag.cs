using System.Collections.Immutable;

namespace Hangman.Gameplay;

/// <summary>
/// Represents the word bag for the game.
/// </summary>
internal static class WordBag
{
    private readonly static Random Random = new();
    
    private readonly static ImmutableArray<string> Words = ImmutableArray.Create(
        "abstract", "assert", "boolean", "break", "byte", "case", "catch", "char", "class", "const", "continue",
        "create", "default", "do", "double", "else", "enum", "extends", "final", "finally", "float", "for",
        "foreach", "goto", "if", "implements", "import", "instanceof", "int", "interface", "long", "native",
        "operator", "package", "private", "protected", "public", "return", "short", "static", "strictfp", "super",
        "this", "throw", "throws", "transient", "try", "void", "volatile", "while"
    );
    
    /// <summary>
    /// Picks a random word from the word bag.
    /// </summary>
    /// <returns>A random word from the word bag.</returns>
    public static string Pick()
    {
        var index = Random.Next(0, Words.Length);
        return Words[index];
    }
}