using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Employee_Management_System.Models
{
    public class Roles
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RoleId { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
    }
}
