using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
    public class Department
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        public int? FloorNumber { get; set; }

        public ICollection<Employee> Employees { get; set; } // Navigation property for related employees
    }
}