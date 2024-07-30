using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Domain.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<TodoEntity>
    {
        public void Configure(EntityTypeBuilder<TodoEntity> builder)
        {
            builder.ToTable(name : "TodoEntity")
                .HasOne(t => t.User)
                .WithMany(u => u.TodoEntities)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
