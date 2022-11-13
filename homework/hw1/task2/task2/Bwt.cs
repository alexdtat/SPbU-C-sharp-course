using System.Text;

namespace task2;

/// <summary>
/// Class implementing Burrows–Wheeler Transformation for given string.
/// </summary>
public static class Bwt
{
    /// <summary>
    /// Direct Burrows–Wheeler Transformation for a string.
    /// </summary>
    /// <param name="encodedText">Encoded text.</param>
    /// <returns>A pair of encoded text and end of string position.</returns>
    public static (string transformedText, int position) DirectTransformation(string encodedText)
    {
        var textForRotations = encodedText + encodedText;
        var suffixArray = GetSuffixPositionsArray(textForRotations);
        var resultText = new StringBuilder(encodedText.Length);
        var endPosition = 0;

        for (var i = 0; i < textForRotations.Length; i++)
        {
            if (suffixArray[i] >= encodedText.Length)
            {
                continue;
            }

            var index = suffixArray[i] - 1;
            if (index == -1)
            {
                index = encodedText.Length - 1;
                endPosition = i / 2;
            }

            resultText.Append(encodedText[index]);
        }

        return (resultText.ToString(), endPosition);
    }

    /// <summary>
    /// Reverse Burrows–Wheeler Transformation for a string.
    /// </summary>
    /// <param name="decodedText">Decoded text.</param>
    /// <param name="endPosition">End of string position after encoding.</param>
    /// <returns>Decoded text.</returns>
    public static string ReverseTransformation(string decodedText, int endPosition)
    {
        var result = new StringBuilder(decodedText.Length);
        var symbols = string.Join("", decodedText
            .Distinct()
            .OrderBy(symbol => symbol));
        var countSymbolsArray = new int[symbols.Length];
        foreach (var symbol in decodedText)
        {
            countSymbolsArray[symbols.IndexOf(symbol)] += 1;
        }

        var charPositionsInSortArray = new int[decodedText.Length];
        for (var j = 0; j < decodedText.Length; j++)
        {
            charPositionsInSortArray[j] = countSymbolsArray.Take(
                                                  symbols
                                                      .IndexOf(decodedText[j]))
                                              .Sum() +
                                          decodedText
                                              .Take(j)
                                              .Count(x => x == decodedText[j]
                                              );
        }

        var currentPosition = endPosition;
        while (result.Length != decodedText.Length)
        {
            result.Insert(0, decodedText[currentPosition]);
            currentPosition = charPositionsInSortArray[currentPosition];
        }

        return result.ToString();
    }

    /// <summary>
    /// Getting array of suffixes' positions after sorting them lexicographically.
    /// </summary>
    /// <param name="text">Text for getting suffixes from.</param>
    /// <returns>Array of lexicographically sorted suffixes' positions.</returns>
    private static int[] GetSuffixPositionsArray(string text)
    {
        var suffixPositionsArray = Enumerable
            .Range(0, text.Length)
            .ToArray();
        Array.Sort(suffixPositionsArray,
            (i, j) => string.Compare(text[i..], text[j..], StringComparison.Ordinal));

        return suffixPositionsArray;
    }
}
