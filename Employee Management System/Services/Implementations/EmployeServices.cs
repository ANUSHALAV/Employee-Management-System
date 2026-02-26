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
                Title = e.Title,
                Gender=e.Gender,
                Email=e.Email,
                Phone=e.Phone,
                Address=e.Address,
                DateOfBirth=e.DateOfBirth,
                DepartmentId=e.DepartmentId,
                RoleId=e.RoleId,
                GradeId=e.GradeId,
                Status=e.Status
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
                Email = obj.Email,
                Gender = obj.Gender,
                Address = obj.Address,
                DateOfBirth = obj.DateOfBirth,
                DepartmentId = obj.DepartmentId,
                RoleId = obj.RoleId,
                GradeId = obj.GradeId,
                Phone = obj.Phone,
                Password=obj.Password,
                Status = 1
            };
            await employeeCollection.InsertOneAsync(employee);
            return obj;
        }

        public async Task<EmployeeDetailDto> GetEmployeDetailsByIdAsync(string employeId)
        {
            var employeCollection = _database.GetCollection<Employees>("Employes");
            var departmentCollection = _database.GetCollection<Departments>("Departments");
            var roleCollection = _database.GetCollection<Roles>("Roles");
            var gradeCollection = _database.GetCollection<Grades>("Grades");

            var employeefilter = Builders<Employees>.Filter.Where(emp => emp.Id == employeId);

            var employeData = await employeCollection.Find(employeefilter).FirstOrDefaultAsync();

            if (employeData == null)
            {
                return null;
            }

            var employeDepartmentId = employeData.DepartmentId;
            var departmentFilter = Builders<Departments>.Filter.Where(d => d.DepartmentId == employeDepartmentId);
            var departmentData = await departmentCollection.Find(departmentFilter).FirstOrDefaultAsync();

            var roleId = employeData.RoleId;
            var roleFilter = Builders<Roles>.Filter.Where(r => r.RoleId == roleId);
            var roleData = await roleCollection.Find(roleFilter).FirstOrDefaultAsync();

            var gradeId = employeData.GradeId;
            var gradeFilter = Builders<Grades>.Filter.Where(g => g.GradeId == gradeId);
            var gradeData = await gradeCollection.Find(gradeFilter).FirstOrDefaultAsync();


            if (departmentData == null)
            {
                return null;
            }

            return new EmployeeDetailDto
            {

                Id = employeData.Id,
                EmployeeId = employeData.EmployeeId,
                Title = employeData.Title,
                Name = employeData.Name,
                Email = employeData.Email,
                Gender = employeData.Gender,
                Address = employeData.Address,
                DateOfBirth = employeData.DateOfBirth,
                DepartmentId = departmentData?.DepartmentId,
                DepartmentName = departmentData?.DepartmentName,
                RoleId = roleData?.RoleId,
                RoleName = roleData?.RoleName,
                GradeId = gradeData?.GradeId,
                GradeName = gradeData?.GradeName,
                Status = employeData.Status
            };
        }
    }
}
