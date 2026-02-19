using Employee_Management_System.Services.Interfaces;
using Employee_Management_System.Models;
using MongoDB.Driver;

namespace Employee_Management_System.Services.Implementations
{
    public class MasterServices : IMasterServices
    {
        private readonly DbSettings dbSettings;
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;

        public MasterServices(DbSettings dbSettings)
        {
            this.dbSettings = dbSettings;
            this.mongoClient = new MongoClient(dbSettings.ConnectionString);
            this.database = mongoClient.GetDatabase(dbSettings.DatabaseName);
        }

        public async Task<List<Departments>> GetDepartmentAsync()
        {
            var departmentCollection = database.GetCollection<Departments>("Departments");

            var departmentList = await departmentCollection.Find(d => true).ToListAsync();

            return departmentList;
        }

        public async Task<List<Roles>> GetRolesAsync()
        {
            var rolesCollection = database.GetCollection<Roles>("Roles");

            var rolesList = await rolesCollection.Find(r => true).ToListAsync();

            return rolesList;
        }

        public async Task<List<Grades>> GetGradesAsync()
        {
            var gradeCollection = database.GetCollection<Grades>("Grades");

            var gradeList = await gradeCollection.Find(g => g.Status == 1).ToListAsync();

            return gradeList;
        }
    }
}
