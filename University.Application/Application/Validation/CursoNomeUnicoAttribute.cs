using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using University.Infrastructure.Data;

namespace University.Application.Validation
{
    public class CursoNomeUnicoAttribute : ValidationAttribute
    {
        public CursoNomeUnicoAttribute()
        {
            ErrorMessage = "Já existe um curso cadastrado com esse nome.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            var nome = value.ToString()!.Trim();

            var db = validationContext.GetService<UniversityDbContext>();
            if (db == null) return ValidationResult.Success;

            // Se estiver editando, ignora o próprio Id
            var viewModel = validationContext.ObjectInstance;
            var idProp = viewModel.GetType().GetProperty("Id");
            var id = idProp != null ? (int)idProp.GetValue(viewModel)! : 0;

            var existe = db.Cursos.Any(c => c.Nome == nome && c.Id != id);

            return existe ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }
}
