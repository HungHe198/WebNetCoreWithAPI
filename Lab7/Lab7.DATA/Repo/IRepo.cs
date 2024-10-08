using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.DATA.Repo
{
    public interface IRepo<H> where H : class
    {
        Task<bool> Create(H h);
        Task<bool> Update(int id, H h );
        Task<bool> Delete(int id);
        Task<IEnumerable<H>> GetAll();
        Task<H> GetById(int id);

    }
}
