using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CORE.Data;
using MVC_CORE.Models;

namespace MVC_CORE.Controllers
{
    public class student_videos1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public student_videos1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: student_videos1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student_Videos.ToListAsync());
        }

        // GET: student_videos1/Details/5
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

        // GET: student_videos1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: student_videos1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,title,Duration,Source,image,confirm,Rate,Num,CourseId")] student_videos student_videos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student_videos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student_videos);
        }

        // GET: student_videos1/Edit/5
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

        // POST: student_videos1/Edit/5
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

        // GET: student_videos1/Delete/5
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

        // POST: student_videos1/Delete/5
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
