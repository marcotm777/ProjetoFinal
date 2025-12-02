using System.ComponentModel.DataAnnotations;
using University.Application.Validation;

namespace University.Application.ViewModels
{
    public class CursoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do curso é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do curso deve ter no máximo 100 caracteres.")]
        [CursoNomeUnico]
        public string Nome { get; set; } = string.Empty;
    }
}
