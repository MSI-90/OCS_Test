using System.ComponentModel.DataAnnotations;

namespace NotionTestWork.Models.EfClasses
{

    public class Application
    {
        public Guid Id { get; set; }
        public User Author { get; set; }
        public ActivityEnum activity { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Unsubmitted { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string Outline { get; set; }
    }
}
