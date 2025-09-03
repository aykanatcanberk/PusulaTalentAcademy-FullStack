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
}