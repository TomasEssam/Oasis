namespace Todo.Api.DTOs
{
    public class TodoEntityDto
    {
        public string Title { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
    }
}
