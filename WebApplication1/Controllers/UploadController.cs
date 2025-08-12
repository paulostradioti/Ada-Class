using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(UploadViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Valida arquivos (ex.: até 10MB cada e apenas png/jpg/pdf)
            var allowed = new[] { ".png", ".jpg", ".jpeg", ".pdf", ".txt" };
            foreach (var file in model.Anexos.Where(f => f?.Length > 0))
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowed.Contains(ext))
                    ModelState.AddModelError(nameof(model.Anexos), $"Extensão não permitida: {ext}");

                if (file.Length > 10 * 1024 * 1024)
                    ModelState.AddModelError(nameof(model.Anexos), $"Arquivo {file.FileName} excede 10MB.");
            }

            if (!ModelState.IsValid)
                return View("Index", model);

            var currentDirectory = Directory.GetCurrentDirectory();
            var uploadRoot = Path.Combine(currentDirectory, "uploads");
            Directory.CreateDirectory(uploadRoot);

            var savedFiles = new List<string>();
            foreach (var file in model.Anexos.Where(f => f?.Length > 0))
            {
                var safeName = Path.GetFileNameWithoutExtension(file.FileName);
                var ext = Path.GetExtension(file.FileName);
                var newName = $"{safeName}-{Guid.NewGuid():N}{ext}";
                var fullPath = Path.Combine(uploadRoot, newName);

                using var stream = System.IO.File.Create(fullPath);
                file.CopyTo(stream);
                savedFiles.Add($"/uploads/{newName}");
            }

            // TODO: persistir Nome/Descricao + paths em banco

            TempData["msg"] = $"Upload salvo com {savedFiles.Count} anexo(s).";
            return RedirectToAction(nameof(Index));
        }
    }
}
