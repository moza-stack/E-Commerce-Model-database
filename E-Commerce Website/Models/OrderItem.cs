using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.Models
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto Generated
        public int orderItemId { get; set; }    // system generated


        // relationship attribute 
        [Required]
        [Range(1, 999)]
        public int quantity { get; set; }                 // user input



        [Required] // Required
        [ForeignKey("Order")] // Foreign Key
        public int orderId { get; set; } // Foreign Key
        public Order Order { get; set; }                  // navigation property


        [Required] // Required
        [ForeignKey("Product")] // Foreign Key
        public int productId { get; set; } // Foreign Key
        public Product Product { get; set; }              // navigation property


        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal unitPrice { get; set; }            // calculated 
    }
}
