using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB7.DATA.Models;
using LAB7.DATA.Repositories.IRepositories;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;

namespace LAB7.WEB.Controllers
{
    public class ChucVusController : Controller
    {
        private readonly IRepositoriesAppAPI<ChucVu> _repo;
        // GET: ChucVus
        public async Task<IActionResult> Index()
        {
            List<ChucVu> lstChucVu = new List<ChucVu>();
            using (var https = new HttpClient())
            {
                using (var Respon = await https.GetAsync("https://localhost:7240/api/ChucVus"))
                {
                    var apiRespon = await Respon.Content.ReadAsStringAsync();
                    lstChucVu = JsonConvert.DeserializeObject<List<ChucVu>>(apiRespon);
                }
            }
            return View(lstChucVu);
        }

        // GET: ChucVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ChucVu chucVuDetail = new ChucVu();
            using (var https = new HttpClient())
            {
                using (var repon = await https.GetAsync("https://localhost:7240/api/ChucVus" + id))
                {
                    if (repon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var apirespon = await repon.Content.ReadAsStringAsync();
                        chucVuDetail = JsonConvert.DeserializeObject<ChucVu>(apirespon);
                    }
                    else
                    {
                        ViewBag.StudentId = repon.StatusCode;
                    }
                }
            }
            return View(chucVuDetail);
        }

        // GET: ChucVus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChucVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] ChucVu chucVu)
        {
            ChucVu NewChucVu = new ChucVu();
            using (var https = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(chucVu),
                    Encoding.UTF8, "application/json");
                using (var repon = await https.PostAsync("https://localhost:7240/api/ChucVus", stringContent))
                {
                    var apiRespon = await repon.Content.ReadAsStringAsync();
                    NewChucVu = JsonConvert.DeserializeObject<ChucVu>(apiRespon);
                }
            }
            return RedirectToAction("Index");
        }

        //GET: ChucVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ChucVu chucVuEdit = new ChucVu();
            using (var https = new HttpClient())
            {
                using (var repon = await https.GetAsync("https://localhost:7240/api/ChucVus" + id))
                {
                    if (repon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var apirespon = await repon.Content.ReadAsStringAsync();
                        chucVuEdit = JsonConvert.DeserializeObject<ChucVu>(apirespon);
                    }
                    else
                    {
                        ViewBag.StudentId = repon.StatusCode;
                    }
                }
            }
            return View(chucVuEdit);
        }

        // POST: ChucVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ChucVu chucVu)
        {
            if (id != chucVu.Id)
            {
                return NotFound();
            }

            using (var https = new HttpClient())
            {
                // Convert đối tượng chucVu thành JSON string
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(chucVu),
                    Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT để cập nhật dữ liệu
                using (var repon = await https.PutAsync($"https://localhost:7240/api/ChucVus/{id}", stringContent))
                {
                    // Kiểm tra nếu yêu cầu cập nhật thành công
                    if (repon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Lấy phản hồi từ API
                        var apiRespon = await repon.Content.ReadAsStringAsync();
                        var updatedChucVu = JsonConvert.DeserializeObject<ChucVu>(apiRespon);
                    }
                    else
                    {
                        // Trả về trang view kèm mã lỗi nếu không thành công
                        ViewBag.StatusCode = repon.StatusCode;
                        return View(chucVu);
                    }
                }
            }

            // Nếu thành công, điều hướng về trang Index
            return RedirectToAction("Index");

        }

        // GET: ChucVus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ChucVu chucVuDel = new ChucVu();
            using (var https = new HttpClient())
            {
                using (var repon = await https.GetAsync("https://localhost:7240/api/ChucVus" + id))
                {
                    if (repon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var apirespon = await repon.Content.ReadAsStringAsync();
                        chucVuDel = JsonConvert.DeserializeObject<ChucVu>(apirespon);
                    }
                    else
                    {
                        ViewBag.StudentId = repon.StatusCode;
                    }
                }
            }
            return View(chucVuDel);

        }

        // POST: ChucVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var https = new HttpClient())
            {
                // Gửi yêu cầu DELETE đến API với id của đối tượng cần xóa
                using (var repon = await https.DeleteAsync($"https://localhost:7240/api/ChucVus/{id}"))
                {
                    // Kiểm tra nếu yêu cầu xóa thành công
                    if (repon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Phản hồi từ API không cần thiết nếu không có dữ liệu trả về, chỉ kiểm tra trạng thái OK
                    }
                    else
                    {
                        // Lưu mã lỗi nếu quá trình xóa thất bại
                        ViewBag.StatusCode = repon.StatusCode;
                        return RedirectToAction("Delete", new { id = id }); // Điều hướng về lại trang Delete nếu lỗi
                    }
                }
            }

            // Nếu thành công, điều hướng về trang Index
            return RedirectToAction("Index");
        }


    }
}
