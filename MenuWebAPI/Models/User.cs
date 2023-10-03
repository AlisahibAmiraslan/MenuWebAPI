using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MenuWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
