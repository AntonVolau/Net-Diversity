using System.Collections.Generic;
using System.Linq;
using Files.Documents;
using Files.Interfaces;

namespace Files
{
    public class FileCabinet : IFileCabinet
    {
        private readonly IRepository _genericRepository;
        private readonly ICardCacheService _cardCacheService;

        public FileCabinet(
            IRepository genericRepository,
            ICardCacheService cardCacheService)
        {
            _genericRepository = genericRepository;
            _cardCacheService = cardCacheService;
        }

        public T GetCard<T>(int id) where T : Document
        {
            var key = $"{typeof(T).Name}_#{{{id}}}";
            var card = _cardCacheService.GetCard<T>(key);
            if (card == null)
            {
                var cards = GetCards<T>();
                card = cards.FirstOrDefault(x => x.Id == id);
                _cardCacheService.AddCard(key, card);
            }
            return card;
        }

        public IEnumerable<T> GetCards<T>() where T : Document
        {
            return _genericRepository.Read<T>();
        }

        public void AddCards<T>(IEnumerable<T> cards) where T : Document
        {
            _genericRepository.Write(cards);
        }
    }
}
