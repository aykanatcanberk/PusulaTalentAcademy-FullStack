using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class VowelSubsequenceFinder
{
    private static readonly HashSet<char> Vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0)
        {
            return "[]";
        }

        var results = new List<VowelResult>();

        foreach (var word in words)
        {
            if (string.IsNullOrEmpty(word))
            {
                results.Add(new VowelResult { Word = word, Sequence = "", Length = 0 });
                continue;
            }

            string longestSequence = FindLongestVowelSubsequence(word);
            results.Add(new VowelResult
            {
                Word = word,
                Sequence = longestSequence,
                Length = longestSequence.Length
            });
        }

        return JsonSerializer.Serialize(results);
    }

    private static string FindLongestVowelSubsequence(string word)
    {
        string longestSequence = "";
        string currentSequence = "";

        foreach (char c in word)
        {
            if (IsVowel(c))
            {
                currentSequence += c;
            }
            else
            {
                if (currentSequence.Length > longestSequence.Length)
                {
                    longestSequence = currentSequence;
                }
                currentSequence = "";
            }
        }

        if (currentSequence.Length > longestSequence.Length)
        {
            longestSequence = currentSequence;
        }

        return longestSequence;
    }

    private static bool IsVowel(char c)
    {
        return Vowels.Contains(c);
    }

    private class VowelResult
    {
        public string Word { get; set; }
        public string Sequence { get; set; }
        public int Length { get; set; }
    }
}