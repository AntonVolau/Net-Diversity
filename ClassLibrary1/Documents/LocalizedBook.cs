using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Documents
{
    public class LocalizedBook : Book
    {
        public string CountryOfLocalization { get; set; }

        public string LocalPublisher { get; set; }
    }
}
