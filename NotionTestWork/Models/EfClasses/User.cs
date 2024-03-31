using System.ComponentModel.DataAnnotations;

namespace NotionTestWork.Models.EfClasses
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(30)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
