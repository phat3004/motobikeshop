using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _21_11_2021.Areas.admin.Data;
using _21_11_2021.Areas.admin.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace _21_11_2021.Areas.admin.Controllers
{
    [Area("admin")]
    public class SlideshowFootsController : Controller
    {
        private readonly DPContext _context;

        public SlideshowFootsController(DPContext context)
        {
            _context = context;
        }

        // GET: admin/slideshowFoots
        public async Task<IActionResult> Index()
        {
            return View(await _context.slideshowFoots.ToListAsync());
        }

        // GET: admin/slideshowFoots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshowFoots = await _context.slideshowFoots
                .FirstOrDefaultAsync(m => m.IdSlideShowFoot == id);
            if (slideshowFoots == null)
            {
                return NotFound();
            }

            return View(slideshowFoots);
        }

        // GET: admin/slideshowFoots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/slideshowFoots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSlideShowFoot,Hinh,TrangThai")] SlideshowFoot slideshowFoots, IFormFile ful)
        {
            List<SlideshowFoot> foots = _context.slideshowFoots.ToList();
            for (int i = 0; i < foots.Count; i++)
            {
                if (i == 9 && foots[i].TrangThai == true)
                {
                    SetAlert("Hình ảnh tối đa là 10 tấm nếu muốn thêm hãy xóa các tấm cũ", "success");
                    return RedirectToAction(nameof(Index));
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(slideshowFoots);
                    slideshowFoots.TrangThai = true;
                    await _context.SaveChangesAsync();
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/footshow", slideshowFoots.IdSlideShowFoot + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await ful.CopyToAsync(stream);
                    }
                    slideshowFoots.Hinh = slideshowFoots.IdSlideShowFoot + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                    _context.Update(slideshowFoots);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("HinhAnh", "Vui lòng chọn hình");
                }

            }
            return View(slideshowFoots);
        }

        // GET: admin/slideshowFoots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshowFoots = await _context.slideshowFoots.FindAsync(id);
            if (slideshowFoots == null)
            {
                return NotFound();
            }
            return View(slideshowFoots);
        }

        // POST: admin/slideshowFoots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Hinh,TrangThai")] SlideshowFoot slideshowFoots, IFormFile ful)
        {
            var foot = _context.slideshowFoots.Find(id);
            if (id != foot.IdSlideShowFoot)
            {
                return NotFound();
            }
 
            try
            {
                //them hinh
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/footshow", foot.IdSlideShowFoot + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ful.CopyToAsync(stream);
                }
                foot.Hinh = foot.IdSlideShowFoot + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];
                //
                if (ModelState.IsValid)
                {
                    try
                    {
                        foot.TrangThai = true;
                        _context.Update(foot);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!slideshowFootsExists(slideshowFoots.IdSlideShowFoot))
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
                SetAlert("Đã cập nhật", "success");
                return RedirectToAction(nameof(Index));
            }
            return View(slideshowFoots);
        }

        // GET: admin/slideshowFoots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshowFoots = await _context.slideshowFoots
                .FirstOrDefaultAsync(m => m.IdSlideShowFoot == id);
            if (slideshowFoots == null)
            {
                return NotFound();
            }

            return View(slideshowFoots);
        }

        // POST: admin/slideshowFoots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slideshowFoots = await _context.slideshowFoots.FindAsync(id);
            _context.slideshowFoots.Remove(slideshowFoots);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool slideshowFootsExists(int id)
        {
            return _context.slideshowFoots.Any(e => e.IdSlideShowFoot == id);
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
