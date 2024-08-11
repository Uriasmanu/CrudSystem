using Microsoft.CodeAnalysis;

namespace CrudSystem.Models
{
    public class Tarefas
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descritiva { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}