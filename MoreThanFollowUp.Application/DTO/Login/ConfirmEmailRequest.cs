using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
