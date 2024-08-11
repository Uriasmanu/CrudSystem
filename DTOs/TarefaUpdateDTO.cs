namespace CrudSystem.DTOs
{
    public class TarefaUpdateDTO
    {
        public string Name { get; set; }
        public string Descritiva { get; set; }
        public Guid ProjectId { get; set; }
    }
}
