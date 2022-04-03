using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = cookies
            };
            var client = new HttpClient(handler);

            var nameResponse = await client.GetAsync("http://localhost:8888/MyName/");
            var name = await nameResponse.Content.ReadAsStringAsync();
            Console.WriteLine(name);

            var infoResponse = await client.GetAsync("http://localhost:8888/Information/");
            var infoStatusCode = infoResponse.StatusCode.ToString();
            Console.WriteLine(infoStatusCode);

            var successResponse = await client.GetAsync("http://localhost:8888/Success/");
            var successStatusCode = successResponse.StatusCode.ToString();
            Console.WriteLine(successStatusCode);

            var redirectResponse = await client.GetAsync("http://localhost:8888/Redirection/");
            var redirectStatusCode = redirectResponse.StatusCode.ToString();
            Console.WriteLine(redirectStatusCode);

            var clientErrorResponse = await client.GetAsync("http://localhost:8888/ClientError/");
            var clientErrorStatusCode = clientErrorResponse.StatusCode.ToString();
            Console.WriteLine(clientErrorStatusCode);

            var serverErrorResponse = await client.GetAsync("http://localhost:8888/ServerError/");
            var serverErrorStatusCode = serverErrorResponse.StatusCode.ToString();
            Console.WriteLine(serverErrorStatusCode);

            var headerNameResponse = await client.GetAsync("http://localhost:8888/MyNameByHeader/");
            var headerName = headerNameResponse.Headers.GetValues("X-MyName").First();
            Console.WriteLine(headerName);

            var cookieNameResponse = await client.GetAsync("http://localhost:8888/MyNameByCookies/");
            var cookieName = cookies.GetCookies(new Uri("http://localhost:8888/MyNameByCookies/"))
                .First(x => x.Name == "MyName").Value;
            Console.WriteLine(cookieName);
        }
    }
}
