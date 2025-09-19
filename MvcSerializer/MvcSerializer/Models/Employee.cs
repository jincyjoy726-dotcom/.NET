namespace MvcSerializer.Models
{
    
    public class Employee
    {
       
        public Employee() { }

        public int Id { get; set; }
        public string Name { get; set; } 
        public string Department { get; set; } 
        public decimal Salary { get; set; }
    }
}