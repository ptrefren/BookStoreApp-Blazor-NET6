using BookStoreApp.API.Models.Author;

namespace BookStoreApp.API.Models.Book;

public class BookReadOnlyDto : BaseDto
{
    public string Title { get; set; } = null!;
    public string Image { get; set; } = null!;
    public double? Price { get; set; } = null!;
    public int? AuthorId { get; set; } = null!;
    public string? AuthorName { get; set; }
}