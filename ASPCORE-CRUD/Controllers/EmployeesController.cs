using ASPCORE_CRUD.Data;
using ASPCORE_CRUD.Models;
using ASPCORE_CRUD.Models.Domain;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCORE_CRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext context;

        public EmployeesController(EmployeeDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var empDb = await context.Employees.ToListAsync();

            

            return View(empDb);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel request)
        {
            var employee = new Employee() 
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Department = request.Department,
                Salary = request.Salary
            };

            await context.Employees.AddAsync(employee);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            var viewModel = new UpdateEmployeeViewModel();

            if(employee != null)
            {
                viewModel = new UpdateEmployeeViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                    Salary = employee.Salary
                };

                return await Task.Run(() => View("View", viewModel));
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel viewModel)
        {
            var emp = await context.Employees.FindAsync(viewModel.Id);

            if(emp != null)
            {
                emp.Name = viewModel.Name;
                emp.Email = viewModel.Email;
                emp.Salary = viewModel.Salary;
                emp.DateOfBirth = viewModel.DateOfBirth;
                emp.Department = viewModel.Department;

                await context.SaveChangesAsync(); 

            }

            return RedirectToAction("Index");
        }
    }
}
