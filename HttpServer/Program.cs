using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Console;

string url = "http://localhost:8080/";
HttpListener listener = new();
listener.Prefixes.Add(url);
listener.Start();

WriteLine($"Server running at {url}");

while (true)
{
    HttpListenerContext context = await listener.GetContextAsync();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;

    if (request.HttpMethod == "GET")
    {
        byte[] buffer = [];
        response.ContentLength64 = buffer.Length;
        response.ContentType = "text/plain";

        var serial_1 = request.QueryString["productSerial1"];
        var serial_2 = request.QueryString["productSerial2"];
        var side = request.QueryString["side"];

        if (string.IsNullOrEmpty(serial_1) || string.IsNullOrEmpty(serial_2) || string.IsNullOrEmpty(side))
        {
            WriteLine("Error");
            Encoding.UTF8.GetBytes("Error");
            response.StatusCode = (int)HttpStatusCode.BadRequest; 
        }

        if (serial_1.Equals("PIECE1") & serial_2.Equals("PIECE2") & side.Equals("RH"))
        {
            WriteLine("Valid");
            Encoding.UTF8.GetBytes("Valid");
            response.StatusCode = (int)HttpStatusCode.OK;
        }
        else
        {
            WriteLine("Invalid");
            Encoding.UTF8.GetBytes("Invalid");
            response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        await response.OutputStream.WriteAsync(buffer);
    }
    else
    {
        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed; // Method Not Allowed
    }

    response.Close();
}