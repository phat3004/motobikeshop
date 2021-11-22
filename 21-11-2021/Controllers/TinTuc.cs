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
    public class TinTuc : Controller
    {
        private readonly DPContext _context;

        public TinTuc(DPContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            if(id == null)
            {
                return Json("Err!");
            }
            ViewBag.TinTuc = _context.tinTucs.Where(x => x.MaTinTuc == id).ToList();
            return View();
        }

    }
}
