using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Callback
    {
        /// <summary>
        /// Номер обращения.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Message { get; set; }

        public int PropertyId { get; set; }
        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; set; }

    }
}
