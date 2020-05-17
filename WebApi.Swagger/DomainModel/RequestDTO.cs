using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Swagger.DomainModel
{
    public class RequestDTO
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }


        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}