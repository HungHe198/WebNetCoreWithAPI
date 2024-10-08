using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB7.DATA.Models;
using LAB7.DATA.Repositories.IRepositories;

namespace LAB7.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoriesAppAPI<Employee> _repo;

        public EmployeesController(IRepositoriesAppAPI<Employee> repo)
        {
            _repo = repo;
        }



        // GET: api/Employees
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _repo.GetAll();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<Employee> GetEmployee(int id)
        {
            return await _repo.GetById(id);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task PutEmployee(int id, Employee employee)
        {
            await _repo.Update(id, employee);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Employee> PostEmployee(Employee employee)
        {
            return await _repo.Create(employee);

        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task DeleteEmployee(int id)
        {
            await _repo.Delete(id);
        }


    }
}
