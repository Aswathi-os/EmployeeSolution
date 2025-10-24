using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("Emplyee")]
    public partial class Employee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Department { get; set; }

        [StringLength(100)]
        public string? Place { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
    }

}
