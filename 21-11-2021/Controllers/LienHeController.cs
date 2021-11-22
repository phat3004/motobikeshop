using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _21_11_2021.Areas.admin.Data;
using _21_11_2021.Areas.admin.Models;

namespace _21_11_2021.Controllers
{
    public class LienHeController : Controller
    {
        private readonly DPContext _context;

        public LienHeController(DPContext context)
        {
            _context = context;
        }

        // GET: admin/DanhGias
        public IActionResult Create()
        {
            ViewBag.Active = "LienHe";
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            return View();
        }
        public IActionResult DoneContact()
        {
            ViewBag.Active = "LienHe";
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            return View();
        }
        // POST: admin/DanhGias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,SDT,ChiTiet,HoTen,NgayLienHe")] LienHe lienHe)
        {
            
            if (ModelState.IsValid)
            {
                lienHe.NgayLienHe = DateTime.Now;
                _context.Add(lienHe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DoneContact));
            }
            return RedirectToAction("DoneContact", "LienHe");
        }
    }
}
