using Files.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Interfaces
{
    public interface IFileCabinet
    {
        T GetCard<T>(int id) where T : Document;

        IEnumerable<T> GetCards<T>() where T : Document;

        void AddCards<T>(IEnumerable<T> cards) where T : Document;
    }
}
