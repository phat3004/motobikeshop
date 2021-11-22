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
    public class CartController : Controller
    {
        private readonly DPContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public CartController(DPContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.CartItem = cart;
            CartViewModel cartVM = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity),

            };
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
            
            return View(cartVM);
        }

        // GET /cart/add/5
        public async Task<IActionResult> Add(int id)
        {
            SanPham product = await _context.sanPhams.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);
            var sp = await _context.sanPhams.FirstOrDefaultAsync(x => x.MaSanPham == id);

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("SanPham","Home", new { id = sp.MaLoaiSanPham });

            return ViewComponent("SmallCart");
        }
        public async Task<IActionResult> AddInCart(int id)
        {
            SanPham product = await _context.sanPhams.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);
            var sp = await _context.sanPhams.FirstOrDefaultAsync(x => x.MaSanPham == id);

            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");

            return ViewComponent("SmallCart");
        }

        // GET /cart/decrease/5
        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        // GET /cart/remove/5
        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        // GET /cart/clear
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            //return RedirectToAction("Page", "Pages");
            //return Redirect("/");
            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return Redirect(Request.Headers["Referer"].ToString());

            return Ok();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInvoice()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);

            ///add Đơn hàng
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            decimal tongtien = 0;
            for(int i = 0; i < cart.Count;i++)
            {
                tongtien += cart[i].Total;
            }
            DonHang don = new DonHang()
            {
                MaDonHang = Guid.NewGuid(),
                NgayLap = DateTime.Now,
                TrangThai = 1,
                TongTien = tongtien

            };
            _context.Add(don);
            _context.SaveChanges();

            ///add chi tiết hóa đơn
            ChiTietHoaDon chitiet = new ChiTietHoaDon();
            for (int i = 0; i < cart.Count; i++)
            {
                chitiet.NgayDatHang = DateTime.Now;
                chitiet.MaSanPham = cart[i].ProductId;
                chitiet.SoLuong = cart[i].Quantity;
                chitiet.Gia = cart[i].Price;
                //chiet khau
                if (user.Hang.Equals("NONE"))
                {
                    chitiet.ChietKhau = 0;
                    chitiet.ThanhTien = cart[i].Total;
                }
                if (user.Hang.Equals("CU"))
                {
                    chitiet.ChietKhau = cart[i].Total * Decimal.Parse(0.05.ToString());
                    chitiet.ThanhTien = cart[i].Total - chitiet.ChietKhau;
                }
                if (user.Hang.Equals("AG"))
                {
                    chitiet.ChietKhau = cart[i].Total * Decimal.Parse(0.1.ToString());
                    chitiet.ThanhTien = cart[i].Total - chitiet.ChietKhau;
                }
                if (user.Hang.Equals("AU"))
                {
                    chitiet.ChietKhau = cart[i].Total * Decimal.Parse(0.2.ToString());
                    chitiet.ThanhTien = cart[i].Total - chitiet.ChietKhau;
                }
                chitiet.IdDonHang = don.IdDonHang;
                chitiet.MaKhachHang = user.Id;
                _context.Add(chitiet);
                _context.SaveChanges();
                chitiet = new ChiTietHoaDon();
            }

            HttpContext.Session.Remove("Cart");
            return RedirectToAction("CheckOut", "Cart");
        }
        public IActionResult CheckOut()
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            ViewBag.SanPham = _context.sanPhams.ToList();
            ViewBag.TinTuc = _context.tinTucs.ToList();
            return View();
        }
        public async Task<IActionResult> Invoice()
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            //check email lấy đơn hàng
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
            //
            ViewBag.don = _context.donHangs.ToList();
            var don = _context.donHangs.ToList();
            ViewBag.ChiTiet = _context.chiTietHoaDons.Distinct().Where(x => x.MaKhachHang == user.Id).OrderByDescending(x => x.NgayDatHang);
            
            return View();

        }
        public async Task<IActionResult> DetailsInvoice(int? id)
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
        //public async Task<IActionResult> DetailsInvoiceComment(int? id)
        //{
        //    ViewBag.DanhMucSp = _context.danhMucs.ToList();
        //    ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
        //    ViewBag.Slide = _context.slideshows.ToList();
        //    ViewBag.Foot = _context.slideshowFoots.ToList();

        //    ViewBag.DonHang = _context.donHangs.Where(x => x.IdDonHang == id).ToList();
        //    ViewBag.DanhGia = _context.danhGias.Where(x => x.IdDonHang == id).ToList();
        //    ViewBag.ChiTiet = _context.chiTietHoaDons.Where(x => x.IdDonHang == id).ToList();
        //    foreach (var item in ViewBag.ChiTiet)
        //    {
        //        ViewBag.ChietKhau = item.ChietKhau;
        //    }
        //    var hd = await _context.donHangs.FirstOrDefaultAsync(x => x.IdDonHang == id);
        //    ViewBag.Tien = hd.TongTien;
        //    ViewBag.SanPham = _context.sanPhams.ToList();
        //    return View();
        //}
        public async Task<IActionResult> InvoiceShip()
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            //check email lấy đơn hàng
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
            //
            ViewBag.don = _context.donHangs.ToList();
            var don = _context.donHangs.ToList();
            ViewBag.ChiTiet = _context.chiTietHoaDons.Distinct().Where(x => x.MaKhachHang == user.Id).OrderByDescending(x => x.NgayDatHang);
            var ChiTiet = _context.chiTietHoaDons.Distinct().Where(x => x.MaKhachHang == user.Id);
            return View();

        }
        public async Task<IActionResult> InvoiceDone()
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            //check email lấy đơn hàng
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
            //
            ViewBag.don = _context.donHangs.ToList();
            var don = _context.donHangs.ToList();
            ViewBag.ChiTiet = _context.chiTietHoaDons.Distinct().Where(x => x.MaKhachHang == user.Id).OrderByDescending(x => x.NgayDatHang);
            var ChiTiet = _context.chiTietHoaDons.Distinct().Where(x => x.MaKhachHang == user.Id);
            return View();

        }
        public async Task<IActionResult> InvoiceCancel()
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            //check email lấy đơn hàng
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
            //
            ViewBag.don = _context.donHangs.ToList();
            var don = _context.donHangs.ToList();
            ViewBag.ChiTiet = _context.chiTietHoaDons.Distinct().Where(x => x.MaKhachHang == user.Id).OrderByDescending(x => x.NgayDatHang);
            var ChiTiet = _context.chiTietHoaDons.Distinct().Where(x => x.MaKhachHang == user.Id);
            return View();

        }
        public async Task<IActionResult> UpdateInvoice(int? id)
        {
            var donhang = await _context.donHangs.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }
            else
            {
                donhang.TrangThai = 0;
                _context.Update(donhang);
                await _context.SaveChangesAsync();
                return RedirectToAction("InvoiceCancel", "Cart");
            }
            
        }
        public async Task<IActionResult> UpdateInvoice1(int? id)
        {
            var donhang = await _context.donHangs.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }
            else
            {
                donhang.TrangThai = 1;
                _context.Update(donhang);
                await _context.SaveChangesAsync();
                return RedirectToAction("Invoice", "Cart");
            }

        }
        public async Task<IActionResult> InvoiceComment()
        {
            ViewBag.DanhMucSp = _context.danhMucs.ToList();
            ViewBag.LoaiSP = _context.loaiSanPhams.ToList();
            ViewBag.Slide = _context.slideshows.ToList();
            ViewBag.Foot = _context.slideshowFoots.ToList();
            //check email lấy đơn hàng
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.users.FirstOrDefaultAsync(m => m.Email == email);
            //
            ViewBag.ChiTiet = _context.chiTietHoaDons.Where(x => x.MaKhachHang == user.Id);
            ViewBag.DanhGia = _context.danhGias.Where(x => x.MaKhachHang == user.Id).ToList();
           
            ViewBag.SP = _context.sanPhams.ToList().Distinct();
              
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSanPham,SoSao,ChiTiet,IdDonHang,TrangThai,MaKhachHang")] DanhGia danhGia)
        {

            //var donhang = await _context.donHangs.FindAsync(danhGia.IdDonHang);
            //if (donhang == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    donhang.TrangThai = 4;
            //    _context.Update(donhang);
            //    await _context.SaveChangesAsync();
            //}
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == email);
            danhGia.MaKhachHang = user.Id;
            danhGia.TrangThai = true;
            if (ModelState.IsValid)
            {
                _context.Add(danhGia);
                await _context.SaveChangesAsync();
                return RedirectToAction("InvoiceComment", "Cart");
            }
            return RedirectToAction(nameof(InvoiceComment));
        }
    }
}
