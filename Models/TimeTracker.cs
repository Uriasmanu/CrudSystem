namespace CrudSystem.Models
{
    public class TimeTracker
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeZoneId { get; set; }
        public Guid TarefasId { get; set; }
        public Tarefas Tarefas { get; set; }
        public Guid CollaboratorId { get; set; }
        public Collaborator Collaborator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}