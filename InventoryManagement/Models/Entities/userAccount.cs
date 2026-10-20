using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models.Entities
{
    public class userAccount
    {

        [Key]

        public Guid UserId { get; set; }

        public required string UserName { get; set; } 

        public required string Password { get; set; }

        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }

        public required string CreatedBy { get; set; }

        public required DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public ICollection<userRole> UserRoles { get; set; }
    }
}
