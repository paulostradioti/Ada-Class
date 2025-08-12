using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class AlunosController : Controller
    {
        private readonly WebAppDbContext dbContext;

        public AlunosController(WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var alunos = dbContext.Alunos.Include(x => x.Turma).ToList();

            return View(alunos);
        }

        public IActionResult Create()
        {
            ViewBag.Turmas = GetTurmas();

            return View();
        }


        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Turmas = GetTurmas();
                return View(aluno);
            }

            dbContext.Alunos.Add(aluno);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            var aluno = dbContext.Alunos.Find(id);
            if (aluno == null)
                return BadRequest();

            ViewBag.Turmas = GetTurmas();

            return View(aluno);
        }

        [HttpPost]
        public IActionResult Edit(Aluno aluno)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Turmas = GetTurmas();
                return View(aluno);
            }

            dbContext.Update(aluno);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            var aluno = dbContext.Alunos.Find(id);
            if (aluno == null)
                return BadRequest();

            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid? id)
        {
            var aluno = dbContext.Alunos.Find(id);

            if (aluno == null)
                return BadRequest();

            dbContext.Alunos.Remove(aluno);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetTurmas()
        {
            var turmas = dbContext.Turmas.Select(t => new SelectListItem { Text = t.Nome, Value = t.Id.ToString() }).ToList();
            turmas.Insert(0, new SelectListItem { Text = "Selecione uma turma", Value = "" });

            return turmas;
        }

    }
}
