using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Domain
{
    public class Turma
    {
        public Guid Id { get; set; }

        [Display(Name = "Nome da Turma")]
        [Required(ErrorMessage = "O campo Nome da Turma é obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Professores")]
        [MinLength(1, ErrorMessage = "Adicione pelo menos um professor.")]
        public List<string> Professores { get; set; } = new();
    }
}
