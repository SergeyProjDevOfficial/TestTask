using System.Data.Entity;


namespace TestTask.Models
{
    public class ScanContext : DbContext
    {
        public DbSet<Scan> scans { get; set; }
        public DbSet<ScanResult> scansResults { get; set; }
    }
}