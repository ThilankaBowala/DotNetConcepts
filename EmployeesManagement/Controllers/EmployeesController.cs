using System;
using Microsoft.AspNetCore.Mvc;
using EmployeesManagement.Models;
using EmployeesManagement.Services.Employee;

namespace EmployeesManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _iEmployeeService;

        public EmployeesController(IEmployeeService iEmployeeService)
        {
            _iEmployeeService = iEmployeeService;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _iEmployeeService.GetEmployeesAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _iEmployeeService.GetById((int)id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,EmailAddress,PhoneNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _iEmployeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _iEmployeeService.GetById((int)id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,EmailAddress,PhoneNumber")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var results = await _iEmployeeService.UpdateEmployee(id, employee);

                if (!results)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _iEmployeeService.GetById((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var results = await _iEmployeeService.DeleteEmployee(id);
            if (!results)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
