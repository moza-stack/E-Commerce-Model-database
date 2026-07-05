using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website.Models
{
   
        public class User
        {
       

            [Key] // Primary Key
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto Generated
            public int userId { get; set; } // System Generated

            [Required] // Required
            [MaxLength(50)] // Max Length = 50
            public string username { get; set; } // User Input

            [Required] // Required
            [MaxLength(150)] // Max Length = 150
            public string email { get; set; } // User Input


            [Required] // Required
            [MaxLength(256)] // Max Length = 256
            public string passwordHash { get; set; } // User Input

            [Required] // Required
            [MaxLength(100)] // Max Length = 100
            public string fullName { get; set; } // User Input

            [MaxLength(20)] // Max Length = 20
            public string? phoneNumber { get; set; } // Optional - User Input

            [MaxLength(300)] // Max Length = 300
            public string? address { get; set; } // Optional - User Input

            [Required] // Required
            public DateTime registrationDate { get; set; } // User Input

            public bool isActive { get; set; } = true; // Default Value

            
            
        //reverse Navigation 
           
            
            public List<Order> Orders { get; set; }  // One User -> Many Orders


            // reverse navigation
        public List<Review> Reviews { get; set; } // One User -> Many Reviews
        }
    }

