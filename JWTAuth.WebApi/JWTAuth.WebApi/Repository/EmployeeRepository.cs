using JWTAuth.WebApi.Interface;
using JWTAuth.WebApi.Models;

namespace JWTAuth.WebApi.Repository
{
    public class EmployeeRepository : IEmployees
    {

        readonly DatabaseContext _dbContext = new();
        readonly ILogger _logger;

        public EmployeeRepository(DatabaseContext dbContext, ILogger<EmployeeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool CheckEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Employee DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployeeDetails()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeDetails(int id)
        {
            _logger.LogInformation("Inicio de método [GetEmployeeDetails]");
            try
            {
                Employee? employee = _dbContext.Employees.Find(id);
                if (employee != null)
                {
                    _logger.LogInformation("Fin de método [GetEmployeeDetails]");
                    return employee;
                }

                _logger.LogError("Error en método [GetEmployeeDetails] : ArgumentNullException, no se encontró employee");
                throw new ArgumentNullException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en método [GetEmployeeDetails] : {ex}");
                throw;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }


}
