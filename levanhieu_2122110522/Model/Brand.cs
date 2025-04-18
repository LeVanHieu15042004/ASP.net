using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace levanhieu_2122110522.Model
{
    [Table("Brands")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Slug { get; set; }

        [MaxLength(1000)]
        public string? Image { get; set; }

        public string? Description { get; set; }

        public uint? SortOrder { get; set; }

        public uint? CreatedBy { get; set; }

        public uint? UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public uint Status { get; set; } = 1;

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
