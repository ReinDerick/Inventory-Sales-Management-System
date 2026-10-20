using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models.Entities
{
    public class userRole
    {

        [Key]

        public required string UserRoleId { get; set; }

        public Guid UserId { get; set; }

        public int RoleId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; }

        public bool IsActive { get; set; }

        public Role? Roles { get; set; }

        public userAccount? UserAccount { get; set; }

    }
}
