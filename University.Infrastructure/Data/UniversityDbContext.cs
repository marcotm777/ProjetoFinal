using University.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace University.Infrastructure.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; } = null!;
        public DbSet<Curso> Cursos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Nome)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Nome)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasOne(a => a.Curso)
                      .WithMany(c => c.Alunos)
                      .HasForeignKey(a => a.CursoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
