namespace University.Domain.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}
