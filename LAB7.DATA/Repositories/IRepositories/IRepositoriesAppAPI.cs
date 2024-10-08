using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB7.DATA.Repositories.IRepositories
{
    public interface IRepositoriesAppAPI<H> where H : class
    {
        Task<IEnumerable<H>> GetAll();
        Task<H> Create(H h);
        Task Update(int id,H h);
        Task Delete(int id);
        Task<H> GetById(int id);

    }
}
