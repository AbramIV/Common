using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Exercise.XML;

[XmlRoot("OBJECT")]
public class AddressObject
{
    [XmlAttribute("ID")]
    public int Id { get; set; }

    [XmlAttribute("OBJECTID")]
    public int ObjectId { get; set; }

    [XmlAttribute("OBJECTGUID")]
    public Guid ObjectGUID { get; set; }

    [XmlAttribute("CHANGEID")]
    public int ChangeId { get; set; }

    [XmlAttribute("NAME")]
    public string Name { get; set; }

    [XmlAttribute("TYPENAME")]
    public string TypeName { get; set; }

    [XmlAttribute("LEVEL")]
    public int Level { get; set; }

    [XmlAttribute("OPERTYPEID")]
    public int OperTypeId { get; set; }

    [XmlAttribute("PREVID")]
    public int PrevId { get; set; }

    [XmlAttribute("NEXTID")]
    public int NextId { get; set; }

    [XmlAttribute("UPDATEDATE")]
    public DateTime UpdateDate { get; set; }

    [XmlAttribute("STARTDATE")]
    public DateTime StartDate { get; set; }

    [XmlAttribute("ENDDATE")]
    public DateTime EndDate { get; set; }

    [XmlAttribute("ISACTUAL")]
    public bool IsActual { get; set; }

    [XmlAttribute("ISACTIVE")]
    public bool IsActive { get; set; }
}

[XmlRoot("ADDRESSOBJECTS")]
public class AddressObjects
{
    [XmlElement("OBJECT")]
    public List<AddressObject> Objects { get; set; }
}