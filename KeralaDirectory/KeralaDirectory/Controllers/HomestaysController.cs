using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KeralaDirectory.Data;
using KeralaDirectory.Models;
using Microsoft.AspNetCore.Authorization;

namespace KeralaDirectory.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class HomestaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomestaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Homestays
        public async Task<IActionResult> Index()
        {
            return View(await _context.Homestays.ToListAsync());
        }

        // GET: Homestays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homestay = await _context.Homestays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homestay == null)
            {
                return NotFound();
            }

            return View(homestay);
        }

        // GET: Homestays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homestays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Description,PricePerNight,OwnerEmail")] Homestay homestay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homestay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homestay);
        }

        // GET: Homestays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homestay = await _context.Homestays.FindAsync(id);
            if (homestay == null)
            {
                return NotFound();
            }
            return View(homestay);
        }

        // POST: Homestays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Description,PricePerNight,OwnerEmail")] Homestay homestay)
        {
            if (id != homestay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homestay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomestayExists(homestay.Id))
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
            return View(homestay);
        }

        // GET: Homestays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homestay = await _context.Homestays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homestay == null)
            {
                return NotFound();
            }

            return View(homestay);
        }

        // POST: Homestays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homestay = await _context.Homestays.FindAsync(id);
            if (homestay != null)
            {
                _context.Homestays.Remove(homestay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomestayExists(int id)
        {
            return _context.Homestays.Any(e => e.Id == id);
        }
    }
}
