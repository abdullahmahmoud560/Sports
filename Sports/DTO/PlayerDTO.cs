using Sports.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DTO
{
    public class PlayerDTO
    {
        public string? PLayerName { get; set; }
        public string? AcademyName { get; set; }
        public string? Category { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Possition { get; set; }
        public int NumberShirt { get; set; }
        public int Goals { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public string? Nationality { get; set; }
    }
}
