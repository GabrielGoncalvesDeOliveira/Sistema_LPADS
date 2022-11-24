using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestaoCantinasIgrejas.Models;

namespace SistemaGestaoCantinasIgrejas.Controllers
{
    public class IgrejasController : Controller
    {
        private readonly Contexto _context;

        public IgrejasController(Contexto context)
        {
            _context = context;
        }

        // GET: Igrejas
        public async Task<IActionResult> Index()
        {
              return View(await _context.igreja.ToListAsync());
        }

        // GET: Igrejas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.igreja == null)
            {
                return NotFound();
            }

            var igreja = await _context.igreja
                .FirstOrDefaultAsync(m => m.id == id);
            if (igreja == null)
            {
                return NotFound();
            }

            return View(igreja);
        }

        // GET: Igrejas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Igrejas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,denominacao,endereco")] Igreja igreja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(igreja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(igreja);
        }

        // GET: Igrejas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.igreja == null)
            {
                return NotFound();
            }

            var igreja = await _context.igreja.FindAsync(id);
            if (igreja == null)
            {
                return NotFound();
            }
            return View(igreja);
        }

        // POST: Igrejas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,denominacao,endereco")] Igreja igreja)
        {
            if (id != igreja.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(igreja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgrejaExists(igreja.id))
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
            return View(igreja);
        }

        // GET: Igrejas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.igreja == null)
            {
                return NotFound();
            }

            var igreja = await _context.igreja
                .FirstOrDefaultAsync(m => m.id == id);
            if (igreja == null)
            {
                return NotFound();
            }

            return View(igreja);
        }

        // POST: Igrejas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.igreja == null)
            {
                return Problem("Entity set 'Contexto.igreja'  is null.");
            }
            var igreja = await _context.igreja.FindAsync(id);
            if (igreja != null)
            {
                _context.igreja.Remove(igreja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IgrejaExists(int id)
        {
          return _context.igreja.Any(e => e.id == id);
        }
    }
}
