using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Resources
{
    public class RevokeTokenResource
    {
        [Required]
        public string Token { get; set; }
    }
}