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
    public class VendasController : Controller
    {
        private readonly Contexto _context;

        public VendasController(Contexto context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.venda
                .Include(v => v.participante)
                .Include(v => v.produto);

            return View(await contexto.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.venda == null)
            {
                return NotFound();
            }

            var venda = await _context.venda
                .Include(v => v.participante)
                .Include(v => v.produto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["participanteid"] = new SelectList(_context.participante, "id", "nome");
            ViewData["produtoid"] = new SelectList(_context.produto, "id", "descricao");

            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,participanteid,produtoid,quantidade,valor,data")] Venda venda)
        {
            var produto = await _context.produto
                .FirstOrDefaultAsync(p => p.id == venda.produtoid);

            if (ModelState.IsValid)
            {
                if (produto.quantidade >= venda.quantidade)
                {
                    float quantidadeFinal = produto.quantidade - venda.quantidade;
                    produto.quantidade = quantidadeFinal;

                    if (quantidadeFinal == 0)
                    {
                         produto.disponivel = false;
                    }

                    venda.produto = produto;

                    _context.Add(venda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("venda_nao_realizada");
                }
            }
            ViewData["participanteid"] = new SelectList(_context.participante, "id", "nome");
            ViewData["produtoid"] = new SelectList(_context.produto, "id", "descricao");

            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.venda == null)
            {
                return NotFound();
            }

            var venda = await _context.venda
                .Include(v => v.participante)
                .Include(v => v.produto)
                .FirstOrDefaultAsync(m => m.id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.venda == null)
            {
                return Problem("Entity set 'Contexto.venda'  is null.");
            }
            var venda = await _context.venda.FindAsync(id);
            if (venda != null)
            {
                _context.venda.Remove(venda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
          return _context.venda.Any(e => e.id == id);
        }
    }
}
