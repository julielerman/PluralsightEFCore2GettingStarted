using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace WebApp.Controllers
{
    public class SamuraisController : Controller
    {
        private readonly SamuraiContext _context;

        public SamuraisController(SamuraiContext context)
        {
            _context = context;
        }

        // GET: Samurais
        public async Task<IActionResult> Index()
        {
            return View(await _context.Samurais.ToListAsync());
        }

        // GET: Samurais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samurai = await _context.Samurais.Include(s => s.SecretIdentity)
                .Include(s => s.Quotes)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (samurai == null)
            {
                return NotFound();
            }

            return View(samurai);
        }

        // GET: Samurais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Samurais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Samurai samurai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(samurai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(samurai);
        }

        // GET: Samurais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samurai = await _context.Samurais.Include(s=>s.SecretIdentity)
                                                 .Include(s=>s.Quotes)
                                                 .SingleOrDefaultAsync(m => m.Id == id);
            if (samurai == null)
            {
                return NotFound();
            }
            return View(samurai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Samurai samurai)
        public async Task<IActionResult> Edit(int id, Samurai samurai)
        {
            if (id != samurai.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(samurai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SamuraiExists(samurai.Id))
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
            return View(samurai);
        }

        // GET: Samurais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samurai = await _context.Samurais
                .SingleOrDefaultAsync(m => m.Id == id);
            if (samurai == null)
            {
                return NotFound();
            }

            return View(samurai);
        }

        // POST: Samurais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var samurai = await _context.Samurais.SingleOrDefaultAsync(m => m.Id == id);
            _context.Samurais.Remove(samurai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SamuraiExists(int id)
        {
            return _context.Samurais.Any(e => e.Id == id);
        }
    }
}
