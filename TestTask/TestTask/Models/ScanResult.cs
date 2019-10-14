using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestTask.Models
{
    [Table("scan_result")]
    public class ScanResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        public int idUrl { get; set; }

        public string SubUrl { get; set; }

        public string MinResponseTime { get; set; }
        public string MaxResponseTime { get; set; }
        public string MidResponseTime { get; set; }
    }
}