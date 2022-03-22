using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace UserRegistrationProject.Pages
{
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [MinLength(6, ErrorMessage = "You password must contain at least 6 characters.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$",
                           ErrorMessage = "Your password must contain 1 lowercase letter, 1 upper case and once special character")]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match. Please make sure both passwords are the same.")]
        public string ConfirmPassword { get; set; }

        [BindProperty, Required, Range(typeof(bool), "true", "true", ErrorMessage = "You have to agree to our TOS in order to register an account")]
        public bool TOSAgreementAccepted { get; set; }

        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                ViewData["Message"] = "Account registered successfuly!";
                ModelState.Clear();
                Email = string.Empty;
                TOSAgreementAccepted = false;
            }

            return Page();
        }
    }
}