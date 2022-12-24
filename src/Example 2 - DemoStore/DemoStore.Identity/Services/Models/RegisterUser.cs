using System.ComponentModel.DataAnnotations;

namespace DemoStore.Identity.Services.Models
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Email cannot be null")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be null")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Password must have 10 to 50 characters")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password and confirm password must be the same")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }

        public void AddError(string error)
        {
            if (Errors is null)
                Errors = new List<string>();

            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                AddError(error);
            }
        }
    }
}
