using Files.Documents;
using Files.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Files.Services
{
    public class CardCacheService : ICardCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDictionary<Type, MemoryCacheEntryOptions> _cardsLifeTimes;

        public CardCacheService(
            IMemoryCache memoryCache,
            IDictionary<Type, MemoryCacheEntryOptions> cardsLifeTimes)
        {
            _memoryCache = memoryCache;
            _cardsLifeTimes = cardsLifeTimes;
        }

        public void AddCard<T>(string key, T card) where T : Document
        {
            if (_cardsLifeTimes.ContainsKey(card.GetType()))
            {
                _memoryCache.Set(key, card, _cardsLifeTimes[card.GetType()]);
            }
        }

        public T GetCard<T>(string key) where T : Document
        {
            return _memoryCache.Get<T>(key);
        }
    }
}
