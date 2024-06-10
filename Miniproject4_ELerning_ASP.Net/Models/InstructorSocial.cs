using System.ComponentModel.DataAnnotations.Schema;

namespace Miniproject4_ELerning_ASP_MVC.Models
{
    public class InstructorSocial : BaseEntity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Instructor))]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        [ForeignKey(nameof(Social))]
        public int SocialId { get; set; }
        public Social Social { get; set; }
        public string Link { get; set; }
    }
}
