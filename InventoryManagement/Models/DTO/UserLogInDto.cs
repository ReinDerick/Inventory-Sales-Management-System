using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models.DTO
{
    public class UserLogInDto
    {

        [Key]
        public required string UserName { get; set; } 

        public required string Password { get; set; }

    }
}
