using System.ComponentModel.DataAnnotations;
using Todo.Domain.Entities.Identity;

namespace Todo.Domain.Entities
{
    public class TodoEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
