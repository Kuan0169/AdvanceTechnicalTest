using System.ComponentModel.DataAnnotations;

namespace MyCompany.Test.Infrastructure.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
