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
    public class ParticipantesController : Controller
    {
        private readonly Contexto _context;

        public ParticipantesController(Contexto context)
        {
            _context = context;
        }

        // GET: Participantes
        public async Task<IActionResult> Index()
        {
              return View(await _context.participante.ToListAsync());
        }

        // GET: Participantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.participante == null)
            {
                return NotFound();
            }

            var participante = await _context.participante
                .FirstOrDefaultAsync(m => m.id == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // GET: Participantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Participantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nome,contato,email,endereco")] Participante participante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participante);
        }

        // GET: Participantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.participante == null)
            {
                return NotFound();
            }

            var participante = await _context.participante.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }
            return View(participante);
        }

        // POST: Participantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,contato,email,endereco")] Participante participante)
        {
            if (id != participante.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipanteExists(participante.id))
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
            return View(participante);
        }

        // GET: Participantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.participante == null)
            {
                return NotFound();
            }

            var participante = await _context.participante
                .FirstOrDefaultAsync(m => m.id == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // POST: Participantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.participante == null)
            {
                return Problem("Entity set 'Contexto.participante'  is null.");
            }
            var participante = await _context.participante.FindAsync(id);
            if (participante != null)
            {
                _context.participante.Remove(participante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipanteExists(int id)
        {
          return _context.participante.Any(e => e.id == id);
        }
    }
}
