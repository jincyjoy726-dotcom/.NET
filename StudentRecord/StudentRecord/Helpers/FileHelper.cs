using Microsoft.AspNetCore.Hosting;
using StudentRecord.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentRecord.Helpers
{
    public static class FileHelper
    {
        private static string GetFilePath(IWebHostEnvironment env)
        {
            var dir = Path.Combine(env.ContentRootPath, "App_Data");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, "students.txt");
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
            }
            return path;
        }

        public static void AppendStudent(IWebHostEnvironment env, Student s)
        {
            var path = GetFilePath(env);
            var line = $"{s.RollNumber}|{s.Name}|{s.Marks}";
            File.AppendAllLines(path, new[] { line });
        }

        public static List<Student> ReadAll(IWebHostEnvironment env)
        {
            var path = GetFilePath(env);
            var result = new List<Student>();
            foreach (var raw in File.ReadAllLines(path))
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;
                var parts = raw.Split('|');
                if (parts.Length < 3) continue;
                int marks = 0;
                int.TryParse(parts[2], out marks);
                result.Add(new Student
                {
                    RollNumber = parts[0].Trim(),
                    Name = parts[1].Trim(),
                    Marks = marks
                });
            }
            return result;
        }

        public static Student FindByRoll(IWebHostEnvironment env, string roll)
        {
            return ReadAll(env)
                   .FirstOrDefault(s => string.Equals(s.RollNumber, roll));
        }
    }
}