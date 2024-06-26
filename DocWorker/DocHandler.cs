using iTextSharp.text.pdf;
using iTextSharp.text;

namespace DocWorker;

public class DocHandler
{
    public void PdfWrite()
    {
        using Document doc = new();
        PdfWriter.GetInstance(doc, new FileStream(@"C:\Main\Test\gar_delta_xml\aa.pdf", FileMode.Create));
        doc.Open();

        int a = 0;       
    }

}
