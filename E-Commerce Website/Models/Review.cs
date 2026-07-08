using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.Models
{
    [Table("Reviews")]
    public class Review
    {
    
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto Generated
        public int reviewId { get; set; } // System Generated


        [Required] // Required
        [Range(1, 5)] // Rating must be between 1 and 5
        public int rating { get; set; } // User Input

        [MaxLength(1000)] // Max Length = 1000
        public string? comment { get; set; } // Optional - User Input

        [Required] // Required
        public DateTime reviewDate { get; set; } // User Input


        
        [Required] // Required
        [ForeignKey("User")] // Foreign Key
        public int userId { get; set; } // Foreign Key

        // Navigation Properties
        public virtual User User { get; set; } // Many Reviews -> One User


        [Required] // Required
        [ForeignKey("Product")] // Foreign Key
        public int productId { get; set; } // Foreign Key

        // navigation property
        public virtual Product product { get; set; } // Many Reviews -> One Product
    }
}
    
