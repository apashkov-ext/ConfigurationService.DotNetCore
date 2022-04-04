using System.ComponentModel.DataAnnotations;

namespace ConfigurationManagementSystem.Api.Dto
{
    public class SignInDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
