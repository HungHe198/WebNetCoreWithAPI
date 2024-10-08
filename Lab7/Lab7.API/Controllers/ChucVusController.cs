using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab7.DATA.Models;
using Lab7.DATA.Repo;

namespace Lab7.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChucVusController : ControllerBase
    {

        private readonly IRepo<ChucVu> _repo;

        public ChucVusController(IRepo<ChucVu> repo)
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
        public async Task<bool> PutChucVu(int id, ChucVu chucVu)
        {
            return await _repo.Update(id, chucVu);
        }

        // POST: api/ChucVus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<bool> PostChucVu(ChucVu chucVu)
        {
            return await _repo.Create(chucVu);
        }

        // DELETE: api/ChucVus/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteChucVu(int id)
        {
            return await _repo.Delete(id);
        }

       
    }
}
