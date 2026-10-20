using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models.DTO
{
    public class UserSignInDto
    {

        [Key]
        public required string UserName { get; set; }

        public required string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }

        public string CreatedBy { get; set; }


    }
}
