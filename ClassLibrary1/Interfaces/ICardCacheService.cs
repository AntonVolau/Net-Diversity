using Files.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files.Interfaces
{
    public interface ICardCacheService
    {
        void AddCard<T>(string key, T card) where T : Document;

        T GetCard<T>(string key) where T : Document;
    }
}
