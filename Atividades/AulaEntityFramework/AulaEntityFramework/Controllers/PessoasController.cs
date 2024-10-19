using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AulaEntityFramework.Models;
using AulaEntityFramework.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace AulaEntityFramework.Controllers
{
    public class PessoasController : Controller
    {
        private IPessoaRepository _pessoaRepository;

        public PessoasController(MyDbContext context, IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet]

        public IActionResult Index(string bdt, string bm)
        {
            var repo = _pessoaRepository.GetAll();
            if (!bdt.IsNullOrEmpty()) {
                repo = _pessoaRepository.GetByBirthDate(Convert.ToDateTime(bdt));
            }
            if (!bm.IsNullOrEmpty()) {
                repo = _pessoaRepository.GetByBirthMonth(Convert.ToInt32(bm));
            }
            return View(repo);
        }

        [HttpPost]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = _pessoaRepository.Get(id.Value);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BirthDate")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _pessoaRepository.Insert(pessoa);
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = _pessoaRepository.Get(id.Value);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,BirthDate")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _pessoaRepository.Update(pessoa);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
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
            return View(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = _pessoaRepository.Get(id.Value);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var pessoa = _pessoaRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(long id)
        {
            var pessoa = _pessoaRepository.Get(id);
            return !(pessoa is null);
        }
    }
}
