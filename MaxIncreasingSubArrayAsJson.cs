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
    }