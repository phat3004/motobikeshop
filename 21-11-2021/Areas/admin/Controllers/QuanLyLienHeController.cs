using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _21_11_2021.Areas.admin.Data;
using _21_11_2021.Areas.admin.Models;

namespace _21_11_2021.Areas.admin.Controllers
{
    [Area("admin")]
    public class QuanLyLienHeController : Controller
    {
        private readonly DPContext _context;

        public QuanLyLienHeController(DPContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.lienHes.OrderByDescending(x => x.NgayLienHe).ToListAsync());
        }
        public async Task<IActionResult> XoaLienHe(int id)
        {
            var lienhe = await _context.lienHes.FindAsync(id);
            _context.lienHes.Remove(lienhe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
