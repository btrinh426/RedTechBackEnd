using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace RedTechBackEnd.Models
{
    public class Order
    {
        [Key]
        [Column(TypeName = "uniqueIdentifier")]
        public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string OrderType { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string CustomerName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "datetime2(7)")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string CreatedByUsername { get; set; }

    }
}
