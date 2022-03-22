using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UserRegistrationProject.Services.Abstraction;

namespace UserRegistrationProject.Pages
{
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUsersService _usersService;

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [MinLength(6, ErrorMessage = "You password must contain at least 6 characters.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$",
                           ErrorMessage = "Your password must contain at least 1 lowercase letter, 1 upper case letter and 1 special character")]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match. Please make sure both passwords are the same.")]
        public string ConfirmPassword { get; set; }

        [BindProperty, Required, Range(typeof(bool), "true", "true", ErrorMessage = "You have to agree to our TOS in order to register an account")]
        public bool TOSAgreementAccepted { get; set; }

        public RegisterModel(ILogger<RegisterModel> logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var (Id, errorMessage) = await _usersService.CreateUser(Email, Password, ConfirmPassword);
                if (Id < 1)
                {
                    ViewData["ErrorMessage"] = errorMessage;
                    return Page();
                }
                else
                {
                    ViewData["Message"] = "Account registered successfuly!";
                    ModelState.Clear();
                    Email = string.Empty;
                    TOSAgreementAccepted = false;
                }
            }

            return Page();
        }
    }
}