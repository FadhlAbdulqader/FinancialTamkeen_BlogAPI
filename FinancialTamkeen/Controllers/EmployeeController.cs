using FinancialTamkeen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialTamkeen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext _context;

        public EmployeeController(EmployeeDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _context.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest();
            var product = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (product == null)
                return NotFound();
            return Ok(product);

        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee emp)
        {
            _context.Add(emp);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee empData)
        {
            if (empData == null || empData.EmployeeID == 0)
                return BadRequest();

            var employee = await _context.Employees.FindAsync(empData.EmployeeID);
            if (employee == null)
                return NotFound();
            employee.FirstName = empData.FirstName;
            employee.LastName = empData.LastName;
            employee.Department = empData.Department;
            employee.Salary = empData.Salary;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
