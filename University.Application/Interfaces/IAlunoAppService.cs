using System.Collections.Generic;
using System.Threading.Tasks;
using University.Application.ViewModels;

namespace University.Application.Interfaces
{
    public interface IAlunoAppService
    {
        Task<IEnumerable<AlunoViewModel>> GetAllAsync();
        Task<AlunoViewModel?> GetByIdAsync(int id);
        Task CreateAsync(AlunoViewModel aluno);
        Task UpdateAsync(AlunoViewModel aluno);
        Task DeleteAsync(int id);
    }
}

