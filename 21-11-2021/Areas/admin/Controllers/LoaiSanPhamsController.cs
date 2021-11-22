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
    public class LoaiSanPhamsController : Controller
    {
        private readonly DPContext _context;

        public LoaiSanPhamsController(DPContext context)
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
        // GET: admin/LoaiSanPhams
        public async Task<IActionResult> Index()
        {
            return View(await _context.loaiSanPhams.ToListAsync());
        }

        // GET: admin/LoaiSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.loaiSanPhams
                .FirstOrDefaultAsync(m => m.MaLoaiSanPham == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            ViewBag.MaLoai = loaiSanPham.MaLoaiSanPham;
            ViewBag.TenLoai = loaiSanPham.TenLoaiSanPham;
            ViewBag.sp = _context.sanPhams.Where(x => x.MaLoaiSanPham == id);
            return View(loaiSanPham);
        }

        // GET: admin/LoaiSanPhams/Create
        public IActionResult Create()
        {
            ViewBag.DanhMuc = _context.danhMucs.ToList();
            return View();
        }

        // POST: admin/LoaiSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiSanPham,TenLoaiSanPham,MaDanhMuc,TrangThai")] LoaiSanPham loaisanpham)
        {
            ViewBag.DanhMuc = _context.danhMucs.ToList();
            var loai = await _context.loaiSanPhams.FirstOrDefaultAsync(m => m.TenLoaiSanPham == loaisanpham.TenLoaiSanPham);
            if (loai != null)
            {

                if (loai.TenLoaiSanPham == loai.TenLoaiSanPham)
                {
                    if (loai.TrangThai == true)
                    {
                        ModelState.AddModelError("TenLoaiSanPham", "Tên này đã tồn tại");
                        return View(loai);
                    }
                    else
                    {
                        loai.TrangThai = true;
                        await _context.SaveChangesAsync();
                        UpSanPhamTrue(loai.MaLoaiSanPham);
                        SetAlert("Đã thêm danh mục!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            if (ModelState.IsValid)
            {
                loaisanpham.TrangThai = true;
                _context.Add(loaisanpham);
                await _context.SaveChangesAsync();
                SetAlert("Đã thêm danh mục!", "success");
                return RedirectToAction(nameof(Index));
            }
            return View(loaisanpham);
        }

        // GET: admin/LoaiSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.DanhMuc = _context.danhMucs.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.loaiSanPhams.FindAsync(id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            return View(loaiSanPham);
        }

        // POST: admin/LoaiSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiSanPham,TenLoaiSanPham,MaDanhMuc,TrangThai")] LoaiSanPham loaiSanPham)
        {
            ViewBag.DanhMuc = _context.danhMucs.ToList();
            if (id != loaiSanPham.MaLoaiSanPham)
            {
                return NotFound();
            }
            var loai = await _context.loaiSanPhams.FirstOrDefaultAsync(m => m.TenLoaiSanPham == loaiSanPham.TenLoaiSanPham);
            if (loai != null)
            {

                if (loai.TenLoaiSanPham == loai.TenLoaiSanPham)
                {
                    if (loai.TrangThai == true)
                    {
                        ModelState.AddModelError("TenLoaiSanPham", "Tên này đã tồn tại");
                        return View(loai);
                    }
                    else
                    {
                        loai.TrangThai = true;
                        await _context.SaveChangesAsync();
                        UpSanPhamTrue(loai.MaLoaiSanPham);
                        SetAlert(loai.TenLoaiSanPham + " đã khôi phục và các sản phẩm liên quan!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    loaiSanPham.TrangThai = true;
                    _context.Update(loaiSanPham);
                    SetAlert("Đã sửa sản phẩm!", "success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSanPhamExists(loaiSanPham.MaLoaiSanPham))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSanPham);
        }

        // GET: admin/LoaiSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSanPham = await _context.loaiSanPhams
                .FirstOrDefaultAsync(m => m.MaLoaiSanPham == id);
            if (loaiSanPham == null)
            {
                return NotFound();
            }

            return View(loaiSanPham);
        }

        // POST: admin/LoaiSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSanPham = await _context.loaiSanPhams.FindAsync(id);
            _context.loaiSanPhams.Remove(loaiSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSanPhamExists(int id)
        {
            return _context.loaiSanPhams.Any(e => e.MaLoaiSanPham == id);
        }
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            var loaiSanPham = await _context.loaiSanPhams.FindAsync(id);
            List<SanPham> sp = _context.sanPhams.ToList();
            if (loaiSanPham == null)
            {
                return NotFound();
            }
            else
            {
                UpSanPhamFalse(loaiSanPham.MaLoaiSanPham);
                loaiSanPham.TrangThai = false;
                await _context.SaveChangesAsync();
                SetAlert("Đã xóa sản phẩm!", "success");
                return RedirectToAction(nameof(Index));
            }

        }
        public void UpSanPhamFalse(int id)
        {
            List<SanPham> sp = _context.sanPhams.ToList();
            for (int i = 0; i < sp.Count; i++)
            {
                if (sp[i].MaLoaiSanPham == id)
                {
                    sp[i].TrangThai = false;
                    _context.SaveChanges();
                }
            }

        }
        public void UpSanPhamTrue(int id)
        {
            List<SanPham> sp = _context.sanPhams.ToList();
            for (int i = 0; i < sp.Count; i++)
            {
                if (sp[i].MaLoaiSanPham == id)
                {
                    sp[i].TrangThai = true;
                    _context.SaveChanges();
                }
            }
        }
    }
}
