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