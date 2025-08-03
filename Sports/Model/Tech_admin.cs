using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.Model
{
    public class Tech_admin
    {
        public int id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Academy? Academy { get; set; }
        [ForeignKey(nameof(Academy))]
        public int AcademyId { get; set; }
        public string AcademyName { get; set; } = string.Empty;
        public string attribute { get; set; } = string.Empty;
        public string URLImage { get; set; } = string.Empty;
        public string URLPassport { get; set; } = string.Empty;
    }
}
