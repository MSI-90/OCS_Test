using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotionTestWork.Domain.Models;

namespace DataAccess.Configuration;
internal class ApplicationConfiguration : IEntityTypeConfiguration<UserReport>
{
    public void Configure(EntityTypeBuilder<UserReport> builder)
    {
        builder.ToTable("application");
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Author).HasColumnName("author_id");
        builder.Property(a => a.Activity).HasColumnName("activity");
        builder.Property(a => a.CreatedAt).HasColumnName("crated_time");
        builder.Property(a => a.Name).HasColumnName("name").HasMaxLength(100);
        builder.Property(a => a.Description).HasColumnName("description").HasMaxLength(300);
        builder.Property(a => a.Outline).HasColumnName("outline").HasMaxLength(1000);
        builder.Property(a => a.IsSubmitted).HasColumnName("is_submitted");
    }
}
