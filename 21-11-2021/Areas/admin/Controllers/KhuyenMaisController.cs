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
    public class KhuyenMaisController : Controller
    {
        private readonly DPContext _context;

        public KhuyenMaisController(DPContext context)
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
        // GET: admin/KhuyenMais
        public async Task<IActionResult> Index()
        {
            return View(await _context.khuyenMais.ToListAsync());
        }

        // GET: admin/KhuyenMais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.khuyenMais
                .FirstOrDefaultAsync(m => m.MaKhuyenMai == id);
            if (khuyenMai == null)
            {
                return NotFound();
            }

            return View(khuyenMai);
        }

        // GET: admin/KhuyenMais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/KhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKhuyenMai,TenKhuyenMai,SoTienGiam,TrangThai")] KhuyenMai khuyenMai)
        {
            var khuyenmai = await _context.khuyenMais.FirstOrDefaultAsync(m => m.TenKhuyenMai == khuyenMai.TenKhuyenMai);
            if (khuyenmai != null)
            {

                if (khuyenmai.TenKhuyenMai == khuyenMai.TenKhuyenMai)
                {
                    if (khuyenmai.TrangThai == true)
                    {
                        ModelState.AddModelError("TenKhuyenMai", "Tên này đã tồn tại");
                        return View(khuyenmai);
                    }
                    else
                    {
                        khuyenmai.TrangThai = true;
                        await _context.SaveChangesAsync();
                        SetAlert("Đã thêm khuyến mãi!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            if (ModelState.IsValid)
            {
                khuyenMai.TrangThai = true;
                _context.Add(khuyenMai);
                await _context.SaveChangesAsync();
                SetAlert("Đã thêm khuyến mãi!", "success");
                return RedirectToAction(nameof(Index));
            }
            return View(khuyenMai);
        }

        // GET: admin/KhuyenMais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.khuyenMais.FindAsync(id);
            if (khuyenMai == null)
            {
                return NotFound();
            }
            return View(khuyenMai);
        }

        // POST: admin/KhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKhuyenMai,TenKhuyenMai,SoTienGiam,TrangThai")] KhuyenMai khuyenMai)
        {
            
            if (id != khuyenMai.MaKhuyenMai)
            {
                return NotFound();
            }
            var khuyenmai = await _context.khuyenMais.FirstOrDefaultAsync(m => m.TenKhuyenMai == khuyenMai.TenKhuyenMai);
            if (khuyenmai != null)
            {

                if (khuyenmai.TenKhuyenMai == khuyenMai.TenKhuyenMai)
                {
                    if (khuyenmai.TrangThai == true)
                    {
                        ModelState.AddModelError("TenKhuyenMai", "Tên này đã tồn tại");
                        return View(khuyenmai);
                    }
                    else
                    {
                        khuyenmai.TrangThai = true;
                        await _context.SaveChangesAsync();
                        SetAlert(khuyenmai.TenKhuyenMai + " đã được khôi phục!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    khuyenMai.TrangThai = true;
                    _context.Update(khuyenMai);
                    SetAlert("Đã cập nhật khuyến mãi!", "success");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhuyenMaiExists(khuyenMai.MaKhuyenMai))
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
            return View(khuyenMai);
        }

        // GET: admin/KhuyenMais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenMai = await _context.khuyenMais
                .FirstOrDefaultAsync(m => m.MaKhuyenMai == id);
            if (khuyenMai == null)
            {
                return NotFound();
            }

            return View(khuyenMai);
        }

        // POST: admin/KhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khuyenMai = await _context.khuyenMais.FindAsync(id);
            _context.khuyenMais.Remove(khuyenMai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhuyenMaiExists(int id)
        {
            return _context.khuyenMais.Any(e => e.MaKhuyenMai == id);
        }
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            var khuyenMai = await _context.khuyenMais.FindAsync(id);
            if (khuyenMai == null)
            {
                return NotFound();
            }
            else
            {
                khuyenMai.TrangThai = false;
                await _context.SaveChangesAsync();
                SetAlert("Đã xóa khuyến mãi!", "success");
                return RedirectToAction(nameof(Index));
            }

        }

    }
}
