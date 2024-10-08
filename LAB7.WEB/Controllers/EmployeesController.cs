using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB7.DATA.Models;
using Newtonsoft.Json;
using System.Text;
using System.Drawing;

namespace LAB7.WEB.Controllers
{
    public class EmployeesController : Controller
    {

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var lstEmp = new List<Employee>();
            using (var https = new HttpClient())
            {
                using (var Respon = await https.GetAsync("https://localhost:7240/api/Employees"))
                {
                    var apiRespon = await Respon.Content.ReadAsStringAsync();
                    lstEmp = JsonConvert.DeserializeObject<List<Employee>>(apiRespon);
                }
            }
            TempData["lstEmp"] = JsonConvert.SerializeObject(lstEmp);
            List<ChucVu> lstChucVu = new List<ChucVu>();
            using (var https = new HttpClient())
            {
                using (var Respon = await https.GetAsync("https://localhost:7240/api/ChucVus"))
                {
                    var apiRespon = await Respon.Content.ReadAsStringAsync();
                    lstChucVu = JsonConvert.DeserializeObject<List<ChucVu>>(apiRespon);
                }
            }
            TempData["lstChucVu"] = JsonConvert.SerializeObject(lstChucVu);
            return View(lstEmp);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Employee EmpDetail = new Employee();
            List<Employee> lstEmp = JsonConvert.DeserializeObject<List<Employee>>(TempData["lstEmp"]?.ToString() ?? "[]");
            EmpDetail = lstEmp.FirstOrDefault(x => x.Id == id);
            return View(EmpDetail);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
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
            TempData["lstChucVu"] = JsonConvert.SerializeObject(lstChucVu);
            ViewData["ChucVuId"] = new SelectList(lstChucVu, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Phone,Address,ChucVuId")] Employee employee)
        {
            List<ChucVu> lstChucVu = JsonConvert.DeserializeObject<List<ChucVu>>(TempData["lstChucVu"]?.ToString() ?? "[]");

            Employee NewEmployee = new Employee();
            using (var https = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employee),
                    Encoding.UTF8, "application/json");
                using (var repon = await https.PostAsync("https://localhost:7240/api/Employees", stringContent))
                {


                    if (repon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var apiRespon = await repon.Content.ReadAsStringAsync();
                        NewEmployee = JsonConvert.DeserializeObject<Employee>(apiRespon);
                    }
                    else
                    {
                        ViewBag.StudentId = repon.StatusCode;
                    }
                }
            }

            ViewData["ChucVuId"] = new SelectList(lstChucVu, "Id", "Name", employee.ChucVuId);
            return RedirectToAction("Index");

        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            Employee EmpDetail = new Employee();
            List<ChucVu> lstChucVu = new List<ChucVu>();
            using (var https = new HttpClient())
            {
                using (var Respon = await https.GetAsync("https://localhost:7240/api/ChucVus"))
                {
                    var apiRespon = await Respon.Content.ReadAsStringAsync();
                    lstChucVu = JsonConvert.DeserializeObject<List<ChucVu>>(apiRespon);
                }
            }

            List<Employee> lstEmp = JsonConvert.DeserializeObject<List<Employee>>(TempData["lstEmp"]?.ToString() ?? "[]");
            EmpDetail = lstEmp.FirstOrDefault(x => x.Id == id);

            TempData["lstChucVu"] = JsonConvert.SerializeObject(lstChucVu);
            ViewData["ChucVuId"] = new SelectList(lstChucVu, "Id", "Name");
            return View(EmpDetail);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Phone,Address,ChucVuId")] Employee employee)
        {
            //if (id != employee.Id)
            //{
            //    return NotFound();
            //}

            //List<ChucVu> lstChucVu = JsonConvert.DeserializeObject<List<ChucVu>>(TempData["lstChucVu"].ToString());
            //ViewData["ChucVuId"] = new SelectList(lstChucVu, "Id", "Name", employee.ChucVuId);
            //using (var https = new HttpClient())
            //{
            //    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

            //    using (var respon = await https.PutAsync($"https://localhost:7240/api/ChucVus/{id}", stringContent))
            //    {
            //        if (respon.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var apiRespon = await respon.Content.ReadAsStringAsync();
            //            Employee EditEmp = JsonConvert.DeserializeObject<Employee>(apiRespon);
            //            return RedirectToAction("Index");
            //        }
            //        else
            //        {
            //            ViewBag.ErrorMessage = "Update fail";
            //        }
            //    }
            //}
            //return View(employee);

            // Kiểm tra nếu id không khớp với employee.Id để tránh lỗi
            if (id != employee.Id)
            {
                return NotFound();
            }

            // Lấy danh sách chức vụ từ TempData để hiển thị trong dropdown
            List<ChucVu> lstChucVu = JsonConvert.DeserializeObject<List<ChucVu>>(TempData["lstChucVu"]?.ToString() ?? "[]");
            ViewData["ChucVuId"] = new SelectList(lstChucVu, "Id", "Name", employee.ChucVuId);

            // Tạo đối tượng HttpClient để gửi yêu cầu HTTP
            using (var https = new HttpClient())
            {
                // Serialize đối tượng employee thành JSON
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT để cập nhật thông tin employee
                using (var respon = await https.PutAsync($"https://localhost:7240/api/Employees/{id}", stringContent))
                {
                    // Kiểm tra nếu phản hồi từ API là thành công
                    if (respon.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Lấy nội dung phản hồi từ API
                        var apirespon = await respon.Content.ReadAsStringAsync();
                        Employee updatedEmployee = JsonConvert.DeserializeObject<Employee>(apirespon);

                        // Điều hướng lại trang Index sau khi cập nhật thành công
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Ghi lại lỗi nếu việc cập nhật thất bại
                        ViewBag.ErrorMessage = $"Update failed: {respon.StatusCode}";
                    }
                }
            }

            // Nếu có lỗi, hiển thị lại view cùng với dữ liệu hiện tại để người dùng có thể thử lại
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Employee EmpDetail = new Employee();
            List<ChucVu> lstChucVu = new List<ChucVu>();
            using (var https = new HttpClient())
            {
                using (var Respon = await https.GetAsync("https://localhost:7240/api/ChucVus"))
                {
                    var apiRespon = await Respon.Content.ReadAsStringAsync();
                    lstChucVu = JsonConvert.DeserializeObject<List<ChucVu>>(apiRespon);
                }
            }

            List<Employee> lstEmp = JsonConvert.DeserializeObject<List<Employee>>(TempData["lstEmp"]?.ToString() ?? "[]");
            EmpDetail = lstEmp.FirstOrDefault(x => x.Id == id);

            TempData["DelEmp"] = JsonConvert.SerializeObject(EmpDetail);
            return View(EmpDetail);

        }

        //POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            using (var https = new HttpClient())
            {
                // Thực hiện lệnh DELETE với id của Employee cần xóa
                using (var Respon = await https.DeleteAsync($"https://localhost:7240/api/Employees/{id}"))
                {
                    if (Respon.IsSuccessStatusCode)
                    {
                        // Xóa thành công, có thể xử lý logic khác nếu cần
                    }
                    else
                    {
                        // Xử lý nếu việc xóa không thành công
                        ModelState.AddModelError(string.Empty, "Error deleting employee");
                        return View(); // Hoặc điều hướng về một trang thông báo lỗi
                    }
                }
            }

            // Sau khi xóa thành công, điều hướng về trang Index
            return RedirectToAction("Index");
            
        }


    }
}
