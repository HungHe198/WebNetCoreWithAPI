using Lab7.DATA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.DATA.Repo
{
    public class RepoEmployee : IRepo<Employee>
    {
        private readonly AppAPIDbContext _context;

        public RepoEmployee(AppAPIDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> Create(Employee h)
        {
            try
            {
                _context.Employees.Add(h);
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var EmpDel = await GetById(id);
            if (EmpDel != null)
            {

                _context.Employees.Remove(EmpDel);
                await _context.SaveChangesAsync();
                await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(int id, Employee h)
        {
            var EmpUpdate = await GetById(id);
            if (EmpUpdate != null)
            {
                EmpUpdate.Name = h.Name;
               
                EmpUpdate.ChucVuId = h.ChucVuId;
                EmpUpdate.Phone = h.Phone;
                EmpUpdate.Address = h.Address;
                EmpUpdate.Age = h.Age;

                _context.Employees.Update(EmpUpdate);
                await _context.SaveChangesAsync();
                await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
