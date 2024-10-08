using LAB7.DATA.Models;
using LAB7.DATA.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB7.DATA.Repositories.Repositories
{
    public class EmployeeRepositories : IRepositoriesAppAPI<Employee>
    {
        private readonly AppAPIDbContext _context;

        public EmployeeRepositories(AppAPIDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Create(Employee h)
        {
            try
            {
                _context.Employees.Add(h);

                var CVofEmp = _context.ChucVus.FirstOrDefault(x => x.Id == h.ChucVuId);
                if (CVofEmp != null)
                {
                    CVofEmp.Employees.Add(h);
                }
                await _context.SaveChangesAsync();
                return h;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var HDel = await GetById(id);
                var CVofEmp = _context.ChucVus.FirstOrDefault(x => x.Id == HDel.ChucVuId);
                if (CVofEmp != null)
                {
                    CVofEmp.Employees.Remove(HDel);
                }
                _context.Employees.Remove(HDel);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {


            }
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {

            try
            {
                return await _context.Employees.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(int id, Employee h)
        {
            try
            {

                var HUpdate = await GetById(id);
                if (HUpdate != null)
                {
                    HUpdate.Name = h.Name;
                    HUpdate.Age = h.Age;
                    HUpdate.Phone = h.Phone;
                    HUpdate.Address = h.Address;
                    HUpdate.ChucVuId = h.ChucVuId;
                    _context.Employees.Update(HUpdate);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý ngoại lệ
                Console.WriteLine(ex.Message); // Log lỗi ra console (hoặc sử dụng logging framework)
                throw; // Ném lại ngoại lệ để xử lý ở tầng cao hơn nếu cần
            }
        }
    }
}
