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

namespace _21_11_2021.Areas.admin.Controllers
{
    [Area("admin")]
    public class SlideshowsController : Controller
    {
        private readonly DPContext _context;

        public SlideshowsController(DPContext context)
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
        // GET: admin/Slideshows
        public async Task<IActionResult> Index()
        {
            return View(await _context.slideshows.ToListAsync());
        }

        // GET: admin/Slideshows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshows = await _context.slideshows
                .FirstOrDefaultAsync(m => m.IdSlideShow == id);
            if (slideshows == null)
            {
                return NotFound();
            }

            return View(slideshows);
        }

        // GET: admin/Slideshows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Slideshows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSlideShow,Hinh,TrangThai,NoiBat")] Slideshows slideshows, IFormFile ful)
        {
            if (ModelState.IsValid)
            {
                List<Slideshows> slide = _context.slideshows.ToList();
                for (int i = 0; i < slide.Count; i++)
                {
                    if (slide[i].TrangThai == true && slide[i].NoiBat == slideshows.NoiBat && (slide[i].NoiBat == 1 || slide[i].NoiBat == 2 || slide[i].NoiBat == 3))
                    {
                        SetAlert("Hiện tại đã có slideshow nổi bật nếu muốn thêm vui lòng xóa hoặc cập nhật slide cũ thành 0", "success");
                        return View(slideshows);
                    }
                }
                slideshows.TrangThai = true;
                _context.Add(slideshows);
                await _context.SaveChangesAsync();               
                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/slideshow", slideshows.IdSlideShow + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ful.CopyToAsync(stream);
                    }
                    slideshows.Hinh = slideshows.IdSlideShow + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                    _context.Update(slideshows);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("Hinh", "Vui lòng chọn hình");
                }

            }
            return View(slideshows);
        }

        // GET: admin/Slideshows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshows = await _context.slideshows.FindAsync(id);
            if (slideshows == null)
            {
                return NotFound();
            }
            return View(slideshows);
        }

        // POST: admin/Slideshows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Hinh,TrangThai,NoiBat")] Slideshows slideshows, IFormFile ful)
        {
            var slide = _context.slideshows.Find(id);
            if (id != slide.IdSlideShow)
            {
                return NotFound();
            }
            List<Slideshows> slide1 = _context.slideshows.ToList();
            for (int i = 0; i < slide1.Count; i++)
            {
                if (slide1[i].TrangThai == true && slide1[i].NoiBat == slideshows.NoiBat && (slide1[i].NoiBat == 1 || slide1[i].NoiBat == 2 || slide1[i].NoiBat == 3))
                {
                    SetAlert("Hiện tại đã có slideshow nổi bật nếu muốn đổi vui lòng xóa hoặc cập nhật slide cũ thành 0", "success");
                    return View(slide);
                }
            }
            //them hinh
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/slideshow", slide.IdSlideShow + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ful.CopyToAsync(stream);
                }
                slide.Hinh = slide.IdSlideShow + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                if (ModelState.IsValid)
                {
                    try
                    {
                        slide.TrangThai = true;
                        slide.NoiBat = slideshows.NoiBat;
                        _context.Update(slide);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SlideshowsExists(slide.IdSlideShow))
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
            }
            catch
            {
                slide.TrangThai = true;
                slide.NoiBat = slideshows.NoiBat;
                _context.Update(slide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slide);
        }

        // GET: admin/Slideshows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshows = await _context.slideshows
                .FirstOrDefaultAsync(m => m.IdSlideShow == id);
            if (slideshows == null)
            {
                return NotFound();
            }

            return View(slideshows);
        }

        // POST: admin/Slideshows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slideshows = await _context.slideshows.FindAsync(id);
            _context.slideshows.Remove(slideshows);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideshowsExists(int id)
        {
            return _context.slideshows.Any(e => e.IdSlideShow == id);
        }
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            var slideshows = await _context.slideshows.FindAsync(id);
            if (slideshows == null)
            {
                return NotFound();
            }
            else
            {
                slideshows.TrangThai = false;
                await _context.SaveChangesAsync();
                SetAlert("Đã xóa slideshow", "success");
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
