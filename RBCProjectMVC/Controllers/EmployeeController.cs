using Business.Services.Abstract;
using Entities.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace RBCProjectMVC.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateVM createVM)
        {
            if (!ModelState.IsValid)
                return View(createVM);

            await _employeeService.AddAsync(createVM);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            var updateVM = new EmployeeUpdateVM
            {
                EmployeeId = employee.EmployeId,
                FullName = employee.FullName,
                Position = employee.Position,
                Department = employee.Department,
                HireDate = employee.HireDate,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary
            };

            return View(updateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeUpdateVM updateVM)
        {
            if (!ModelState.IsValid)
                return View(updateVM);

            await _employeeService.UpdateAsync(updateVM);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
