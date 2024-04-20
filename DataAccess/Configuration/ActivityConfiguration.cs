using Domain.Models;
using NotionTestWork.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;

namespace DataAccess.Configuration;
internal class ActivityConfiguration : IEntityTypeConfiguration<ActivityType>
{
    public void Configure(EntityTypeBuilder<ActivityType> builder)
    {
        builder.ToTable("activities");
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.TypeOfActivity).HasColumnName("activity_kind").HasMaxLength(20);
        builder.Property(a => a.Description).HasColumnName("description").HasMaxLength(100);

        builder.HasData
        (
            new ActivityType
            {
                Id = 1,
                TypeOfActivity = ActivityEnum.Report,
                Description = "Доклад, 35 - 45 минут"
            },
            new ActivityType
            {
                Id = 2,
                TypeOfActivity = ActivityEnum.Masterclass,
                Description = "Мастеркласс, 1-2 часа"
            },
            new ActivityType
            {
                Id = 3,
                TypeOfActivity = ActivityEnum.Discussion,
                Description = "Дискуссия/круглый стол, 40-50 минут"
            }
        );
    }
}
