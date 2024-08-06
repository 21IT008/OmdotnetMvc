using System.ComponentModel.DataAnnotations;

namespace OmdotnetMvc.Models
{
    public class User
    {
        [Required(ErrorMessage ="Plaese Provide Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Plaese Provide Email")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Plaese Confirm Email")]
        [Compare("Email",ErrorMessage ="Emails are not matching")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Plaese Enter Password")]
        [StringLength(10,MinimumLength =6,ErrorMessage ="password should be between 6 t0 10 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Plaese Choose Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Plaese Accpet Terms")]
        public bool AcceptTerms { get; set; }

        [Required(ErrorMessage = "Plaese Choose Country")]
        public string Country { get; set; }
    }
}
