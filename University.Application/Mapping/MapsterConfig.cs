using Mapster;
using Microsoft.Extensions.DependencyInjection;
using University.Application.ViewModels;
using University.Domain.Entities;

namespace University.Application.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            // Aluno ↔ AlunoViewModel
            config.NewConfig<Aluno, AlunoViewModel>()
                  .Map(dest => dest.CursoNome, src => src.Curso != null ? src.Curso.Nome : string.Empty);

            config.NewConfig<AlunoViewModel, Aluno>();

            // Curso ↔ CursoViewModel
            config.NewConfig<Curso, CursoViewModel>();
            config.NewConfig<CursoViewModel, Curso>();

            services.AddSingleton(config);
            services.AddScoped<MapsterMapper.IMapper, MapsterMapper.ServiceMapper>();
        }
    }
}
