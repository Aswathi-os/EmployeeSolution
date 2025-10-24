//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Model;
//using Service;

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeesController : ControllerBase
//    {
//        private readonly IEmployeeService _service;
//        public EmployeesController(IEmployeeService service)
//        {
//            _service = service;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var employees = await _service.GetAllAsync();
//            return Ok(employees);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(int id)
//        {
//            var employee = await _service.GetByIdAsync(id);
//            if (employee == null) return NotFound();
//            return Ok(employee);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(Employee employee)
//        {
//            var created = await _service.AddAsync(employee);
//            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, Employee employee)
//        {
//            if (id != employee.Id) return BadRequest();

//            try
//            {
//                await _service.UpdateAsync(employee);
//            }
//            catch (KeyNotFoundException)
//            {
//                return NotFound();
//            }

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var deleted = await _service.DeleteAsync(id);
//            if (!deleted) return NotFound();
//            return NoContent();
//        }


//    }
//}


using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // ✅ GET all employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllAsync();
            return Ok(employees);
        }

        // ✅ GET single employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        // ✅ CREATE new employee
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var created = await _service.AddAsync(employee);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // ✅ UPDATE employee
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();

            try
            {
                await _service.UpdateAsync(employee);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // ✅ DELETE employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // ✅ BULK DELETE employees
        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest("No employee IDs provided.");

            var result = await _service.BulkDeleteAsync(ids);

            if (!result)
                return NotFound("No matching employees found.");

            return Ok("Employees deleted successfully.");
        }

        // ✅ TOGGLE active/inactive status
        [HttpPut("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var employee = await _service.ToggleStatusAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
    }
}
