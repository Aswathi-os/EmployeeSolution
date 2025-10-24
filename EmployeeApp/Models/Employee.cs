namespace EmployeeApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Place { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;

      
        public bool IsSelected { get; set; } = false;
    }
}

