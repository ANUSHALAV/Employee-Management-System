using Employee_Management_System.Services.Interfaces;
using Employee_Management_System.Models;
using MongoDB.Driver;

namespace Employee_Management_System.Services.Implementations
{
    public class MasterServices : IMasterServices
    {
        private readonly DbSettings _dbSettings;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public MasterServices(DbSettings dbSettings)
        {
            this._dbSettings = dbSettings;
            this._mongoClient = new MongoClient(_dbSettings.ConnectionString);
            this._database = _mongoClient.GetDatabase(_dbSettings.DatabaseName);
        }

        public async Task<List<Departments>> GetDepartmentAsync()
        {
            var departmentCollection = _database.GetCollection<Departments>("Departments");

            var departmentList = await departmentCollection.Find(d => true).ToListAsync();

            return departmentList;
        }

        public async Task<List<Roles>> GetRolesAsync()
        {
            var rolesCollection = _database.GetCollection<Roles>("Roles");

            var rolesList = await rolesCollection.Find(r => true).ToListAsync();

            return rolesList;
        }

        public async Task<List<Grades>> GetGradesAsync()
        {
            var gradeCollection = _database.GetCollection<Grades>("Grades");

            var gradeList = await gradeCollection.Find(g => g.Status == 1).ToListAsync();

            return gradeList;
        }
    }
}
