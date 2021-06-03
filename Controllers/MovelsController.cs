using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBL.Models;

namespace PBL.Controllers
{
    public class MovelsController : Controller
    {
        private readonly VendaContext _context;

        public MovelsController(VendaContext context)
        {
            _context = context;
        }

        // GET: Movels
        public async Task<IActionResult> Index()
        {
            var vendaContext = _context.Movels.Include(m => m.Funcionarios);
            return View(await vendaContext.ToListAsync());
        }

        // GET: Movels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movel = await _context.Movels
                .Include(m => m.Funcionarios)
                .FirstOrDefaultAsync(m => m.MovelId == id);
            if (movel == null)
            {
                return NotFound();
            }

            return View(movel);
        }

        // GET: Movels/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios.Where(f => f.StatusFuncionario == "Disponivel").ToList(), "FuncionarioId", "NomeFuncionario");
            return View();
        }

        // POST: Movels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovelId,Nome,Cor,Material,Medidas,Link,Status,FuncionarioId")] Movel movel)
        {
            if (ModelState.IsValid)
            {
                Funcionario funcionario = _context.Funcionarios.Find(movel.FuncionarioId);
                funcionario.StatusFuncionario = "Indisponivel";
                _context.Update(funcionario);
                _context.Add(movel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }                                         //   _context.Funcionarios.Where(f => f.StatusFuncionario == "Disponivel").ToList()
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios.Where(f => f.StatusFuncionario == "Disponivel").ToList(), "FuncionarioId", "NomeFuncionario", movel.FuncionarioId);
            return View(movel);
        }

        // GET: Movels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movel = await _context.Movels.FindAsync(id);
            if (movel == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "FuncionarioId", "NomeFuncionario", movel.FuncionarioId);
            return View(movel);
        }

        // POST: Movels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovelId,Nome,Cor,Material,Medidas,Link,Status,FuncionarioId")] Movel movel)
        {
            if (id != movel.MovelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (movel.Status == "Entregue")
                    {
                        Funcionario funcionario = _context.Funcionarios.Find(movel.FuncionarioId);
                        funcionario.StatusFuncionario = "Disponivel";
                        _context.Update(funcionario);

                    }
                    _context.Update(movel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovelExists(movel.MovelId))
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "FuncionarioId", "NomeFuncionario", movel.FuncionarioId);
            return View(movel);
        }

        // GET: Movels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movel = await _context.Movels
                .Include(m => m.Funcionarios)
                .FirstOrDefaultAsync(m => m.MovelId == id);
            if (movel == null)
            {
                return NotFound();
            }

            return View(movel);
        }

        // POST: Movels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movel = await _context.Movels.FindAsync(id);
            _context.Movels.Remove(movel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovelExists(int id)
        {
            return _context.Movels.Any(e => e.MovelId == id);
        }
    }
}
