using Microsoft.AspNetCore.Identity;

namespace Todo.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string CreationUser { get; set; } = "Thomas";
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
