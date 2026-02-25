using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Employee_Management_System.Models
{
    public class CreateEmployeeDTO
    {
        public string EmployeeId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DepartmentId { get; set; }
        public string? RoleId { get; set; }
        public string? GradeId { get; set; }
        public string? Password { get; set; }
    }
}
