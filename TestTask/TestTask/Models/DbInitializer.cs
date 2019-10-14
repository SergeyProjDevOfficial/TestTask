using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTask.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<ScanContext>
    {
        protected override void Seed(ScanContext db)
        {
            base.Seed(db);
        }
    }
}