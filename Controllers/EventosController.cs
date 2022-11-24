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
    public class EventosController : Controller
    {
        private readonly Contexto _context;

        public EventosController(Contexto context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var contexto = _context.evento.Include(e => e.igreja);

            return View(await contexto.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.evento == null)
            {
                return NotFound();
            }

            var evento = await _context.evento
                .Include(e => e.igreja)
                .FirstOrDefaultAsync(m => m.id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["igrejaid"] = new SelectList(_context.igreja, "id", "denominacao");

            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,descricao,igrejaid,endereco,horario")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["igrejaid"] = new SelectList(_context.igreja, "id", "denominacao");
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.evento == null)
            {
                return NotFound();
            }

            var evento = await _context.evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["igrejaid"] = new SelectList(_context.igreja, "id", "denominacao");

            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,descricao,igrejaid,endereco,horario")] Evento evento)
        {
            if (id != evento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.id))
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
            ViewData["igrejaid"] = new SelectList(_context.igreja, "id", "denominacao");

            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.evento == null)
            {
                return NotFound();
            }

            var evento = await _context.evento
                .Include(evento => evento.igreja)
                .FirstOrDefaultAsync(m => m.id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.evento == null)
            {
                return Problem("Entity set 'Contexto.evento'  is null.");
            }
            var evento = await _context.evento.FindAsync(id);
            if (evento != null)
            {
                _context.evento.Remove(evento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
          return _context.evento.Any(e => e.id == id);
        }
    }
}
