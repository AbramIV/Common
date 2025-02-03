using ConsoleApp.Helpers;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

List<EmailSender> emails = Enumerable.Range(0, 200).Select(n => new EmailSender()).ToList();
var subject = "Logger test";
var body = "Ыыыыыыыы";
var recipients = "jorge.salazar@sks-textile.com;gerardo.nino@sks-textile.com";
//Parallel.ForEach(emails, e => e.SendDirectly(subject, body, recipients));

try
{
    var tcpListener = new TcpListener(IPAddress.Parse("192.168.1.71"), 8888);
    try
    {
        tcpListener.Start();    // запускаем сервер
        Console.WriteLine("Сервер запущен. Ожидание подключений... ");

        while (true)
        {
            // получаем подключение в виде TcpClient
            using var tcpClient = await tcpListener.AcceptTcpClientAsync();
            Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");
        }
    }
    finally
    {
        tcpListener.Stop(); // останавливаем сервер
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    File.WriteAllText(@"C:\Main\Test\logs.txt", ex.Message);
}

Console.WriteLine("Done!");
Console.ReadLine();