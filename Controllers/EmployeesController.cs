using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRMS.Dtos.Employees;
using HRMS.Models;
using HRMS.DbContexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //dependency injection of dbcontext
        private readonly HRMSContext _dbContext;

        public EmployeesController(HRMSContext dbContext)
        {
            _dbContext = dbContext;
        }

       


        public static List<Employee> employees = new List<Employee>()
        {
            new Employee(){ Id = 1, FirstName = "Ahmad", LastName="Nasser", Email = "Ahmad@123.com", Position="Developer", BirthDate = new DateTime(1995,1,25), PhoneNumber="+9625588625", IsActive = true, StartDate = new DateTime(2026, 5, 10), Salary = 1000},
            new Employee(){ Id = 2, FirstName = "Layla", LastName = "Kareem", Email = "Layla@123.com", Position = "HR", BirthDate = new DateTime(2000,1,25), PhoneNumber = "+9625588625", IsActive = true, StartDate = new DateTime(2026, 1, 1), Salary = 1000},
            new Employee(){ Id = 3, FirstName = "Yousef", LastName = "Faris", Email = "Yousef@123.com", Position = "Manager", BirthDate = new DateTime(1996,1,25), PhoneNumber = "+9625588625", IsActive = true, StartDate = new DateTime(2026, 1, 1), Salary = 1200},
            new Employee(){ Id = 4, FirstName = "Nadia", LastName = "Zaid", Email = "Nadia@123.com", Position = "Developer", BirthDate = new DateTime(1999,1,25), PhoneNumber = "+9625588625", IsActive = true, StartDate = new DateTime(2026, 1, 1), Salary = 800}

        };



        [HttpGet()]
        public IActionResult GetByCriteria([FromQuery] SearchEmployeeDto searchEmployeeDto)
        {
            var data = from emp in _dbContext.Employees
                       from dep in _dbContext.Departments.Where(d => d.Id == emp.DepartmentId).DefaultIfEmpty()
                       from manager in _dbContext.Employees.Where(m => m.Id == emp.ManagerId).DefaultIfEmpty()
                       from position in _dbContext.Lookups.Where(p => p.Id == emp.PositionId).DefaultIfEmpty()
                       where (searchEmployeeDto.PositionId == null || emp.PositionId == searchEmployeeDto.PositionId) &&
                             (searchEmployeeDto.Name == null || emp.FirstName.ToUpper().Contains(searchEmployeeDto.Name.ToUpper()))
                       orderby emp.Id descending
                       select new EmployeeDto
                       {
                           Id = emp.Id,
                           Name = emp.FirstName + " " + emp.LastName,
                           PositionId = emp.PositionId,
                           PositionName = position.Name,
                           BirthDate = emp.BirthDate,
                           StartDate = emp.StartDate,
                           EndDate = emp.EndDate,
                           Email= emp.Email,
                           PhoneNumber= emp.PhoneNumber,
                           IsActive= emp.IsActive,
                           Salary= emp.Salary,
                           DepartmentId = emp.DepartmentId,
                           DepartmentName = dep.Name,
                           ManagerId = emp.ManagerId,
                           ManagerName = manager.FirstName + " " + manager.LastName


                       };

            return Ok(data.ToList());
            //return BadRequest("data not loaded");//400 bad request
            //return NotFound("Employee not found");//404 Not Found
            //return StatusCode(500, "Something Went Wrong");//500 internal error
        }

        [HttpGet("{id:long}")]//Route parameter
        public IActionResult GetById(long id)
        {

            var data = _dbContext.Employees.Select(x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
                PositionId = x.PositionId,
                PositionName = x.Lookup.Name,
                BirthDate = x.BirthDate,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive,
                Salary = x.Salary,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.Name,
                ManagerId = x.ManagerId,
                ManagerName = x.Manager != null ? x.Manager.FirstName + " " + x.Manager.LastName : null
            }).FirstOrDefault(x => x.Id == id);


            //var data= _dbContext.Employees.Include(x=>x.Department).Include(x=>x.Manager).FirstOrDefault(x => x.Id == id);

            //eager loading: Include(x=>x.Department).Include(x=>x.Manager)
            //lazy loading: virtual navigation properties in the model class
            //projection: Select(x=> new EmployeeDto{...})


            if (data == null)
            {
                return (NotFound("Employee Not Found"));
            }

            return Ok(data);
        }

        [HttpPost("Add")]
        public IActionResult Add(SaveEmployeeDto employeeDto)
        {
            try
            {

                var employee = new Employee()
                {
                    Id = 0,//(employees.LastOrDefault()?.Id ?? 0) + 1,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    PositionId = employeeDto.PositionId,
                    BirthDate = employeeDto.BirthDate,
                    StartDate = employeeDto.StartDate,
                    EndDate = employeeDto.EndDate,
                    Email = employeeDto.Email,
                    IsActive = employeeDto.IsActive,
                    PhoneNumber = employeeDto.PhoneNumber,
                    Salary = employeeDto.Salary,
                    DepartmentId = employeeDto.DepartmentId,
                    ManagerId = employeeDto.ManagerId





                };


                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
                return Ok(employee.Id);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new Exception(ex.Message));
            }



        }

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, SaveEmployeeDto employeeDto)
        {
            try
            {
                if (id != employeeDto.Id)
                {
                    return BadRequest("id mismatch");//400
                }

                var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == employeeDto.Id);

                if (employee == null)
                {
                    return NotFound("Employee Does Not Exist");
                }




                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.PositionId = employeeDto.PositionId;
                employee.BirthDate = employeeDto.BirthDate;
                employee.StartDate = employeeDto.StartDate;
                employee.EndDate = employeeDto.EndDate;
                employee.Email = employeeDto.Email;
                employee.IsActive = employeeDto.IsActive;
                employee.PhoneNumber = employeeDto.PhoneNumber;
                employee.Salary = employeeDto.Salary;
                employee.DepartmentId = employeeDto.DepartmentId;
                employee.ManagerId = employeeDto.ManagerId;

                _dbContext.SaveChanges();


                return Ok();

            }
            catch (NullReferenceException)
            {
                return NotFound("Employee Does Not Exist");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something Went Wrong: " + new Exception(ex.Message));
            }


           

        }


        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {

            try
            {
                var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    return NotFound("Employee Does Not Exist");
                }

                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Exception(ex.Message));
            }
            
        }



    }//end class

       
}//end namespace