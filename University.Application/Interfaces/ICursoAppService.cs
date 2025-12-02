using System.Collections.Generic;
using System.Threading.Tasks;
using University.Application.ViewModels;

namespace University.Application.Interfaces
{
    public interface ICursoAppService
    {
        Task<IEnumerable<CursoViewModel>> GetAllAsync();
        Task<CursoViewModel?> GetByIdAsync(int id);
        Task CreateAsync(CursoViewModel curso);
        Task UpdateAsync(CursoViewModel curso);
        Task DeleteAsync(int id);
    }
}
