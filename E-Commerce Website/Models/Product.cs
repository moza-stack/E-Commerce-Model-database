using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.Models
{
    public class Product
    {

        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto Generated
        public int productId { get; set; } // System Generated

        [Required] // Required
        [MaxLength(150)] // Max Length = 150
        public string productName { get; set; } // User Input

        [MaxLength(1000)] // Max Length = 1000
        public string? description { get; set; } // Optional - User Input

        [Required] // Required  
        [Column(TypeName = "decimal(10,2)")] // Decimal Data Type

        [Range(0.01, double.MaxValue)] // Must be greater than 0
        public decimal price { get; set; } // User Input

        [Required] // Required

        [Range(0, double.MaxValue)] // Must be greater than or equal to 0
        public int stockQuantity { get; set; } // User Input

        [MaxLength(300)] // Max Length = 300
        public string? imageUrl { get; set; } // Optional - User Input

        

        [Required] // Required
        public DateTime createdAt { get; set; } // System Generated

        public bool isAvailable { get; set; } = true; // Default Value




        [Required] // Required

        [ForeignKey("Category")] // Foreign Key
        public int categoryId { get; set; } // Foreign Key

        // Navigation Properties
        public Category Category { get; set; } // Many Products -> One Category


        // reverse navigation 
        public List<Review> Reviews { get; set; } = new List<Review>(); // One Product -> Many Reviews


        // reverse navigation — one Product appears in many OrderItems (bridge table)
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();  
    }
}
    
    

