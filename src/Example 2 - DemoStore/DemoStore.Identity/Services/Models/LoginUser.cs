using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DemoStore.Identity.Services.Models
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email cannot be null")]
        [EmailAddress(ErrorMessage = "Invalid e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be null")]
        public string Password { get; set; }
    }

    public class LoginUserResponse
    {
        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; set; }
        public List<string> Errors { get; set; }
        public void AddError(string error)
        {
            if (Errors is null)
                Errors = new List<string>();

            Errors.Add(error);
        }

    }
}
