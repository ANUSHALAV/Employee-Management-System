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

        public async Task<Response> GetDepartment()
        {
            var res = new Response();
            try
            {
                var DepartmentCollection  = database.GetCollection<Departments>("Departments");

                var DepartmentList  = await DepartmentCollection.Find(d => true).ToListAsync();

                res.Data = DepartmentList;
                res.Success = true;
                res.Message = "Department data retrieved successfully.";
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
