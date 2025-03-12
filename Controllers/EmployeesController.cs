using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get All Employees
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            return Ok(dbContext.Employees.ToList());
        }

        //Get Employee by ID
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var emp = dbContext.Employees.Find(id);
            if (emp == null) { return NotFound(); }
            return Ok(emp);
        }

        //Post an employee details
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto) 
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };
            
            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();
            return Ok(employeeEntity);
        }

        //Update employee details
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee= dbContext.Employees.Find(id);
            if (employee == null) { return NotFound(); };

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }

        //Delete an employee
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var emp = dbContext.Employees.Find(id);
            if (emp == null) { return NotFound(); }

            dbContext.Employees.Remove(emp);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
