namespace Employee_Management_System.Models
{
    public class EmployeeDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Status { get; set; }
    }
}
