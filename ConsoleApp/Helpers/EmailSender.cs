using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsOutlook = Microsoft.Office.Interop.Outlook;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers;

internal class EmailSender : IDisposable
{
    private readonly MsOutlook.Application outlookApp;

    internal EmailSender() => outlookApp = new();

    internal void SendDirectly(string subject, string body, string recipient)
    {
        MsOutlook.MailItem mail = (MsOutlook.MailItem)outlookApp.CreateItem(MsOutlook.OlItemType.olMailItem);

        mail.Subject = subject;
        mail.HTMLBody = body;
        mail.To = recipient;
        mail.Send();
    }

    public void Dispose()
    {
        outlookApp?.Quit();
        GC.Collect();
    }
}

