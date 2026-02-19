using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;
using MongoDB.Driver;

namespace Employee_Management_System.Services.Implementations
{
    public class EmployeServices : IEmployeServices
    {
        private readonly DbSettings dbSettings;
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;
        public EmployeServices(DbSettings _dbSettings)
        {
            this.dbSettings = _dbSettings;
            this.mongoClient = new MongoClient(this.dbSettings.ConnectionString);
            this.database = this.mongoClient.GetDatabase(this.dbSettings.DatabaseName);
        }

        public async Task<List<Employees>> GetEmployeeAsync()
        {
           var employeesCollection = database.GetCollection<Employees>("Employes");

            var employeesFilter = Builders<Employees>.Filter.Where(e => e.Status == 1);

            var employesList = await employeesCollection.Find(employeesFilter).ToListAsync();

            return employesList;
        }

        public async Task<Employees> AddEmployeeAsync(Employees obj)
        {
            if (obj== null)
            {
                return null;
            }
            else
            {
                var employeeCollection = database.GetCollection<Employees>("Employes");
                await employeeCollection.InsertOneAsync(obj);
                return obj;
            }
        }
    }
}
