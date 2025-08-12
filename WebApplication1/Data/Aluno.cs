using Application.Domain;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class Aluno
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo Nome deve ter no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Turma é obrigatório")]
        public Guid? TurmaId { get; set; }

        public Turma? Turma { get; set; }
    }
}
