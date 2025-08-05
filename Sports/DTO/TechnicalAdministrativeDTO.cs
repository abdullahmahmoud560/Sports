using Sports.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DTO
{
    public class TechnicalAdministrativeDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string attribute { get; set; } = string.Empty;
        public IFormFile? URLImage { get; set; } 
        public IFormFile? URLPassport { get; set; }
    }
}
