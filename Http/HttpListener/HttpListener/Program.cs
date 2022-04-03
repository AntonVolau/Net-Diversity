using System.Linq;
using System.Net;
using System.Text;

namespace Listener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");

            while (true)
            {
                listener.Start();
                var context = listener.GetContext();
                var request = context.Request;
                using var response = context.Response;
                var method = GetMethod(request);
                if (method == "MyName")
                {
                    GetMyName(response);
                }
                if (method == "Information")
                {
                    response.StatusCode = (int)HttpStatusCode.SwitchingProtocols;
                    response.KeepAlive = false;
                    GetMyName(response);
                }
                if (method == "Success")
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    GetMyName(response);
                }
                if (method == "Redirection")
                {
                    response.StatusCode = (int)HttpStatusCode.Redirect;
                    GetMyName(response);
                }
                if (method == "ClientError")
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    GetMyName(response);
                }
                if (method == "ServerError")
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    GetMyName(response);
                }
                if (method == "MyNameByHeader")
                {
                    GetMyNameByHeader(response);
                }
                if (method == "MyNameByCookies")
                {
                    MyNameByCookies(response);
                }
                listener.Stop();
            }
        }

        static string GetMethod(HttpListenerRequest request)
        {
            var path = request.Url.AbsolutePath;
            var method = path
                .Split("/")
                .First(x => !string.IsNullOrEmpty(x));
            return method;
        }

        static void GetMyName(HttpListenerResponse response)
        {
            using var output = response.OutputStream;
            var buffer = Encoding.UTF8.GetBytes("Anton");
            response.ContentLength64 = buffer.Length;
            output.Write(buffer, 0, buffer.Length);
        }

        static void GetMyNameByHeader(HttpListenerResponse response)
        {
            response.AddHeader("X-MyName", "Anton");
            GetMyName(response);
        }

        static void MyNameByCookies(HttpListenerResponse response)
        {
            response.Cookies.Add(new Cookie
            {
                Name = "MyName",
                Value = "Anton"
            });
            GetMyName(response);
        }
    }
}
