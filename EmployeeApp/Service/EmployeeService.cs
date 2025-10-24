using System.Net.Http.Json;

using EmployeeApp.Models;
using System.Net.Http.Json;

namespace EmployeeApp.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _http;

        public EmployeeService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Employee>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<Employee>>("api/employees");

        public async Task<Employee> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<Employee>($"api/employees/{id}");

        public async Task CreateAsync(Employee employee)
            => await _http.PostAsJsonAsync("api/employees", employee);

        public async Task UpdateAsync(Employee employee)
            => await _http.PutAsJsonAsync($"api/employees/{employee.Id}", employee);

        public async Task DeleteAsync(int id)
            => await _http.DeleteAsync($"api/employees/{id}");
        public async Task BulkDeleteAsync(List<int> ids)
        {
            await _http.PostAsJsonAsync("api/employees/bulk-delete", ids);
        }

        public async Task ToggleStatusAsync(int id)
        {
            await _http.PutAsync($"api/employees/{id}/toggle-status", null);
        }
        public async Task<int> GetActiveEmployeesCountAsync()
        {
            var employees = await _http.GetFromJsonAsync<List<Employee>>("api/employees");
            return employees?.Count(e => e.IsActive) ?? 0;
        }

    }
}
