using Employee_Management_System.Models;
using Employee_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Employee_Management_System.Services.Implementations
{
    public class EmployeServices : IEmployeServices
    {
        private readonly DbSettings _dbSettings;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly JWTSettings _jwtSettings;
        public EmployeServices(DbSettings dbSettings, JWTSettings jwtSettings)
        {
            this._dbSettings = dbSettings;
            this._mongoClient = new MongoClient(this._dbSettings.ConnectionString);
            this._database = this._mongoClient.GetDatabase(this._dbSettings.DatabaseName);
            this._jwtSettings = jwtSettings;
        }

        public async Task<ResponseLoginDTO> Login(LoginDTO obj)
        {
            var employeeCollection = _database.GetCollection<Employees>("Employes");
            if (obj == null)
            {
                return null;
            }

            var employeeData = await employeeCollection.Find(e => e.EmployeeId == obj.EmployeeId).FirstOrDefaultAsync();
            if (employeeData == null)
            {
                return null;
            }
            var passwordHasher = new PasswordHasher<Employees>();
            var result = passwordHasher.VerifyHashedPassword(null, employeeData.Password, obj.Password);
            ResponseLoginDTO responce;
            if (result == PasswordVerificationResult.Success)
            {
                var token = this.TokenGenerater(employeeData.EmployeeId);
                responce = new ResponseLoginDTO
                {
                    EmployeeId = employeeData.EmployeeId,
                    RoleId = employeeData.RoleId,
                    Token = token
                };
            }
            else
            {
                return null;
            }

            return responce;
        }

        public async Task<List<ResponseEmployeeDTO>> GetEmployeeAsync()
        {
            var employeesCollection = _database.GetCollection<Employees>("Employes");
            var departmentCollection = _database.GetCollection<Departments>("Departments");
            var roleCollection = _database.GetCollection<Roles>("Roles");
            var gradeCollection = _database.GetCollection<Grades>("Grades");

            var employeesFilter = Builders<Employees>.Filter.Where(e => e.Status == 1);

            var employesList = await employeesCollection.Find(employeesFilter).ToListAsync();

            var departmentList = await departmentCollection.Find(d => true).ToListAsync();
            var gradeList = await gradeCollection.Find(g => true).ToListAsync();
            var roleList = await roleCollection.Find(r => true).ToListAsync();

            var query = (from e in employesList

                         join d in departmentList on e.DepartmentId equals d.DepartmentId into dept
                         from d in dept.DefaultIfEmpty()

                         join r in roleList on e.RoleId equals r.RoleId into roles
                         from r in roles.DefaultIfEmpty()

                         join g in gradeList on e.GradeId equals g.GradeId into grades
                         from g in grades.DefaultIfEmpty()

                         select new ResponseEmployeeDTO
                         {
                             Id = e.Id,
                             EmployeeId = e.EmployeeId,
                             Name = e.Name,
                             Title = e.Title,
                             Gender = e.Gender,
                             Email = e.Email,
                             Phone = e.Phone,
                             Address = e.Address,
                             DateOfBirth = e.DateOfBirth,
                             DepartmentId = e.DepartmentId,
                             DepartmentName = d?.DepartmentName,
                             RoleId = e.RoleId,
                             RoleName = r?.RoleName,
                             GradeId = e.GradeId,
                             GradeName = g?.GradeName,
                             Status = e.Status

                         }).ToList();

            return query;
        }

        public async Task<CreateEmployeeDTO> AddEmployeeAsync(CreateEmployeeDTO obj)
        {
            var employeeCollection = _database.GetCollection<Employees>("Employes");


            var passwordHasher = new PasswordHasher<Employees>();
            var hashPassword = passwordHasher.HashPassword(null, obj.Password);

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
                Password = hashPassword,
                Status = 1
            };
            await employeeCollection.InsertOneAsync(employee);
            return obj;
        }

        public async Task<ResponseEmployeeDTO> GetEmployeDetailsByIdAsync(string employeId)
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

            return new ResponseEmployeeDTO
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




        #region JWT Token Generater
        public string TokenGenerater(string userId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}