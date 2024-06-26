using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrainingTasksExecutor.Http;

internal class MyClient
{
    private HttpClient client;

    internal MyClient() 
    {
        client = new();
    }

    internal void Request()
    {
        var xml = client.GetAsync("http://fias.nalog.ru/WebServices/Public/DownloadService.asmx");

        var a = 0;
    }

    internal void Request2(string url, string envelope, string action)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("SOAPAction", action);
        request.ContentType = "text/xml;charset=\"utf-8\"";
        request.Accept = "text/xml";
        request.Method = "POST";

        XmlDocument xml = new();
        xml.LoadXml(envelope);

        using Stream stream = request.GetRequestStream();
        xml.Save(stream);

        IAsyncResult asyncResult = request.BeginGetResponse(null, null);
        asyncResult.AsyncWaitHandle.WaitOne();

        using WebResponse webResponse = request.EndGetResponse(asyncResult);
        using StreamReader rd = new(webResponse.GetResponseStream());
        string soapResult = rd.ReadToEnd();
        Console.Write(soapResult);
    }
}
