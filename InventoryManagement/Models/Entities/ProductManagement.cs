using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models.Entities
{
    public class ProductManagement
    {

        [Key]

        public Guid ProductId { get; set; }

        public required string ProductCode { get; set; }

        public required string ProductName { get; set; }

        public required decimal ProductPrice { get; set; }

        public required int ProductQuantity { get; set; }

        public required bool IsActive { get; set; }

        public required string ProductCategory { get; set; }

    }
}
