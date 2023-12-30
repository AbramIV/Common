using ConsoleApp.Helpers;

List<EmailSender> emails = Enumerable.Range(0, 200).Select(n => new EmailSender()).ToList();
var subject = "Logger test";
var body = "Ыыыыыыыы";
var recipients = "jorge.salazar@sks-textile.com;gerardo.nino@sks-textile.com";

try
{
    Parallel.ForEach(emails, e => e.SendDirectly(subject, body, recipients));
}
catch (Exception ex)
{
    File.WriteAllText(@"C:\Main\Test\logs.log.txt", ex.Message);
}