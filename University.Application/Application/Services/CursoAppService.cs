using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Application.Interfaces;
using University.Application.ViewModels;
using University.Domain.Entities;
using University.Infrastructure.Data;
using Mapster;
using MapsterMapper;

namespace University.Application.Services
{
   public class CursoAppService : ICursoAppService
{
    private readonly UniversityDbContext _context;
    private readonly IMapper _mapper;

    public CursoAppService(UniversityDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CursoViewModel>> GetAllAsync()
    {
        var cursos = await _context.Cursos.ToListAsync();
        return cursos.Adapt<IEnumerable<CursoViewModel>>();
    }

    public async Task<CursoViewModel?> GetByIdAsync(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        return curso?.Adapt<CursoViewModel>();
    }

    public async Task CreateAsync(CursoViewModel vm)
    {
        var curso = vm.Adapt<Curso>();
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CursoViewModel vm)
    {
        var curso = await _context.Cursos.FindAsync(vm.Id);
        if (curso == null) return;

        vm.Adapt(curso);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso == null) return;

        _context.Cursos.Remove(curso);
        await _context.SaveChangesAsync();
    }
}

}
