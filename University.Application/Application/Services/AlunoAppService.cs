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
            public class AlunoAppService : IAlunoAppService
        {
            private readonly UniversityDbContext _context;
            private readonly IMapper _mapper;

            public AlunoAppService(UniversityDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<AlunoViewModel>> GetAllAsync()
            {
                var alunos = await _context.Alunos
                    .Include(a => a.Curso)
                    .ToListAsync();

                return alunos.Adapt<IEnumerable<AlunoViewModel>>();
                // ou: return _mapper.Map<IEnumerable<AlunoViewModel>>(alunos);
            }

            public async Task<AlunoViewModel?> GetByIdAsync(int id)
            {
                var aluno = await _context.Alunos
                    .Include(a => a.Curso)
                    .FirstOrDefaultAsync(a => a.Id == id);

                return aluno?.Adapt<AlunoViewModel>();
            }

            public async Task CreateAsync(AlunoViewModel vm)
            {
                var aluno = vm.Adapt<Aluno>();
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(AlunoViewModel vm)
            {
                var aluno = await _context.Alunos.FindAsync(vm.Id);
                if (aluno == null) return;

                vm.Adapt(aluno); // aplica mudanças em cima da entidade existente
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var aluno = await _context.Alunos.FindAsync(id);
                if (aluno == null) return;

                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
        }
}
