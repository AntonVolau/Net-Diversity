using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Documents
{
    public class Patent : Document
    {
        public string Authors { get; set; }

        public int UniqueId { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
