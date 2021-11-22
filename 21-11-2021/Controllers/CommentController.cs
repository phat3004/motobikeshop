using _21_11_2021.Areas.admin.Data;
using _21_11_2021.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using _21_11_2021.Infrastructure;
using _21_11_2021.Areas.admin.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace _21_11_2021.Controllers
{
    public class CommentController : Controller
    {
        private readonly DPContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public CommentController(DPContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> CommentsInvoice(int? id)
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();

            ViewBag.DonHang = _context.donHangs.Where(x => x.IdDonHang == id).ToList();
            ViewBag.ChiTiet = _context.chiTietHoaDons.Where(x => x.IdDonHang == id).ToList();
            foreach (var item in ViewBag.ChiTiet)
            {
                ViewBag.ChietKhau = item.ChietKhau;
            }
            var hd = await _context.donHangs.FirstOrDefaultAsync(x => x.IdDonHang == id);
            ViewBag.Tien = hd.TongTien;
            ViewBag.SanPham = _context.sanPhams.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSanPham,ChiTiet,TrangThai,MaKhachHang,NgayDanhGia")] DanhGia danhGia)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == email);
            List<DanhGia> danhgia = _context.danhGias.Where(x => x.MaKhachHang == user.Id).ToList();
            if(danhgia.Count == 0)
            {
                danhGia.MaKhachHang = user.Id;
                danhGia.TrangThai = true;
                danhGia.NgayDanhGia = DateTime.Now;
                if (ModelState.IsValid)
                {
                    _context.Add(danhGia);
                    await _context.SaveChangesAsync();
                    var sp = await _context.sanPhams.FirstOrDefaultAsync(x => x.MaSanPham == danhGia.MaSanPham);
                    SetAlert("Đã bình luận sản phẩm " + sp.TenSanPham, "success");
                    return RedirectToAction("InvoiceComment", "Cart");
                }
            }    
            for(int i = 0; i < danhgia.Count ;i++ )
            {
                if(danhgia[i].MaSanPham == danhGia.MaSanPham)
                {
                    danhgia[i].NgayDanhGia = DateTime.Now;
                    danhgia[i].ChiTiet = danhGia.ChiTiet;
                    await _context.SaveChangesAsync();
                    var sp = await _context.sanPhams.FirstOrDefaultAsync(x => x.MaSanPham == danhgia[i].MaSanPham);
                    SetAlert("Đã thay thế bình luận trước " + sp.TenSanPham, "success");
                    return RedirectToAction("InvoiceComment", "Cart");
                }
                if( i+1 == danhgia.Count )
                {
                    danhGia.MaKhachHang = user.Id;
                    danhGia.TrangThai = true;
                    danhGia.NgayDanhGia = DateTime.Now;
                    if (ModelState.IsValid)
                    {
                        _context.Add(danhGia);
                        await _context.SaveChangesAsync();
                        var sp = await _context.sanPhams.FirstOrDefaultAsync(x => x.MaSanPham == danhGia.MaSanPham);
                        SetAlert("Đã bình luận sản phẩm " + sp.TenSanPham, "success");
                        return RedirectToAction("InvoiceComment", "Cart");
                    }
                }
            }
            SetAlert("Lỗi", "err");
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Modal(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
        public void SetAlert(string mess, string type)
        {
            TempData["AlertMessage"] = mess;
            if (type == "success")
            {
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["AlertType"] = "err";
            }

        }
    }
}
