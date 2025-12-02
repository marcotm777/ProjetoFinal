using System;
using System.ComponentModel.DataAnnotations;

namespace University.Application.Validation
{
    public class DataNascimentoAnteriorHojeAttribute : ValidationAttribute
    {
        public DataNascimentoAnteriorHojeAttribute()
        {
            ErrorMessage = "A data de nascimento deve ser menor ou igual à data de hoje.";
        }

        public override bool IsValid(object? value)
        {
            if (value is null) return true;

            if (value is DateTime data)
            {
                return data.Date <= DateTime.Today;
            }

            return false;
        }
    }
}
