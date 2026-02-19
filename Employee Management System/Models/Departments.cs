using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Employee_Management_System.Models
{
    public class Departments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Status { get; set; }
    }
}
