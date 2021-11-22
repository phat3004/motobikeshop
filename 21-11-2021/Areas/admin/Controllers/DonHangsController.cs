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
    public class DonHangsController : Controller
    {
        private readonly DPContext _context;

        public DonHangsController(DPContext context)
        {
            _context = context;
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
        // GET: admin/DonHangs
        public async Task<IActionResult> Index(string searchString)
        {
            var sp = from l in _context.donHangs
                     select new {l.IdDonHang, l.MaDonHang, l.NgayLap, l.TrangThai };
            //truy van
            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.MaDonHang.ToString().Contains(searchString));
            }
            else
            {
                return View(await _context.donHangs.ToListAsync());
            }
            //
            List<DonHang> listdon = new List<DonHang>();
            foreach (var item in sp)
            {
                DonHang temp = new DonHang();
                temp.IdDonHang = item.IdDonHang;
                temp.MaDonHang = item.MaDonHang;
                temp.NgayLap = item.NgayLap;
                temp.TrangThai = item.TrangThai;
                listdon.Add(temp);
            }
            //await _context.donHangs.ToListAsync()
            return View(listdon);
        }
        public async Task<IActionResult> Ship(string searchString)
        {
            ViewBag.List = _context.donHangs.ToList();
            var sp = from l in _context.donHangs
                     select new { l.IdDonHang, l.MaDonHang, l.NgayLap, l.TrangThai };
            //truy van
            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.MaDonHang.ToString().Contains(searchString));
            }
            else
            {
                return View(await _context.donHangs.ToListAsync());
            }
            //
            List<DonHang> listdon = new List<DonHang>();
            foreach (var item in sp)
            {
                DonHang temp = new DonHang();
                temp.IdDonHang = item.IdDonHang;
                temp.MaDonHang = item.MaDonHang;
                temp.NgayLap = item.NgayLap;
                temp.TrangThai = item.TrangThai;
                listdon.Add(temp);
            }
            //await _context.donHangs.ToListAsync()
            return View(listdon);
        }
        public async Task<IActionResult> Done(string searchString,string thang)
        {
            ViewBag.List = _context.donHangs.ToList();
            var sp = from l in _context.donHangs
                     select new { l.IdDonHang, l.TongTien, l.MaDonHang, l.NgayLap, l.TrangThai };
            //truy van
            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.MaDonHang.ToString().Contains(searchString));
            }    
            if(!String.IsNullOrEmpty(thang))
            {
                sp = sp.Where(x => x.NgayLap.Month.ToString().Equals(thang));
            }    
            //
            List<DonHang> listdon = new List<DonHang>();
            foreach (var item in sp)
            {
                DonHang temp = new DonHang();
                temp.IdDonHang = item.IdDonHang;
                temp.MaDonHang = item.MaDonHang;
                temp.NgayLap = item.NgayLap;
                temp.TrangThai = item.TrangThai;
                temp.TongTien = item.TongTien; 
                listdon.Add(temp);
            }
            //await _context.donHangs.ToListAsync()
            return View(listdon);
        }
        public async Task<IActionResult> Cancel(string searchString)
        {
            ViewBag.List = _context.donHangs.ToList();
            var sp = from l in _context.donHangs
                     select new { l.IdDonHang, l.MaDonHang, l.NgayLap, l.TrangThai };
            //truy van
            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.MaDonHang.ToString().Contains(searchString));
            }
            else
            {
                return View(await _context.donHangs.ToListAsync());
            }
            //
            List<DonHang> listdon = new List<DonHang>();
            foreach (var item in sp)
            {
                DonHang temp = new DonHang();
                temp.IdDonHang = item.IdDonHang;
                temp.MaDonHang = item.MaDonHang;
                temp.NgayLap = item.NgayLap;
                temp.TrangThai = item.TrangThai;
                listdon.Add(temp);
            }
            //await _context.donHangs.ToListAsync()
            return View(listdon);
        }
        // GET: admin/DonHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var sp = _context.chiTietHoaDons.Where(x => x.IdDonHang == id).ToList();
            decimal total = 0;
            foreach(var item in sp)
            {
                ViewBag.Ten = _context.sanPhams.Where(x => x.MaSanPham == item.MaSanPham).ToList();
                total += item.ThanhTien;
                ViewBag.NgayDatHang = item.NgayDatHang;
                ViewBag.Khach = _context.Users.Where(x => x.Id == item.MaKhachHang).ToList();
            }
            ViewBag.ThanhTien = total;
            ViewBag.ChiTiet = sp;
           
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.donHangs
                .FirstOrDefaultAsync(m => m.IdDonHang == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }


        // GET: admin/DonHangs/Edit/5

        public async Task<IActionResult> EditShip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.donHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            return View(donHang);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShip(int id, [Bind("TrangThai")] DonHang donHang)
        {
            var don = await _context.donHangs.FindAsync(id);
            don.TrangThai = donHang.TrangThai;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(don);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.IdDonHang))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                SetAlert("Đơn hàng đã được cập nhật trạng thái!", "success");
                return RedirectToAction(nameof(Ship));
            }
            return View(donHang);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.donHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            return View(donHang);
        }

        // POST: admin/DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrangThai")] DonHang donHang)
        {
            var don = await _context.donHangs.FindAsync(id);
            don.TrangThai = donHang.TrangThai;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(don);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.IdDonHang))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                SetAlert("Đơn hàng đã được cập nhật trạng thái!", "success");
                return RedirectToAction(nameof(Index));
            }
            return View(donHang);
        }


        private bool DonHangExists(int id)
        {
            return _context.donHangs.Any(e => e.IdDonHang == id);
        }
    }
}
