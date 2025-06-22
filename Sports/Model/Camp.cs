using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class Camp
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? task { get; set; }
        public string? Time { get; set; }
        public Academy? Academy { get; set; }
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
    }
}
