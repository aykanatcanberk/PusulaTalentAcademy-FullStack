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

    // Test metodu
    public static void Test()
    {
        Console.WriteLine("=== Sesli Harf Alt Dizi Testleri ===");
        Console.WriteLine();

        // Test 1
        var test1 = new List<string> { "aeiou", "bcd", "aaa" };
        Console.WriteLine(" TEST 1:");
        Console.WriteLine($"Giriş: [\"aeiou\", \"bcd\", \"aaa\"]");
        Console.WriteLine($"Çıkış: {LongestVowelSubsequenceAsJson(test1)}");
        Console.WriteLine("Beklenen: [{\"word\":\"aeiou\",\"sequence\":\"aeiou\",\"length\":5},{\"word\":\"bcd\",\"sequence\":\"\",\"length\":0},{\"word\":\"aaa\",\"sequence\":\"aaa\",\"length\":3}]");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();

        // Test 2
        var test2 = new List<string> { "miscellaneous", "queue", "sky", "cooperative" };
        Console.WriteLine(" TEST 2:");
        Console.WriteLine($"Giriş: [\"miscellaneous\", \"queue\", \"sky\", \"cooperative\"]");
        Console.WriteLine($"Çıkış: {LongestVowelSubsequenceAsJson(test2)}");
        Console.WriteLine("Beklenen: [{\"word\":\"miscellaneous\",\"sequence\":\"eou\",\"length\":3},{\"word\":\"queue\",\"sequence\":\"ueue\",\"length\":4},{\"word\":\"sky\",\"sequence\":\"\",\"length\":0},{\"word\":\"cooperative\",\"sequence\":\"oo\",\"length\":2}]");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();

        // Test 3
        var test3 = new List<string> { "sequential", "beautifully", "rhythms", "encyclopaedia" };
        Console.WriteLine(" TEST 3:");
        Console.WriteLine($"Giriş: [\"sequential\", \"beautifully\", \"rhythms\", \"encyclopaedia\"]");
        Console.WriteLine($"Çıkış: {LongestVowelSubsequenceAsJson(test3)}");
        Console.WriteLine("Beklenen: [{\"word\":\"sequential\",\"sequence\":\"ue\",\"length\":2},{\"word\":\"beautifully\",\"sequence\":\"eau\",\"length\":3},{\"word\":\"rhythms\",\"sequence\":\"\",\"length\":0},{\"word\":\"encyclopaedia\",\"sequence\":\"ae\",\"length\":2}]");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();

        // Test 4
        var test4 = new List<string> { "algorithm", "education", "idea", "strength" };
        Console.WriteLine(" TEST 4:");
        Console.WriteLine($"Giriş: [\"algorithm\", \"education\", \"idea\", \"strength\"]");
        Console.WriteLine($"Çıkış: {LongestVowelSubsequenceAsJson(test4)}");
        Console.WriteLine("Beklenen: [{\"word\":\"algorithm\",\"sequence\":\"a\",\"length\":1},{\"word\":\"education\",\"sequence\":\"io\",\"length\":2},{\"word\":\"idea\",\"sequence\":\"ea\",\"length\":2},{\"word\":\"strength\",\"sequence\":\"e\",\"length\":1}]");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();

        // Test 5
        var test5 = new List<string>();
        Console.WriteLine(" TEST 5:");
        Console.WriteLine($"Giriş: []");
        Console.WriteLine($"Çıkış: {LongestVowelSubsequenceAsJson(test5)}");
        Console.WriteLine("Beklenen: []");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {

        VowelSubsequenceFinder.Test();
    }
}
