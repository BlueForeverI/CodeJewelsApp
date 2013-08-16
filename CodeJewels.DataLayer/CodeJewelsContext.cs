using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeJewels.Models;

namespace CodeJewels.DataLayer
{
    public class CodeJewelsContext : DbContext
    {
        public CodeJewelsContext() : base("CodeJewelsDb")
        {
            
        }

        public DbSet<Jewel> Jewels { get; set; } 
    }
}
