using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab7.DATA.Models;
using Lab7.DATA.Repo;
using Newtonsoft.Json;
using System.Text;
using System.Text.Unicode;
using System.Text.Encodings;

namespace Lab7.WEB.Controllers
{
    public class ChucVusController : Controller
    {
        public ChucVusController()
        {
            
        }

        // GET: ChucVus
        public async Task<IActionResult> Index()
        {
            List<ChucVu> lstChucVu = new List<ChucVu>();
            using (var https = new HttpClient())
            {
                using (var repon = await https.GetAsync("https://localhost:7286/api/ChucVus"))
                {
                    var apirespon = await repon.Content.ReadAsStringAsync();
                    lstChucVu = JsonConvert.DeserializeObject<List<ChucVu>>(apirespon);
                }
            }
            return View(lstChucVu);
        }

        //GET: ChucVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           ChucVu chucVu = new ChucVu();
            using (var https = new HttpClient())
            {
                using (var repon = await https.GetAsync("https://localhost:7286/api/ChucVus" + id))
                {
                    if (repon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var apirespon = await repon.Content.ReadAsStringAsync();
                        chucVu = JsonConvert.DeserializeObject<ChucVu>(apirespon);
                    }
                    else
                    {
                        ViewBag.StudentId = repon.StatusCode;
                    }
                }
            }
            return View(chucVu);
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
                    Encoding.UTF8,"application/json");
                using (var repon = await https.PostAsync("https://localhost:7286/api/ChucVus", stringContent))
                {
                    var apiRespon = await repon.Content.ReadAsStringAsync();
                    NewChucVu = JsonConvert.DeserializeObject<ChucVu>(apiRespon);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: ChucVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ChucVu chucVuDetail = new ChucVu();
            using (var https = new HttpClient())
            {
                using (var repon = await https.GetAsync("https://localhost:7286/api/ChucVus" + id))
                {
                    if (repon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var apiRespon = await repon.Content.ReadAsStringAsync();
                        chucVuDetail = JsonConvert.DeserializeObject<ChucVu>(apiRespon);
                    }
                    else
                    {
                        ViewBag.StudentId = repon.StatusCode;
                    }

                }
            }
            return View(chucVuDetail);
        }

        // POST: ChucVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ChucVu chucVu)
        {
            ChucVu   chucVuUpdate = new ChucVu();
            using (var https = new HttpClient())
            {
                var conten = new MultipartFormDataContent();
                conten.Add(new StringContent(chucVu.Id.ToString()), "Id");
                conten.Add(new StringContent(chucVu.Name), "Name");
                conten.Add(new StringContent(chucVu.Description), "Description");
                //StringContent stringContent = new StringContent(JsonConvert.SerializeObject(student),
                //    Encoding.UTF8, "application/json");
                using (var repon = await https.PutAsync("https://localhost:7286/api/ChucVus", conten))
                {
                    var apirepon = await repon.Content.ReadAsStringAsync();
                    ViewBag.Result = "Sucscess";
                    chucVuUpdate = JsonConvert.DeserializeObject<ChucVu>(apirepon);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: ChucVus/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{

        //}

        //// POST: ChucVus/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{

        //}


    }
}
