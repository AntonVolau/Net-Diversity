using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Documents
{
    public class Book : Document
    {

        public string Authors { get; set; }

        public string ISBN { get; set; }

        public int PageNumbers { get; set; }

        public string Publisher { get; set; }
    }
}
