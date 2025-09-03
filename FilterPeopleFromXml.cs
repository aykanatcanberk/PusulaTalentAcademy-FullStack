using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public class XmlPersonFilter
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        try
        {
            XDocument doc = XDocument.Parse(xmlData);

            var filteredPeople = doc.Descendants("Person")
                .Where(person =>
                    (int)person.Element("Age") > 30 &&
                    (string)person.Element("Department") == "IT" &&
                    (int)person.Element("Salary") > 5000 &&
                    DateTime.Parse((string)person.Element("HireDate")) < new DateTime(2019, 1, 1)
                )
                .Select(person => new
                {
                    Name = (string)person.Element("Name"),
                    Salary = (int)person.Element("Salary")
                })
                .ToList();

            var result = new
            {
                Names = filteredPeople.Select(p => p.Name).OrderBy(name => name).ToList(),
                TotalSalary = filteredPeople.Sum(p => p.Salary),
                AverageSalary = filteredPeople.Any() ? filteredPeople.Average(p => p.Salary) : 0,
                MaxSalary = filteredPeople.Any() ? filteredPeople.Max(p => p.Salary) : 0,
                Count = filteredPeople.Count
            };

            return JsonSerializer.Serialize(result);
        }
        catch (Exception ex)
        {
            return JsonSerializer.Serialize(new
            {
                Names = new List<string>(),
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });
        }
    }

    // Test
    public static void Test()
    {
        Console.WriteLine("=== XML Person Filtreleme Testleri ===");
        Console.WriteLine();

        // Test 1
        string xml1 = "<People><Person><Name>Ali</Name><Age>35</Age><Department>IT</Department><Salary>6000</Salary><HireDate>2018-06-01</HireDate></Person><Person><Name>Ayşe</Name><Age>28</Age><Department>HR</Department><Salary>4500</Salary><HireDate>2020-04-15</HireDate></Person></People>";
        Console.WriteLine(" TEST 1:");
        Console.WriteLine($"Çıkış: {FilterPeopleFromXml(xml1)}");
        Console.WriteLine();

        // Test 2
        string xml2 = "<People><Person><Name>Mehmet</Name><Age>40</Age><Department>IT</Department><Salary>7500</Salary><HireDate>2017-02-01</HireDate></Person></People>";
        Console.WriteLine(" TEST 2:");
        Console.WriteLine($"Çıkış: {FilterPeopleFromXml(xml2)}");
        Console.WriteLine();

        // Test 3
        string xml3 = "<People><Person><Name>Zeynep</Name><Age>45</Age><Department>IT</Department><Salary>9000</Salary><HireDate>2010-01-10</HireDate></Person><Person><Name>Ahmet</Name><Age>50</Age><Department>IT</Department><Salary>8000</Salary><HireDate>2015-05-20</HireDate></Person></People>";
        Console.WriteLine(" TEST 3:");
        Console.WriteLine($"Çıkış: {FilterPeopleFromXml(xml3)}");
        Console.WriteLine();

        // Test 4
        string xml4 = "<People><Person><Name>Fatma</Name><Age>33</Age><Department>Finance</Department><Salary>6000</Salary><HireDate>2018-11-01</HireDate></Person></People>";
        Console.WriteLine(" TEST 4:");
        Console.WriteLine($"Çıkış: {FilterPeopleFromXml(xml4)}");
        Console.WriteLine();

        // Test 5
        string xml5 = "<People><Person><Name>Selim</Name><Age>32</Age><Department>IT</Department><Salary>5500</Salary><HireDate>2018-08-05</HireDate></Person></People>";
        Console.WriteLine(" TEST 5:");
        Console.WriteLine($"Çıkış: {FilterPeopleFromXml(xml5)}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        XmlPersonFilter.Test();

        // Kullanıcıdan XML girişi (opsiyonel)
        Console.WriteLine("=== Kendi XML'inizi Test Edin ===");
        Console.WriteLine("XML verisini girin (çıkmak için 'exit' yazın):");

        while (true)
        {
            Console.Write("XML: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
                break;

            if (!string.IsNullOrEmpty(input))
            {
                string result = XmlPersonFilter.FilterPeopleFromXml(input);
                Console.WriteLine($"Sonuç: {result}");
                Console.WriteLine();
            }
        }
    }
}
