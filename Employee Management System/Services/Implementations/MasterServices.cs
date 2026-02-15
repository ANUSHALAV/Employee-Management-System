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

        public async Task<List<Departments>> GetDepartment()
        {
            var DepartmentCollection = database.GetCollection<Departments>("Departments");

            var DepartmentList = await DepartmentCollection.Find(d => true).ToListAsync();

            return DepartmentList;
        }

        public async Task<List<Roles>> GetRoles()
        {
            var RolesCollection = database.GetCollection<Roles>("Roles");

            var RolesList = await RolesCollection.Find(r => true).ToListAsync();

            return RolesList;
        }
    }
}
