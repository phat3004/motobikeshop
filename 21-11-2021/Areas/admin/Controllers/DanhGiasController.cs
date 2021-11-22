using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _21_11_2021.Areas.admin.Data;
using _21_11_2021.Areas.admin.Models;
using Newtonsoft.Json;

namespace _21_11_2021.Areas.admin.Controllers
{
    [Area("admin")]
    public class DanhGiasController : Controller
    {
        private readonly DPContext _context;

        public DanhGiasController(DPContext context)
        {
            _context = context;
        }
        public void SetAlert(string mess,string type)
        {
            TempData["AlertMessage"] = mess;
            if(type == "success")
            {
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["AlertType"] = "err";
            }

        }
        // GET: admin/DanhMucs
        public async Task<IActionResult> Index()
        {
            ViewBag.KhachHang = _context.Users.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            return View(await _context.danhGias.ToListAsync());
        }

        // GET: admin/DanhMucs/Details/5
     
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            var danhmuc = await _context.danhGias.FindAsync(id);
            if (danhmuc == null)
            {
                return NotFound();
            }
            else
            {
                danhmuc.TrangThai = false;
                await _context.SaveChangesAsync();
                SetAlert("Đã xóa đánh giá", "success");
                return RedirectToAction(nameof(Index));
            }
            
        }

        private bool DanhMucExists(int id)
        {
            return _context.danhMucs.Any(e => e.MaDanhMuc == id);
        }
    }
}
