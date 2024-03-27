using System.ComponentModel.DataAnnotations;

namespace NotionTestWork.Models.EfClasses
{
    public class User
    {
        public Guid Id { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
