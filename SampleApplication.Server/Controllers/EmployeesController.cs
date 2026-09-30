using Microsoft.AspNetCore.Mvc;
using SampleApplication.Server.Models;
using SampleApplication.Server.Services;

namespace SampleApplication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
        var items = await _service.GetAllAsync();
        return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
        var created = await _service.CreateAsync(employee);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
        await _service.UpdateAsync(id, employee);
        return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
        await _service.DeleteAsync(id);
        return NoContent();
        }
    }
}
