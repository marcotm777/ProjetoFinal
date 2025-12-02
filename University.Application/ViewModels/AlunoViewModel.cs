using System;
using System.ComponentModel.DataAnnotations;
using University.Application.Validation;

namespace University.Application.ViewModels
{
    public class AlunoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [AlunoEmailUnico]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [DataNascimentoAnteriorHoje]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Selecione um curso.")]
        public int CursoId { get; set; }

        public string CursoNome { get; set; } = string.Empty;
    }
}
