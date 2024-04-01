using System.ComponentModel.DataAnnotations;
using BookStoreApp.API.Models.Author;

namespace BookStoreApp.API.Models.Book;

public class BookUpdateDto : BaseDto
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;
    
    [Required]
    [Range(1800, int.MaxValue)]
    public int? Year { get; set; }
    
    [Required]
    public string Isbn { get; set; } = null!;
    
    [Required]
    public string Summary { get; set; } = null!;
    
    public string Image { get; set; } = null!;
    
    [Required]
    [Range(0, int.MaxValue)]
    public double? Price { get; set; }

}