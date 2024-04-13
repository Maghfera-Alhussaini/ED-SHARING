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
    public class student_videosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public student_videosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: student_videos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student_Videos.ToListAsync());
        }

        // GET: student_videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_videos = await _context.Student_Videos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student_videos == null)
            {
                return NotFound();
            }

            return View(student_videos);
        }

        // GET: student_videos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: student_videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Create(string title, string Duration, int Num, int CourseId, IFormFile file, IFormFile file1)
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
            var fileModel = new student_videos
            {
                title = title,
                Duration = Duration,
                Num = Num,
                CourseId = CourseId,
                confirm = "waiting",
                Source = file.FileName,
                image = file1.FileName,

            };

            _context.Add(fileModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));






        }

        // GET: student_videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_videos = await _context.Student_Videos.FindAsync(id);
            if (student_videos == null)
            {
                return NotFound();
            }
            return View(student_videos);
        }

        // POST: student_videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,Duration,Source,image,confirm,Rate,Num,CourseId")] student_videos student_videos)
        {
            if (id != student_videos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student_videos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!student_videosExists(student_videos.Id))
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
            return View(student_videos);
        }

        // GET: student_videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_videos = await _context.Student_Videos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student_videos == null)
            {
                return NotFound();
            }

            return View(student_videos);
        }

        // POST: student_videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student_videos = await _context.Student_Videos.FindAsync(id);
            _context.Student_Videos.Remove(student_videos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool student_videosExists(int id)
        {
            return _context.Student_Videos.Any(e => e.Id == id);
        }
    }
}
