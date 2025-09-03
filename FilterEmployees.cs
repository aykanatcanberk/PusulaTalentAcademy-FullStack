using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class EmployeeFilter
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        var filteredEmployees = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000 && e.Salary <= 9000)
            .Where(e => e.HireDate.Year > 2017)
            .ToList();

        var sortedNames = filteredEmployees
            .Select(e => e.Name)
            .OrderByDescending(name => name.Length)
            .ThenBy(name => name)
            .ToList();

        decimal totalSalary = filteredEmployees.Sum(e => e.Salary);
        decimal averageSalary = filteredEmployees.Any() ? filteredEmployees.Average(e => e.Salary) : 0;
        decimal minSalary = filteredEmployees.Any() ? filteredEmployees.Min(e => e.Salary) : 0;
        decimal maxSalary = filteredEmployees.Any() ? filteredEmployees.Max(e => e.Salary) : 0;
        int count = filteredEmployees.Count;

        var result = new
        {
            Names = sortedNames,
            TotalSalary = totalSalary,
            AverageSalary = Math.Round(averageSalary, 2),
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}

class Program
{
    static void Main()
    {
        TestEmployeeFilter();
    }

    static void TestEmployeeFilter()
    {
        Console.WriteLine("=== Employee Filter Testleri ===");
        Console.WriteLine();

        // Test 1
        var employees1 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Ali", 30, "IT", 6000m, new DateTime(2018, 5, 1)),
            ("Ayşe", 35, "Finance", 8500m, new DateTime(2019, 3, 15)),
            ("Veli", 28, "IT", 7000m, new DateTime(2020, 1, 1))
        };
        Console.WriteLine("TEST 1:");
        Console.WriteLine($"Sonuç: {EmployeeFilter.FilterEmployees(employees1)}");
        Console.WriteLine();

        // Test 2
        var employees2 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Mehmet", 26, "Finance", 5000m, new DateTime(2021, 7, 1)),
            ("Zeynep", 39, "IT", 9000m, new DateTime(2018, 11, 20))
        };
        Console.WriteLine("TEST 2:");
        Console.WriteLine($"Sonuç: {EmployeeFilter.FilterEmployees(employees2)}");
        Console.WriteLine();

        // Test 3
        var employees3 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Burak", 41, "IT", 6000m, new DateTime(2018, 6, 1))
        };
        Console.WriteLine("TEST 3:");
        Console.WriteLine($"Sonuç: {EmployeeFilter.FilterEmployees(employees3)}");
        Console.WriteLine();

        // Test 4
        var employees4 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Canan", 29, "Finance", 8000m, new DateTime(2019, 9, 1)),
            ("Okan", 35, "IT", 7500m, new DateTime(2020, 5, 10))
        };
        Console.WriteLine("TEST 4:");
        Console.WriteLine($"Sonuç: {EmployeeFilter.FilterEmployees(employees4)}");
        Console.WriteLine();

        // Test 5
        var employees5 = new List<(string, int, string, decimal, DateTime)>
        {
            ("Elif", 27, "Finance", 6500m, new DateTime(2017, 12, 31))
        };
        Console.WriteLine("TEST 5:");
        Console.WriteLine($"Sonuç: {EmployeeFilter.FilterEmployees(employees5)}");
        Console.WriteLine();
    }
}
