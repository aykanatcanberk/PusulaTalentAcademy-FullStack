using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class MaxIncreasingSubarray
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            return JsonSerializer.Serialize(new List<int>());
        }

        if (numbers.Count == 1)
        {
            return JsonSerializer.Serialize(numbers);
        }

        List<int> maxSubarray = new List<int>();
        List<int> currentSubarray = new List<int> { numbers[0] };
        int maxSum = numbers[0];
        int currentSum = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            // Eğer mevcut eleman bir öncekinden büyükse, artan diziyi devam ettir
            if (numbers[i] > numbers[i - 1])
            {
                currentSubarray.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSubarray = new List<int>(currentSubarray);
                }

                // Yeni dizi
                currentSubarray = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

        // Son diziyi de kontrol et
        if (currentSum > maxSum)
        {
            maxSubarray = new List<int>(currentSubarray);
        }

        return JsonSerializer.Serialize(maxSubarray);
    }

    // Test metodu
    public static void Test()
    {
        Console.WriteLine("=== En Büyük Artan Alt Dizi Testleri ===");
        Console.WriteLine();

        // Test 1
        var test1 = new List<int> { 1, 2, 3, 1, 2 };
        Console.WriteLine($"Test 1 - Giriş: [{string.Join(", ", test1)}]");
        Console.WriteLine($"Çıkış: {MaxIncreasingSubArrayAsJson(test1)}");
        Console.WriteLine("Beklenen: [1,2,3]");
        Console.WriteLine();

        // Test 2
        var test2 = new List<int> { 2, 5, 4, 3, 2, 1 };
        Console.WriteLine($"Test 2 - Giriş: [{string.Join(", ", test2)}]");
        Console.WriteLine($"Çıkış: {MaxIncreasingSubArrayAsJson(test2)}");
        Console.WriteLine("Beklenen: [2,5]");
        Console.WriteLine();

        // Test 3
        var test3 = new List<int> { 1, 2, 2, 3 };
        Console.WriteLine($"Test 3 - Giriş: [{string.Join(", ", test3)}]");
        Console.WriteLine($"Çıkış: {MaxIncreasingSubArrayAsJson(test3)}");
        Console.WriteLine("Beklenen: [2,3]");
        Console.WriteLine();

        // Test 4
        var test4 = new List<int> { 1, 3, 5, 4, 7, 8, 2 };
        Console.WriteLine($"Test 4 - Giriş: [{string.Join(", ", test4)}]");
        Console.WriteLine($"Çıkış: {MaxIncreasingSubArrayAsJson(test4)}");
        Console.WriteLine("Beklenen: [4,7,8]");
        Console.WriteLine();

        // Test 5
        var test5 = new List<int>();
        Console.WriteLine($"Test 5 - Giriş: []");
        Console.WriteLine($"Çıkış: {MaxIncreasingSubArrayAsJson(test5)}");
        Console.WriteLine("Beklenen: []");
        Console.WriteLine();

        // Test 6
        var test6 = new List<int> { 5 };
        Console.WriteLine($"Test 6 - Giriş: [{string.Join(", ", test6)}]");
        Console.WriteLine($"Çıkış: {MaxIncreasingSubArrayAsJson(test6)}");
        Console.WriteLine("Beklenen: [5]");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MaxIncreasingSubarray.Test();
    }
}
