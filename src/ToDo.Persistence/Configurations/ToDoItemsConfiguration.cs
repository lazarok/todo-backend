using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Entities;

namespace ToDo.Persistence.Configurations;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.ToTable("ToDoItems");
        builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();

        builder
            .HasOne(s => s.ToDoList)
            .WithMany(g => g.Items)
            .HasForeignKey(s => s.ToDoListId);
    }
}