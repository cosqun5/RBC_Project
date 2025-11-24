using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee
    {
        [Key]
        public int EmployeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; }

        [Required]
        [MaxLength(50)]
        public string Department { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Salary { get; set; }

        public byte[]? FileBlob { get; set; }

        [MaxLength(260)]
        public string? FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
