using _21_11_2021.Areas.admin.Data;
using _21_11_2021.Areas.admin.Models;
using _21_11_2021.Infrastructure;
using _21_11_2021.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _21_11_2021.Controllers
{
    public class HomeController : Controller
    {
        private readonly DPContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public HomeController(DPContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Modal()
        {

            return View();
        }
        public async Task<IActionResult> ModalDelete(int ?id)
        {
            ViewBag.Id = id;
            return View();
        }
        public async Task<IActionResult> ModalDelete1(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Active = "Home";
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            //Cập nhật hạng
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
                var chitiet = _context.chiTietHoaDons.Where(x => x.MaKhachHang == user.Id).ToList();
                int count = 0;
                foreach (var item in chitiet)
                {
                    count += item.SoLuong;
                }
                if (count <= 5)
                {
                    user.Hang = "NONE";
                }
                if (count > 5 && count <= 10)
                {
                    user.Hang = "CU";
                }
                if (count > 10 && count <= 20)
                {
                    user.Hang = "AG";
                }
                if (count > 20)
                {
                    user.Hang = "AU";
                }
                _context.Update(user);
                _context.SaveChanges();
                //
            }
            ///
            int count1 = 0;
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            for (int i = 0; i < cart.Count; i++)
            {

                count1 += cart[i].Quantity;
                ViewBag.SoLuong = count1;
            }

            ////
            return View();
        }

        public async Task<IActionResult> Sanpham(int? id, string searchString)
        {
            ViewBag.Active = "SanPham";
            var sp = await _context.loaiSanPhams.FirstOrDefaultAsync(x => x.MaLoaiSanPham == id);
            if(sp.TrangThai == false)
            {
                SetAlert("Không có sản phẩm này", "success");
                return RedirectToAction("Index");
            }    
            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.ListSP = _context.sanPhams.Where(x => x.TenSanPham.Contains(searchString)).Where(x => x.MaLoaiSanPham == id).ToList();
            }
            else
            {
                ViewBag.ListSP = _context.sanPhams.Where(x => x.MaLoaiSanPham == id).ToList();
            }
            //end search
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            if (id == null)
            {
                return NotFound();
            }
            //Cập nhật hạng
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
                var chitiet = _context.chiTietHoaDons.Where(x => x.MaKhachHang == user.Id).ToList();
                int count = 0;
                foreach (var item in chitiet)
                {
                    count += item.SoLuong;
                }
                if (count <= 5)
                {
                    user.Hang = "NONE";
                }
                if (count > 5 && count <= 10)
                {
                    user.Hang = "CU";
                }
                if (count > 10 && count <= 20)
                {
                    user.Hang = "AG";
                }
                if (count > 20)
                {
                    user.Hang = "AU";
                }
                _context.Update(user);
                _context.SaveChanges();
                //
            }

            ///
            int count1 = 0;
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            for (int i = 0; i < cart.Count; i++)
            {

                count1 += cart[i].Quantity;
                ViewBag.SoLuong = count1;
            }

            ////
            return View();
        }
        public async Task<IActionResult> SanphamDanhMuc(int? id, string searchString)
        {
            ViewBag.Active = "SanPham";
            ViewBag.danhmuc = _context.danhMucs.Where(x => x.MaDanhMuc == id).ToList();
            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.ListSP = _context.sanPhams.Where(x => x.TenSanPham.Contains(searchString)).ToList();
            }
            else
            {
                ViewBag.ListSP = _context.sanPhams.ToList();
            }
            //end search
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            if (id == null)
            {
                return NotFound();
            }
            //Cập nhật hạng
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
                var chitiet = _context.chiTietHoaDons.Where(x => x.MaKhachHang == user.Id).ToList();
                int count = 0;
                foreach (var item in chitiet)
                {
                    count += item.SoLuong;
                }
                if (count <= 5)
                {
                    user.Hang = "NONE";
                }
                if (count > 5 && count <= 10)
                {
                    user.Hang = "CU";
                }
                if (count > 10 && count <= 20)
                {
                    user.Hang = "AG";
                }
                if (count > 20)
                {
                    user.Hang = "AU";
                }
                _context.Update(user);
                _context.SaveChanges();
                //
            }

            ///
            int count1 = 0;
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            for (int i = 0; i < cart.Count; i++)
            {

                count1 += cart[i].Quantity;
                ViewBag.SoLuong = count1;
            }

            ////
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.FirstOrDefaultAsync(m => m.Email == email);
            ViewBag.Email = user.Email;
            ViewBag.HoTen = user.HoTen;
            ViewBag.SDT = user.PhoneNumber;
            ViewBag.DiaChi = user.DiaChi;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel profile)
        {
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();

            //if (!ModelState.IsValid)
            //{
            //    return View(pro);
            //}
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.FirstOrDefaultAsync(m => m.Email == email);
            ViewBag.Email = user.Email;

            user.HoTen = profile.HoTen;
            user.PhoneNumber = profile.SDT;
            user.DiaChi = profile.DiaChi;

            IdentityResult result = await _userManager.UpdateAsync(user);

            _context.SaveChanges();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public async Task<IActionResult> Chitietsanpham(int? id)
        {
            ViewBag.Active = "SanPham";
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
  
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Chitiet = _context.sanPhams.Where(x => x.MaSanPham == id).ToList();
            //Cập nhật hạng
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
                var chitiet = _context.chiTietHoaDons.Where(x => x.MaKhachHang == user.Id).ToList();
                int count = 0;
                foreach (var item in chitiet)
                {
                    count += item.SoLuong;
                }
                if (count <= 5)
                {
                    user.Hang = "NONE";
                }
                if (count > 5 && count <= 10)
                {
                    user.Hang = "CU";
                }
                if (count > 10 && count <= 20)
                {
                    user.Hang = "AG";
                }
                if (count > 20)
                {
                    user.Hang = "AU";
                }
                _context.Update(user);
                _context.SaveChanges();
                //
            }
            ViewBag.DanhGia = _context.danhGias.Where(x => x.MaSanPham == id).ToList();
            ViewBag.Khach = _context.Users.ToList();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> FullSanpham(string searchString)
        {
            ViewBag.Active = "SanPham";
            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.ListSP = _context.sanPhams.Where(x => x.TenSanPham.Contains(searchString)).ToList();
            }
            else
            {
                ViewBag.ListSP = _context.sanPhams.ToList();
            }
            //end search
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            //Cập nhật hạng
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
                var chitiet = _context.chiTietHoaDons.Where(x => x.MaKhachHang == user.Id).ToList();
                int count = 0;
                foreach (var item in chitiet)
                {
                    count += item.SoLuong;
                }
                if (count <= 5)
                {
                    user.Hang = "NONE";
                }
                if (count > 5 && count <= 10)
                {
                    user.Hang = "CU";
                }
                if (count > 10 && count <= 20)
                {
                    user.Hang = "AG";
                }
                if (count > 20)
                {
                    user.Hang = "AU";
                }
                _context.Update(user);
                _context.SaveChanges();
                //
            }
            return View();
        }
        public IActionResult AboutUs()
        {
            ViewBag.Active = "About";
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            int count1 = 0;
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            for (int i = 0; i < cart.Count; i++)
            {

                count1 += cart[i].Quantity;
                ViewBag.SoLuong = count1;
            }
            return View();
        }

    }
}
