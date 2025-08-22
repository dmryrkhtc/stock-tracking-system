using System.ComponentModel.DataAnnotations;

namespace STS.Application.DTOs.Users
{
    public class UserUpdateInfoDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
