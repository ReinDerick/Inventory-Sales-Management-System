using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models.Entities
{
    public class Role
    {

        [Key]

        public required int RoleId { get; set; }

        public required string RoleName { get; set; }

        public required string RoleDescription { get; set; }

        public ICollection<userRole> UserRoles { get; set; }
    }
}
