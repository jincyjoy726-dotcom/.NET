using System;
using System.Net.NetworkInformation;
using System.Xml;
using System.Xml.Schema;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Employee Records Management System ---");

        string xmlFile = "employees.xml"; 
        string xsdFile = "employees.xsd";

        Console.WriteLine("\nValidating XML file against schema...");
        bool isValid = ValidateXml(xmlFile, xsdFile);

        if (!isValid)
        {
            Console.WriteLine("\nValidation failed. Exiting program.");
            return; 
        }

        Console.WriteLine("Validation successful!");


        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFile);
        Console.WriteLine("\n--- Employees in IT Department ---");
        XmlNodeList itEmployees = doc.SelectNodes("/employees/employee[Department='IT']");
        foreach (XmlNode node in itEmployees)
        {
            string name = node["Name"]?.InnerText ?? "N/A";
            string dept = node["Department"]?.InnerText ?? "N/A";
            Console.WriteLine($"- Name: {name}, Department: {dept}");
        }

        Console.WriteLine("\n--- Employees with Salary > 50000 ---");
        XmlNodeList highSalaryEmployees = doc.SelectNodes("/employees/employee[Salary > 50000]");
        foreach (XmlNode node in highSalaryEmployees)
        {
            string name = node["Name"]?.InnerText ?? "N/A";
            string salary = node["Salary"]?.InnerText ?? "0";
            Console.WriteLine($"- Name: {name}, Salary: {salary}");
        }

        Console.WriteLine("\n--- Employees who joined after 2020-01-01 ---");

        DateTime cutoffDate = new DateTime(2020, 1, 1);

        XmlNodeList allEmployees = doc.SelectNodes("/employees/employee");

        foreach (XmlNode node in allEmployees)
        {
            string name = node["Name"]?.InnerText ?? "N/A";
            string joinDateStr = node["JoiningDate"]?.InnerText;

            if (DateTime.TryParse(joinDateStr, out DateTime joiningDate))
            {

                if (joiningDate > cutoffDate)
                {
                    Console.WriteLine($"- Name: {name}, Joined on: {joinDateStr}");
                }
            }
        }
        Console.WriteLine("\n--- Program Finished ---");
    }
    public static bool ValidateXml(string xmlFile, string xsdFile)
    {
        bool success = true;
        try
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdFile);
            settings.ValidationType = ValidationType.Schema;

            using (XmlReader reader = XmlReader.Create(xmlFile, settings))
            {
                while (reader.Read()) { }
            }
        }
        catch (XmlSchemaValidationException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");
            success = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            success = false;
        }
        return success;
    }
}

