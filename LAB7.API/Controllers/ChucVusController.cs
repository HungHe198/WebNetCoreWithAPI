using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB7.DATA.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.AccessControl;
using LAB7.DATA.Repositories.IRepositories;

namespace LAB7.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVusController : ControllerBase
    {
        private readonly IRepositoriesAppAPI<ChucVu> _repo;

        public ChucVusController(IRepositoriesAppAPI<ChucVu> repo)
        {
            _repo = repo;
        }

        // GET: api/ChucVus
        [HttpGet]
        public async Task<IEnumerable<ChucVu>> GetChucVus()
        {
            return await _repo.GetAll();
        }

        // GET: api/ChucVus/5
        [HttpGet("{id}")]
        public async Task<ChucVu> GetChucVu(int id)
        {
            return await _repo.GetById(id);

        }

        // PUT: api/ChucVus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task PutChucVu(int id, ChucVu chucVu)
        {
            await _repo.Update(id, chucVu);
        }

        // POST: api/ChucVus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ChucVu> PostChucVu(ChucVu chucVu)
        {
            return await _repo.Create(chucVu);
        }

        // DELETE: api/ChucVus/5
        [HttpDelete("{id}")]
        public async Task DeleteChucVu(int id)
        {
            await _repo.Delete(id);
        }


    }
}
