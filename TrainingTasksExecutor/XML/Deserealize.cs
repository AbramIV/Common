using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrainingTasksExecutor.XML;

internal class Deserealize
{
    readonly XmlDocument document = new();

    internal AddressObjects GetAddressObjects(string path)
    {
        AddressObjects addresses = new();
        //string path = "C:\\Main\\Test\\In\\02\\AS_ADDR_OBJ_20240429_616ade91-3774-42a5-ad2f-fac190c475e3.xml";

        string xml = File.ReadAllText(path);
        document.LoadXml(xml);

        foreach (XmlElement xnode in document.DocumentElement)
        {
            addresses.Objects.Add(new AddressObject()
            {
                Id = Convert.ToInt32(xnode.Attributes.GetNamedItem("ID")?.Value),
                ObjectId = Convert.ToInt32(xnode.Attributes.GetNamedItem("OBJECTID")?.Value),
                ObjectGUID = Guid.Parse(xnode.Attributes.GetNamedItem("OBJECTID")?.Value),
                ChangeId = Convert.ToInt32(xnode.Attributes.GetNamedItem("CHANGEID")?.Value),
                Name = xnode.Attributes.GetNamedItem("NAME")?.Value,
                TypeName = xnode.Attributes.GetNamedItem("TYPENAME")?.Value,
                Level = Convert.ToInt32(xnode.Attributes.GetNamedItem("LEVEL")?.Value),
                OperTypeId = Convert.ToInt32(xnode.Attributes.GetNamedItem("OPERTYPEID")?.Value),
                PrevId = Convert.ToInt32(xnode.Attributes.GetNamedItem("PREVID")?.Value),
                NextId = Convert.ToInt32(xnode.Attributes.GetNamedItem("NEXTID")?.Value),
                UpdateDate = DateTime.Parse(xnode.Attributes.GetNamedItem("UPDATEDATE")?.Value),
                StartDate = DateTime.Parse(xnode.Attributes.GetNamedItem("STARTDATE")?.Value),
                EndDate = DateTime.Parse(xnode.Attributes.GetNamedItem("ENDDATE")?.Value),
                IsActual = Convert.ToInt32(xnode.Attributes.GetNamedItem("ISACTUAL")?.Value) != 0,
                IsActive = Convert.ToInt32(xnode.Attributes.GetNamedItem("ISACTIVE")?.Value) != 0
            });
        }

        return addresses;
    }


    
}
