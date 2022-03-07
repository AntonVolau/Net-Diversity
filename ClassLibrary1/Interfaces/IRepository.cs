using Files.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Interfaces
{
    public interface IRepository
    {
        void Write<T>(IEnumerable<T> documents) where T : Document;

        IEnumerable<T> Read<T>() where T : Document;
    }
}
