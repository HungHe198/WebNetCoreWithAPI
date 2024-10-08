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
    public class ChucVuRepositories : IRepositoriesAppAPI<ChucVu>
    {
        private readonly AppAPIDbContext _context;

        public ChucVuRepositories(AppAPIDbContext context)
        {
            _context = context;
        }

        public async Task<ChucVu> Create(ChucVu h)
        {
            try
            {
                _context.ChucVus.Add(h);
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
                _context.ChucVus.Remove(HDel);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {

                
            }
        }

        public async Task<IEnumerable<ChucVu>> GetAll()
        {
           return await _context.ChucVus.ToListAsync();
        }

        public async Task<ChucVu> GetById(int id)
        {
            return await _context.ChucVus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(int id, ChucVu h)
        {
            try
            {

                var HUpdate = await GetById(id);
                
                if (HUpdate == null)
                {
                    throw new Exception("ChucVu not found");
                }
                HUpdate.Name = h.Name;
                HUpdate.Description = h.Description;
                _context.ChucVus.Update(HUpdate);
                await _context.SaveChangesAsync();
               

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
