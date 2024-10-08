using Lab7.DATA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.DATA.Repo
{
    public class RepoChucVu : IRepo<ChucVu>
    {
        private readonly AppAPIDbContext _context;

        public RepoChucVu(AppAPIDbContext context)
        {
            _context = context;
        }

     
        public async Task<bool> Create(ChucVu h)
        {
            try
            {
                _context.ChucVus.Add(h);
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
            var ChucVuDel = await GetById(id);
            if (ChucVuDel != null)
            {
                //CancellationToken cancellationToken = CancellationToken.None;
                var lstEmpCVDel = await _context.Employees.Where(x => x.ChucVuId == ChucVuDel.Id).ToListAsync();
                foreach (var item in lstEmpCVDel)
                {
                    item.ChucVu = null;
                    item.ChucVuId = -1;
                    _context.Employees.Update(item);
                    await _context.SaveChangesAsync();
                }
                _context.ChucVus.Remove(ChucVuDel);
                await _context.SaveChangesAsync();
                await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<IEnumerable<ChucVu>> GetAll()
        {
            return await _context.ChucVus.ToListAsync();
        }

        public async Task<ChucVu> GetById(int id)
        {
            return await _context.ChucVus.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(int id, ChucVu h)
        {
            var ChucVuUpdate = await GetById(id);

            if (ChucVuUpdate != null)
            {
                ChucVuUpdate.Name = h.Name;
                ChucVuUpdate.Description = h.Description;
                _context.ChucVus.Update(ChucVuUpdate);
                await _context.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
