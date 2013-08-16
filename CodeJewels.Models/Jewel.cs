using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJewels.Models
{
    public class Jewel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string AuthorEmail { get; set; }
        public int Rating { get; set; }
        public string SourceCode { get; set; }
    }
}
