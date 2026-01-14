using Delta.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delta.Domain.Entities.Utilities
{
    [Table("TblMenuItem")]
    public class Menu : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuID { get; set; }

        // 🔹 SELF REFERENCE (Parent Menu)
        public int? ParentID { get; set; }

        [ForeignKey(nameof(ParentID))]
        public Menu? ParentMenu { get; set; }

        public ICollection<Menu> ChildMenus { get; set; } = new List<Menu>();

        // 🔹 MENU INFO
        [Required]
        [MaxLength(100)]
        public string MenuTitle { get; set; } = null!;

        [MaxLength(200)]
        public string? MenuUrl { get; set; }

        [MaxLength(100)]
        public string? DisplayName { get; set; }

        [MaxLength(250)]
        public string? MenuDescription { get; set; }

        // 🔹 UI CONTROL
        public int IsPop { get; set; }

        [MaxLength(200)]
        public string? UrlMenuPath { get; set; }

        public int MenuOrder { get; set; }

        [MaxLength(100)]
        public string? IconClass { get; set; }

        [MaxLength(100)]
        public string? IconName { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }
    }
}
