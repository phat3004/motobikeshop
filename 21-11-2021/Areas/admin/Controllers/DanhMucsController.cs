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
    public class DanhMucsController : Controller
    {
        private readonly DPContext _context;

        public DanhMucsController(DPContext context)
        {
            _context = context;
        }
        public void SetAlert(string mess,string type)
        {
            TempData["AlertMessage"] = mess;
            if(type == "success")
            {
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["AlertType"] = "err";
            }

        }
        // GET: admin/DanhMucs
        public async Task<IActionResult> Index()
        {
            return View(await _context.danhMucs.ToListAsync());
        }

        // GET: admin/DanhMucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.danhMucs
                .FirstOrDefaultAsync(m => m.MaDanhMuc == id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            ViewBag.MaDanhMuc = danhMuc.MaDanhMuc;
            ViewBag.TeDanhMuc = danhMuc.TenDanhMuc;
            ViewBag.loaisp = _context.loaiSanPhams.Where(x => x.MaDanhMuc == id);
            return View(danhMuc);
        }

        // GET: admin/DanhMucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/DanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDanhMuc,TenDanhMuc,TrangThai")] DanhMuc danhMuc)
        {
            var danhmuc = await _context.danhMucs.FirstOrDefaultAsync(m => m.TenDanhMuc == danhMuc.TenDanhMuc);
            if(danhmuc != null)
            {

                if(danhmuc.TenDanhMuc == danhMuc.TenDanhMuc)
                {
                    if(danhmuc.TrangThai == true)
                    {
                        ModelState.AddModelError("TenDanhMuc", "Tên này đã tồn tại");
                        return View(danhMuc);
                    }
                   else
                    {
                        danhmuc.TrangThai = true;
                        UpLoaiAndSannPhamTrue(danhmuc.MaDanhMuc);
                        await _context.SaveChangesAsync();
                        SetAlert("Đã thêm danh mục!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            if (ModelState.IsValid)
            {
                danhMuc.TrangThai = true;
                _context.Add(danhMuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhMuc);
        }

        // GET: admin/DanhMucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.danhMucs.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        // POST: admin/DanhMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDanhMuc,TenDanhMuc,TrangThai")] DanhMuc danhMuc)
        {
            if (id != danhMuc.MaDanhMuc)
            {
                return NotFound();
            }
            var danhmuc = await _context.danhMucs.FirstOrDefaultAsync(m => m.TenDanhMuc == danhMuc.TenDanhMuc);
            if (danhmuc != null)
            {

                if (danhmuc.TenDanhMuc == danhMuc.TenDanhMuc)
                {
                    if (danhmuc.TrangThai == true)
                    {
                        ModelState.AddModelError("TenDanhMuc", "Tên này đã tồn tại");
                        return View(danhMuc);
                    }
                    else
                    {
                        danhmuc.TrangThai = true;
                        UpLoaiAndSannPhamTrue(danhMuc.MaDanhMuc);
                        await _context.SaveChangesAsync();
                        SetAlert(danhmuc.TenDanhMuc + " đã phục hồi!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    danhMuc.TrangThai = true;
                    _context.Update(danhMuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucExists(danhMuc.MaDanhMuc))
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
            return View(danhMuc);
        }

        // GET: admin/DanhMucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.danhMucs
                .FirstOrDefaultAsync(m => m.MaDanhMuc == id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        // POST: admin/DanhMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhMuc = await _context.danhMucs.FindAsync(id);
            _context.danhMucs.Remove(danhMuc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            var danhmuc = await _context.danhMucs.FindAsync(id);
            
            if (danhmuc == null)
            {
                return NotFound();
            }
            else
            {
                UpLoaiAndSannPhamFalse(danhmuc.MaDanhMuc);
                danhmuc.TrangThai = false;
                await _context.SaveChangesAsync();
                SetAlert("Đã xóa danh mục thành công", "success");
                return RedirectToAction(nameof(Index));
            }
            
        }
        public void UpLoaiAndSannPhamFalse(int id)
        {
            List<SanPham> sp = _context.sanPhams.ToList();
            List<LoaiSanPham> lsp = _context.loaiSanPhams.ToList();
            for (int i = 0; i < lsp.Count; i++)
            {
                if (lsp[i].MaDanhMuc == id)
                {
                    lsp[i].TrangThai = false;
                    _context.SaveChanges();
                    for (int a = 0; a < sp.Count; a++)
                    {
                        if (sp[a].MaLoaiSanPham == lsp[i].MaLoaiSanPham)
                        {
                            sp[a].TrangThai = false;
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
        public void UpLoaiAndSannPhamTrue(int id)
        {
            List<SanPham> sp = _context.sanPhams.ToList();
            List<LoaiSanPham> lsp = _context.loaiSanPhams.ToList();
            for (int i = 0; i < lsp.Count; i++)
            {
                if (lsp[i].MaDanhMuc == id)
                {
                    lsp[i].TrangThai = true;
                    _context.SaveChanges();
                    for (int a = 0; a < sp.Count; a++)
                    {
                        if (sp[a].MaLoaiSanPham == lsp[i].MaLoaiSanPham)
                        {
                            sp[a].TrangThai = true;
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
        private bool DanhMucExists(int id)
        {
            return _context.danhMucs.Any(e => e.MaDanhMuc == id);
        }
    }
}
