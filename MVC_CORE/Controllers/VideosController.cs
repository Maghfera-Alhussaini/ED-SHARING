using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CORE.Data;
using MVC_CORE.Models;

namespace MVC_CORE.Controllers
{
    public class VideosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.videos.Include(v => v.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.videos
                .Include(v => v.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.courses, "Id", "Id");
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string Duration,int Num,int CourseId, IFormFile file, IFormFile file1)
        {
            
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\video");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);


                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                }
            var basePath1 = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\video_image");
            bool basePathExists1 = System.IO.Directory.Exists(basePath1);
            if (!basePathExists1) Directory.CreateDirectory(basePath1);
            var fileName1 = Path.GetFileNameWithoutExtension(file1.FileName);
            var filePath1 = Path.Combine(basePath, file1.FileName);


            if (!System.IO.File.Exists(filePath1))
            {
                using (var stream = new FileStream(filePath1, FileMode.Create))
                {
                    await file1.CopyToAsync(stream);
                }


            }
            var fileModel = new Video
                {
                    title= title,
                    Duration= Duration,
                    Num= Num,
                    CourseId= CourseId,
                    Source = file.FileName,
                    image= file1.FileName,
                    Rate = 0
                };

                _context.Add(fileModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));




           

        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.courses, "Id", "Id", video.CourseId);
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,Duration,Source,image,Rate,Num,CourseId")] Video video)
        {
            if (id != video.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.Id))
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
            ViewData["CourseId"] = new SelectList(_context.courses, "Id", "Id", video.CourseId);
            return View(video);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.videos
                .Include(v => v.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.videos.FindAsync(id);
            _context.videos.Remove(video);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return _context.videos.Any(e => e.Id == id);
        }
    }
}
