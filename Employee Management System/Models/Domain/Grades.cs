using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Employee_Management_System.Models
{
    public class Grades
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? GradeId { get; set; }
        public string GradeName { get; set; }
        public int Status { get; set; }
    }
}
