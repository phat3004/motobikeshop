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
    public class KhachHangsController : Controller
    {
        private readonly DPContext _context;

        public KhachHangsController(DPContext context)
        {
            _context = context;
        }

        // GET: admin/DanhMucs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Where(x => x.Email != "nhmphat304@gmail.com").OrderByDescending(x => x.HoTen).ToListAsync());
        }

        // GET: admin/DanhMucs/Details/5
      

        private bool DanhMucExists(int id)
        {
            return _context.danhMucs.Any(e => e.MaDanhMuc == id);
        }
    }
}
