using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto Generated
        public int orderId { get; set; } // System Generated

        [Required] // Required

        [ForeignKey("User")] // Foreign Key
        public int userId { get; set; } // Foreign Key

        [Required] // Required
        public DateTime orderDate { get; set; } // User Input

        [Required] // Required
        [Column(TypeName = "decimal(10,2)")] // Decimal Data Type

        [Range(0, double.MaxValue)] // Must be greater than or equal to 0
        public decimal totalAmount { get; set; } // Calculated

        [Required] // Required

        [MaxLength(30)] // Max Length = 30
        public string status { get; set; } = "Pending"; // Default Value

        [Required] // Required

        [MaxLength(300)] // Max Length = 300
        public string shippingAddress { get; set; } // User Input

        [Required] // Required

        [MaxLength(50)] // Max Length = 50
        public string paymentMethod { get; set; } // User Input

        
        // Navigation Properties


        public virtual User User { get; set; } // Many Orders -> One User


        // reverse navigation — one Order has many OrderItems
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
    }
}
