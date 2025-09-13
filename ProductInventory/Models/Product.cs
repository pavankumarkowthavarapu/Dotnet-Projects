using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInventory.Models
{
    public class Product
    {
        
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(15, ErrorMessage = "Name Can't exceed 15 characters")]
        //[RegularExpression("^[a-zA-Z]+$")]
        public string?  Name { get; set; }


        //---------------->Pending<------------------
       
        [Required(ErrorMessage = "Price is required")]
         public decimal Price { get; set; }
      
        
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 9999, ErrorMessage = "Quantity Must be 0 and 9999")]
        public int Quantity { get; set; }



        //---------------->Pending<------------------


        [Required(ErrorMessage = "Category is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]

        public string? Category { get; set; }
        [NotMapped]
        [Display(Name="Choose the Images of Your products")]
        [Required]
        public IFormFile?  ProductImage { get; set; }

        public string? ImageUrl { get; set; }


    }
}
