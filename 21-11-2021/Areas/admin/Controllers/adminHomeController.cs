using _21_11_2021.Areas.admin.Data;
using _21_11_2021.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace _21_11_2021.Areas.admin.Controllers
{
    [Area("Admin")]
    public class adminHomeController : Controller
    {
        
        //private readonly ILogger<adminHomeController> _logger;
        private readonly DPContext _context;

        //public adminHomeController(ILogger<adminHomeController> logger)
        //{
        //    _logger = logger;
        //}


        public adminHomeController(DPContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            ViewBag.DanhMuc = _context.danhMucs.ToList().ToPagedList(pageNumber, 3);
            ViewBag.DonHang = _context.donHangs.Where(x => x.TrangThai == 1).ToList().OrderByDescending(x => x.IdDonHang).ToPagedList(pageNumber, 3);
            ViewBag.DonHang1 = _context.donHangs.Where(x => x.TrangThai == 2).ToList().ToPagedList(pageNumber, 3);
            ViewBag.DonHang2 = _context.donHangs.Where(x => x.TrangThai == 3).ToList().ToPagedList(pageNumber, 5);
            ViewBag.DonHang0 = _context.donHangs.Where(x => x.TrangThai == 0).ToList().ToPagedList(pageNumber, 3);
            //số lượng đơn hàng mới.
            int soluongdonmoi = 0;
            var donmoi = _context.donHangs.Where(x => x.TrangThai == 1);
            foreach(var item in donmoi)
            {
                soluongdonmoi += 1;
            }
            ViewBag.DonMoi = soluongdonmoi;
            //doanh thu.
            decimal sotien = 0;
            var tien = _context.donHangs.Where(x => x.TrangThai == 3);
            foreach (var item in tien)
            {
                sotien += item.TongTien;
            }
            ViewBag.SoTien = sotien;
            //khách hàng
            int khachhang = 0;
            var khach = _context.Users.Where(x => x.Email != "nhmphat304@gmail.com");
            foreach (var item in khach)
            {
                khachhang += 1;
            }
            ViewBag.Khach = khachhang;
            //liên hệ
            int lienhe = 0;
            var lien = _context.lienHes.ToList();
            foreach (var item in lien)
            {
                lienhe += 1;
            }
            ViewBag.LienHe = lienhe;
            return View();

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
