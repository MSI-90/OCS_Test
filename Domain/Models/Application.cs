using NotionTestWork.Application;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotionTestWork.Domain.Models;

[Table("applications")]
public class Applications
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("author_id")]
    public Guid Author { get; set; }

    [Column("activity")]
    public ActivityEnum Activity { get; set; }

    [Column("crated_time")]
    public DateTime CreatedAt { get; set; }

    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Column("description")]
    [MaxLength(300)]
    public string Description { get; set; }

    [Column("outline")]
    [MaxLength(1000)]
    public string Outline { get; set; }

    [Column("is_submitted")]
    public bool IsSubmitted { get; set; } = false;
}
