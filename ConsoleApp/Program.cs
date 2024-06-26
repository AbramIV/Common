using ConsoleApp.Helpers;

List<EmailSender> emails = Enumerable.Range(0, 200).Select(n => new EmailSender()).ToList();
var subject = "Logger test";
var body = "Ыыыыыыыы";
var recipients = "jorge.salazar@sks-textile.com;gerardo.nino@sks-textile.com";
//Parallel.ForEach(emails, e => e.SendDirectly(subject, body, recipients));

try
{
    
}
catch (Exception ex)
{
    File.WriteAllText(@"C:\Main\Test\logs.txt", ex.Message);
}