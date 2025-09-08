//Task 1 & 2 combined 

//using StudentRecords;


//Console.WriteLine("enter name");
//string name = Console.ReadLine();
//Console.WriteLine("enter age");
//int age = Convert.ToInt32(Console.ReadLine());
//Console.WriteLine($"HEllo {name} Your age is {age}");
////Task 2
//Student s = new Student(name, age);
//s.PrintDetails();
//Console.WriteLine("need to update age ");
//Console.WriteLine("enter yes or no");
//string choice = Console.ReadLine();
//if (choice.ToLower() == "yes")
//{

//    Console.WriteLine("enter new age");
//    int newage = Convert.ToInt32(Console.ReadLine());
//    s.Age = newage;
//    s.PrintDetails();
//}
//else
//{
//    Console.WriteLine("no changes made");
//}

//Task 3

using System.Collections.Generic;
using System.Linq;
using StudentRecords;
List<Student> studentList = new List<Student>();
studentList.Add(new Student("Jincy", 12));
studentList.Add(new Student("jibin", 23));
//studentList.Add(new Student("Hanna", 25, "English"));
//studentList.Add(new Student("jinz", 2, "English"));

Console.WriteLine("list of students");
//foreach(Student i in studentList)
//{
//    i.PrintDetails();
//}
Console.WriteLine();
List<Student> adult = studentList.Where(s => s.Age >= 18 && s.Age <= 25).ToList().OrderBy(s => s.Name).ToList();

Console.WriteLine("Adult students whose age is greater than is and less than 25 ");
foreach (Student i in adult)
{
    i.PrintDetails();
}





