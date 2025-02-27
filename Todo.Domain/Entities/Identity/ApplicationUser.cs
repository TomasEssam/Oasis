﻿using Microsoft.AspNetCore.Identity;

namespace Todo.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public Guid ApplicationUserId { get; set; } = Guid.NewGuid();
        public string? RefreshToken { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string CreationUser { get; set; } = "Thomas";
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }

        public ICollection<TodoEntity> TodoEntities { get; set; }
    }
}
