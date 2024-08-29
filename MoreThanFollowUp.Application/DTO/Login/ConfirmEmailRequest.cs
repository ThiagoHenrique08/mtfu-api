using System.ComponentModel.DataAnnotations;

namespace MoreThanFollowUp.Application.DTO.Login
{
    public class ConfirmEmailRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Token { get; set; }
    }
}
