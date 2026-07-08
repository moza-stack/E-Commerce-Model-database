using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.Models
{
    [Table("Categories")]
    public class Category
    {
     
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto Generated
        public int categoryId { get; set; } // System Generated

        [Required] // Required
        [MaxLength(100)] // Max Length = 100
        public string categoryName { get; set; } // User Input

        [MaxLength(500)] // Max Length = 500
        public string? description { get; set; } // Optional - User Input

        [MaxLength(300)] // Max Length = 300
        public string? imageUrl { get; set; } // Optional - User Input

        
        //// reverse navigation One Category -> Many Products
    
        public virtual List<Product> Products { get; set; } =new List<Product>();  
    }
}
    

