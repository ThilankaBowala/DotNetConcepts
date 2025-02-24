namespace EmployeesManagement.Services.Employee
{
    public interface IEmployeeService
    {
        Task<List<Models.Employee>> GetEmployeesAsync();
        Task<Models.Employee> GetById(int id);
        Task CreateAsync(Models.Employee employee);
        Task<bool> UpdateEmployee(int id, Models.Employee employee);
        Task<bool> DeleteEmployee(int id);
    }
}
