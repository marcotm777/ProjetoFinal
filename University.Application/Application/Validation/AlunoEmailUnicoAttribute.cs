using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using University.Infrastructure.Data;

namespace University.Application.Validation
{
    public class AlunoEmailUnicoAttribute : ValidationAttribute
    {
        public AlunoEmailUnicoAttribute()
        {
            ErrorMessage = "Já existe um aluno cadastrado com esse e-mail.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            var email = value.ToString()!.Trim();

            var db = validationContext.GetService<UniversityDbContext>();
            if (db == null) return ValidationResult.Success;

            // pega o Id do VM para ignorar o próprio registro em edição
            var viewModel = validationContext.ObjectInstance;
            var idProp = viewModel.GetType().GetProperty("Id");
            var id = idProp != null ? (int)idProp.GetValue(viewModel)! : 0;

            var existe = db.Alunos.Any(a => a.Email == email && a.Id != id);

            return existe ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }
}
