using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace StudentRecords
{
    public class Student
    {
        public int Age { get; set; }
        public string Name { get; set; }
        //public string Subject{get;  }


        public Student(string a, int b)
        {
            Name = a;
            Age = b;
            //Subject = c;
        }
        public void PrintDetails()
        {
            //Console.WriteLine("student Details");
            Console.WriteLine($"Name {Name} Age is {Age}");
        }
    }
}
