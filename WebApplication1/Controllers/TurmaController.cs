using Application.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class TurmaController : Controller
    {
        private readonly WebAppDbContext dbContext;

        public TurmaController(WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.Turmas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Turma turma)
        {
            if (turma.Professores == null || turma.Professores.Count == 0 || turma.Professores.All(string.IsNullOrWhiteSpace))
            {
                ModelState.AddModelError("Professores", "Adicione pelo menos um professor.");
            }

            if (!ModelState.IsValid)
            {
                return View(turma);
            }

            dbContext.Turmas.Add(turma);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var turma = dbContext.Turmas.Find(id);
            if (turma == null)
                return NotFound();

            return View(turma);
        }

        [HttpPost]
        public IActionResult Edit(Turma turma)
        {
            dbContext.Update(turma);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid? id)
        {
            var turma = dbContext.Turmas.Find(id);
            if (turma == null)
                return NotFound();

            dbContext.Alunos.Where(x => x.TurmaId == turma.Id).ExecuteDelete();
            dbContext.Turmas.Remove(turma);

            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
