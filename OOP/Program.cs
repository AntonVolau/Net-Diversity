using System;
using System.Collections.Generic;
using Files;
using Files.Services;
using Microsoft.Extensions.Caching.Memory;
using Files.Documents;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            var initialBooks = GetBooks();
            var initialPatents = GetPatents();
            var initialLocalizedBooks = GetLocalizedBooks();
            var initialMagazines = GetMagazines();
            var genericRepository = new Repository();

            var withExpiration = new MemoryCacheEntryOptions();
            withExpiration.SetAbsoluteExpiration(TimeSpan.FromSeconds(15));
            var withoutExpiration = new MemoryCacheEntryOptions();
            var cardsLifeTimes = new Dictionary<Type, MemoryCacheEntryOptions>
            {
                { typeof(Patent), withExpiration },
                { typeof(LocalizedBook), withExpiration },
                { typeof(Magazine), withoutExpiration }
            };
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cardCacheService = new CardCacheService(memoryCache, cardsLifeTimes);
            var fileCabinet = new FileCabinet(genericRepository, cardCacheService);

            fileCabinet.AddCards(initialBooks);
            fileCabinet.AddCards(initialPatents);
            fileCabinet.AddCards(initialLocalizedBooks);
            fileCabinet.AddCards(initialMagazines);

            var book = fileCabinet.GetCard<Book>(1);
            var patent = fileCabinet.GetCard<Patent>(1);
            var localizedBook = fileCabinet.GetCard<LocalizedBook>(1);
            var magazine = fileCabinet.GetCard<Magazine>(1);

            var cachedPatent = fileCabinet.GetCard<Patent>(1);
            var cachedLocalizedBook = fileCabinet.GetCard<LocalizedBook>(1);
            var cachedMagazine = fileCabinet.GetCard<Magazine>(1);

            var books = new List<Book> { book };
            var patents = new List<Patent> { patent, cachedPatent };
            var localizedBooks = new List<LocalizedBook> { localizedBook, cachedLocalizedBook };
            var magazines = new List<Magazine> { magazine, cachedMagazine };

            PrintBooks(books);
            PrintPatents(patents);
            PrintLocalizedBooks(localizedBooks);
            PrintMagazines(magazines);
        }

        static IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>();
            var book1 = new Book
            {
                Id = 1,
                Title = "Lord of the Rings",
                Authors = "J. R. R. Tolkien",
                PublishDate = DateTime.UtcNow,
                ISBN = "isbn1",
                PageNumbers = 500,
                Publisher = "somePublisher"
            };
            var book2 = new Book
            {
                Id = 2,
                Title = "Harry Potter",
                Authors = "Joanne Rowling",
                PublishDate = DateTime.UtcNow,
                ISBN = "isbn2",
                PageNumbers = 301,
                Publisher = "someOtherPublisher"
            };

            books.Add(book1);
            books.Add(book2);

            return books;
        }

        static IEnumerable<Patent> GetPatents()
        {
            var patents = new List<Patent>();
            var patent = new Patent
            {
                Id = 1,
                Title = "SomePatent",
                Authors = "J. R. R. Tolkien",
                PublishDate = DateTime.UtcNow,
                UniqueId = 1,
                ExpirationDate = DateTime.UtcNow
            };
            var patent1 = new Patent
            {
                Id = 2,
                Title = "someOtherPatent",
                Authors = "Joanne Rowling",
                PublishDate = DateTime.UtcNow,
                UniqueId = 2,
                ExpirationDate = DateTime.UtcNow
            };

            patents.Add(patent);
            patents.Add(patent1);

            return patents;
        }

        static IEnumerable<LocalizedBook> GetLocalizedBooks()
        {
            var localizedBooks = new List<LocalizedBook>();
            var localizedBook = new LocalizedBook
            {
                Id = 1,
                Title = "Metro 2033",
                Authors = "Dmitry",
                PublishDate = DateTime.UtcNow,
                ISBN = "isbn1",
                PageNumbers = 500,
                Publisher = "publisher1",
                CountryOfLocalization = "Russia",
                LocalPublisher = "localPublisher1"
            };
            var localizedBook1 = new LocalizedBook
            {
                Id = 2,
                Title = "1488",
                Authors = "Steven",
                PublishDate = DateTime.UtcNow,
                ISBN = "isbn2",
                PageNumbers = 401,
                Publisher = "publisher2",
                CountryOfLocalization = "USA",
                LocalPublisher = "localPublisher2"
            };

            localizedBooks.Add(localizedBook);
            localizedBooks.Add(localizedBook1);

            return localizedBooks;
        }

        static IEnumerable<Magazine> GetMagazines()
        {
            var magazines = new List<Magazine>();
            var magazine = new Magazine
            {
                Id = 1,
                Title = "someMagazine",
                PublishDate = DateTime.UtcNow,
                Publisher = "publisher1",
                ReleaseNumber = 1
            };
            var magazine1 = new Magazine
            {
                Id = 2,
                Title = "someOtherMagazine",
                PublishDate = DateTime.UtcNow,
                Publisher = "publisher2",
                ReleaseNumber = 2
            };

            magazines.Add(magazine);
            magazines.Add(magazine1);

            return magazines;
        }

        static void PrintBooks(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"{nameof(Book)} {book.Title}");
            }
        }

        static void PrintPatents(IEnumerable<Patent> patents)
        {
            foreach (var patent in patents)
            {
                Console.WriteLine($"{nameof(Patent)} {patent.Title}");
            }
        }

        static void PrintLocalizedBooks(IEnumerable<LocalizedBook> localizedBooks)
        {
            foreach (var localizedBook in localizedBooks)
            {
                Console.WriteLine($"{nameof(LocalizedBook)} {localizedBook.Title}");
            }
        }

        static void PrintMagazines(IEnumerable<Magazine> magazines)
        {
            foreach (var magazine in magazines)
            {
                Console.WriteLine($"{nameof(Magazine)} {magazine.Title}");
            }
        }
    }
}
