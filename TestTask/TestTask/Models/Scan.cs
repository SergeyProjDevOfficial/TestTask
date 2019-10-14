using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestTask.Models
{
    [Table("scan")]
    public class Scan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idUrl { get; set; }

        // entered url
        public string ScanningUrl { get; set; }

        // date and time of scan
        public string Date { get; set; }
    }
}