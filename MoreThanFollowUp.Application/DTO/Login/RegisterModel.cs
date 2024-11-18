using System.ComponentModel.DataAnnotations;

namespace MoreThanFollowUp.Application.DTO.Login
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Completed Name is required")]
        public string? CompletedName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Function is required")]
        public string? Function { get; set; }


        [Required(ErrorMessage = "EnterpriseName is required")]
        public string? EnterpriseName { get; set; }


    }
}
