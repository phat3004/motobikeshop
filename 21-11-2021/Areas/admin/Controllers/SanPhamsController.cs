using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _21_11_2021.Areas.admin.Data;
using _21_11_2021.Areas.admin.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using X.PagedList;

namespace _21_11_2021.Areas.admin.Controllers
{
    [Area("admin")]
    public class SanPhamsController : Controller
    {
        private readonly DPContext _context;

        public SanPhamsController(DPContext context)
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
        // GET: admin/SanPhams
        public IActionResult Index(string searchString, int LoaiSP = 0)
        {
            List<KhuyenMai> khuyenmai = _context.khuyenMais.ToList();
            List<SanPham> sanPham = _context.sanPhams.ToList();
            for(int i = 0; i < khuyenmai.Count; i++)
            {
                for(int a = 0; a < sanPham.Count; a++)
                {
                    if(khuyenmai[i].TrangThai == false && sanPham[a].MaKhuyenMai == khuyenmai[i].MaKhuyenMai)
                    {
                        sanPham[a].GiaKhuyenMai = 0;
                        sanPham[a].GiaDaKhuyenMai = sanPham[a].Gia;
                        sanPham[a].MaKhuyenMai = 1;
                        _context.SaveChanges();
                    }
                }
            }
            ///Truyen loai san pham
            ///// Lấy data
            // Lấy toàn bộ thể loại:
            List<LoaiSanPham> cate = _context.loaiSanPhams.Where(x => x.TrangThai == true).ToList();

            // Tạo SelectList
            SelectList cateList = new SelectList(cate, "MaLoaiSanPham", "TenLoaiSanPham");

            // Set vào ViewBag
            ViewBag.LoaiSP = cateList;

            ///join 2 bang
            var sp = from l in _context.loaiSanPhams
                     join c in _context.sanPhams on l.MaLoaiSanPham equals c.MaLoaiSanPham
                     select new { c.MaSanPham, c.TenSanPham, c.GiaDaKhuyenMai, c.HangMuc, l.TenLoaiSanPham,c.TrangThai, c.MaLoaiSanPham};
            //truy van
            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.TenSanPham.Contains(searchString));
            }
            if (LoaiSP != 0)
            {
                sp = sp.Where(x => x.MaLoaiSanPham == LoaiSP);
            }
            List<SanPham> listproduct = new List<SanPham>();
            foreach (var item in sp)
            {
                SanPham temp = new SanPham();
                temp.MaSanPham = item.MaSanPham;
                temp.TenSanPham = item.TenSanPham;
                temp.GiaDaKhuyenMai = item.GiaDaKhuyenMai;
                temp.HangMuc = item.HangMuc;
                temp.TrangThai = item.TrangThai;
                listproduct.Add(temp);
            }
            return View(listproduct);
        }

        // GET: admin/SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.SanPham = _context.sanPhams.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.sanPhams
                .Include(s => s.Loai)
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: admin/SanPhams/Create
        public IActionResult Create()
        {
            ViewBag.MaLoai = _context.loaiSanPhams.ToList();
            ViewBag.KM = _context.khuyenMais.ToList();
            ViewData["MaKhuyenMai"] = new SelectList(_context.khuyenMais, "MaKhuyenMai", "MaKhuyenMai");
            ViewData["MaLoaiSanPham"] = new SelectList(_context.loaiSanPhams, "MaLoaiSanPham", "MaLoaiSanPham");
            return View();
        }

        // POST: admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSanPham,HinhAnh,TenSanPham,Gia,GiaKhuyenMai,GiaDaKhuyenMai,ChiTiet,MaKhuyenMai,MaLoaiSanPham,HangMuc,TrangThai")] SanPham sanPham, IFormFile ful)
        {
            ViewBag.MaLoai = _context.loaiSanPhams.ToList();
            ViewBag.KM = _context.khuyenMais.ToList();
            var data = from pay in _context.khuyenMais
                       where pay.MaKhuyenMai == sanPham.MaKhuyenMai
                       select pay;
            var loai = await _context.sanPhams.FirstOrDefaultAsync(m => m.TenSanPham == sanPham.TenSanPham);
            if (loai != null)
            {

                if (loai.TenSanPham == loai.TenSanPham)
                {
                    if (loai.TrangThai == true)
                    {
                        ModelState.AddModelError("TenSanPham", "Tên này đã tồn tại");
                        return View(loai);
                    }
                    else
                    {
                        loai.TrangThai = true;
                        await _context.SaveChangesAsync();
                        SetAlert("Đã thêm sản phẩm!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            foreach (var item in data)
            {
                sanPham.GiaKhuyenMai = item.SoTienGiam;
                sanPham.GiaDaKhuyenMai = sanPham.Gia - item.SoTienGiam;
            }
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pro", sanPham.MaSanPham + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ful.CopyToAsync(stream);
                    }
                    sanPham.HinhAnh = sanPham.MaSanPham + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                    sanPham.TrangThai = true;
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                    SetAlert("Đã thêm sản phẩm!", "success");
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("HinhAnh", "Vui lòng chọn hình");
                    return View(sanPham);
                }

            }
            return View(sanPham);
        }

        // GET: admin/SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Json = "";
            ViewBag.MaLoai = _context.loaiSanPhams.ToList();
            ViewBag.KM = _context.khuyenMais.ToList();
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.sanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            //ViewData["MaKhuyenMai"] = new SelectList(_context.khuyenMais, "MaKhuyenMai", "MaKhuyenMai", sanPham.MaKhuyenMai);
            //ViewData["MaLoaiSanPham"] = new SelectList(_context.loaiSanPhams, "MaLoaiSanPham", "MaLoaiSanPham", sanPham.MaLoaiSanPham);
            return View(sanPham);
        }

        // POST: admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HinhAnh,TenSanPham,Gia,GiaKhuyenMai,GiaDaKhuyenMai,ChiTiet,MaKhuyenMai,MaLoaiSanPham,HangMuc,TrangThai")] SanPham sanPham, IFormFile ful)
        {
            ViewBag.MaLoai = _context.loaiSanPhams.ToList();
            ViewBag.KM = _context.khuyenMais.ToList();
            var sanpham = _context.sanPhams.Find(id);
            //ktra ma san pham co ton tai
            if (id != sanpham.MaSanPham)
            {
                return NotFound();
            }
            var loai = await _context.sanPhams.FirstOrDefaultAsync(m => m.TenSanPham == sanPham.TenSanPham);
            if (loai != null)
            {

                if (loai.TenSanPham == loai.TenSanPham)
                {
                    if (loai.TrangThai == true)
                    {
                        ModelState.AddModelError("TenSanPham", "Tên này đã tồn tại");
                        return View(loai);
                    }
                    else
                    {
                        loai.TrangThai = true;
                        await _context.SaveChangesAsync();
                        SetAlert("Đã thêm sản phẩm!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            //them hinh
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pro", sanpham.MaSanPham + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ful.CopyToAsync(stream);
                }
                sanpham.HinhAnh = sanpham.MaSanPham + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                //ktra ma loai san pham
                if (sanPham.MaLoaiSanPham == 0)
                {
                    var data = from img in _context.sanPhams
                               where img.MaSanPham == id
                               select img;
                    foreach (var item in data)
                    {
                        sanPham.MaLoaiSanPham = item.MaLoaiSanPham;
                    }
                }
                //ktra ma khuyen mai
                if (sanPham.MaKhuyenMai == 0)
                {
                    var data = from img in _context.sanPhams
                               where img.MaSanPham == id
                               select img;
                    foreach (var item in data)
                    {
                        sanPham.MaKhuyenMai = item.MaKhuyenMai;
                    }
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        //linq ma khuyen mai
                        var data = from pay in _context.khuyenMais
                                   where pay.MaKhuyenMai == sanPham.MaKhuyenMai
                                   select pay;
                        sanpham.TenSanPham = sanPham.TenSanPham;
                        sanpham.Gia = sanPham.Gia;
                        //chinh sua gia tien
                        foreach (var item in data)
                        {
                            sanpham.GiaKhuyenMai = item.SoTienGiam;
                            sanpham.GiaDaKhuyenMai = sanPham.Gia - item.SoTienGiam;
                        }
                        sanpham.ChiTiet = sanPham.ChiTiet;
                        sanpham.MaKhuyenMai = sanPham.MaKhuyenMai;
                        sanpham.MaLoaiSanPham = sanPham.MaLoaiSanPham;
                        sanpham.HangMuc = sanPham.HangMuc;
                        sanpham.TrangThai = true;
                        await _context.SaveChangesAsync();
                        _context.Update(sanpham);
                        SetAlert("Đã sửa sản phẩm!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SanPhamExists(sanPham.MaSanPham))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch
            {
                //ktra ma loai san pham
                if (sanPham.MaLoaiSanPham == 0)
                {
                    var data = from img in _context.sanPhams
                               where img.MaSanPham == id
                               select img;
                    foreach (var item in data)
                    {
                        sanPham.MaLoaiSanPham = item.MaLoaiSanPham;
                    }
                }
                //ktra ma khuyen mai
                if (sanPham.MaKhuyenMai == 0)
                {
                    var data = from img in _context.sanPhams
                               where img.MaSanPham == id
                               select img;
                    foreach (var item in data)
                    {
                        sanPham.MaKhuyenMai = item.MaKhuyenMai;
                    }
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        //linq ma khuyen mai
                        var data = from pay in _context.khuyenMais
                                   where pay.MaKhuyenMai == sanPham.MaKhuyenMai
                                   select pay;
                        sanpham.TenSanPham = sanPham.TenSanPham;
                        sanpham.Gia = sanPham.Gia;
                        //chinh sua gia tien
                        foreach (var item in data)
                        {
                            sanpham.GiaKhuyenMai = item.SoTienGiam;
                            sanpham.GiaDaKhuyenMai = sanPham.Gia - item.SoTienGiam;
                        }
                        sanpham.ChiTiet = sanPham.ChiTiet;
                        sanpham.MaKhuyenMai = sanPham.MaKhuyenMai;
                        sanpham.MaLoaiSanPham = sanPham.MaLoaiSanPham;
                        sanpham.HangMuc = sanPham.HangMuc;
                        sanpham.TrangThai = true;
                        await _context.SaveChangesAsync();
                        _context.Update(sanpham);
                        SetAlert("Đã sửa sản phẩm!", "success");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SanPhamExists(sanPham.MaSanPham))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            return View(sanpham);
        }

        // GET: admin/SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sanPham = await _context.sanPhams
                .Include(s => s.Loai)
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.sanPhams.FindAsync(id);
            _context.sanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.sanPhams.Any(e => e.MaSanPham == id);
        }
        public async Task<IActionResult> DeleteModal(int? id)
        {
            var sanpham = await _context.sanPhams.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            else
            {
                sanpham.TrangThai = false;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
