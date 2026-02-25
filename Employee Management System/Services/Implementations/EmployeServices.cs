using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;
using MongoDB.Driver;

namespace Employee_Management_System.Services.Implementations
{
    public class EmployeServices : IEmployeServices
    {
        private readonly DbSettings _dbSettings;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        public EmployeServices(DbSettings dbSettings)
        {
            this._dbSettings = dbSettings;
            this._mongoClient = new MongoClient(this._dbSettings.ConnectionString);
            this._database = this._mongoClient.GetDatabase(this._dbSettings.DatabaseName);
        }

        public async Task<List<ResponseEmployeeDTO>> GetEmployeeAsync()
        {
            var employeesCollection = _database.GetCollection<Employees>("Employes");

            var employeesFilter = Builders<Employees>.Filter.Where(e => e.Status == 1);

            var employesList = await employeesCollection.Find(employeesFilter).ToListAsync();

            return employesList.Select(e=>new ResponseEmployeeDTO
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Title = e.Title

            }).ToList();
        }

        public async Task<CreateEmployeeDTO> AddEmployeeAsync(CreateEmployeeDTO obj)
        {
            var employeeCollection = _database.GetCollection<Employees>("Employes");
            var employee = new Employees
            {
                EmployeeId = obj.EmployeeId,
                Name = obj.Name,
                Title = obj.Title,
                Gender = obj.Gender,
                Status = 1
            };
            await employeeCollection.InsertOneAsync(employee);
            return obj;
        }

        public async Task<EmployeeDetailDto> GetEmployeDetailsByIdAsync(string employeId)
        {
            var employeCollection = _database.GetCollection<Employees>("Employes");
            var departmentCollection = _database.GetCollection<Departments>("Departments");

            var employeefilter = Builders<Employees>.Filter.Where(emp => emp.Id == employeId);

            var employeData = await employeCollection.Find(employeefilter).FirstOrDefaultAsync();

            if (employeData == null)
            {
                return null;
            }

            var employeDepartmentId = employeData.DepartmentId;

            var departmentFilter = Builders<Departments>.Filter.Where(d => d.DepartmentId == employeDepartmentId);

            var departmentData = await departmentCollection.Find(departmentFilter).FirstOrDefaultAsync();

            if (departmentData == null)
            {
                return null;
            }

            return new EmployeeDetailDto
            {

                Id = employeData.Id,
                Name = employeData.Name,
                Email = employeData.Email,
                Gender = employeData.Gender,
                Address = employeData.Address,
                DateOfBirth = employeData.DateOfBirth,
                DepartmentId = departmentData.DepartmentId,
                DepartmentName = departmentData.DepartmentName,
                Status = employeData.Status
            };
        }
    }
}
