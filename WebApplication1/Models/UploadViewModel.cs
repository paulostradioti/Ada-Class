using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UploadViewModel
    {
        [Required, StringLength(100)]
        public string Nome { get; set; } = default!;

        [StringLength(500)]
        public string? Descricao { get; set; }

        // Múltiplos arquivos
        public List<IFormFile> Anexos { get; set; } = new();
    }
}
