using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ExcelConverterLibrary.Models
{
    [XmlRoot(ElementName = "Klient")]
    public class Client
    {
        [JsonProperty("Název klienta")]
        [XmlElement("NazevKlienta")]
        public required string Name { get; set; }

        [JsonProperty("IČ klienta")]
        [XmlElement("ICKlienta")]
        public string? CompanyID { get; set; }

        [JsonProperty("Zakázky")]
        [XmlArray("Zakazky")]
        [XmlArrayItem("Zakazka")]
        public required List<Order> Orders { get; set; }
    }

    public class Order
    {
        [JsonProperty("Název zakázky")]
        [XmlElement("NazevZakazky")]
        public required string OrderName { get; set; }

        [JsonProperty("Vyrobené kusy")]
        [XmlArray("VyrobeneKusy")]
        [XmlArrayItem("VyrobniJednotka")]
        public List<ProductionUnit>? ProductionUnits { get; set; }
    }

    public class ProductionUnit
    {
        [JsonProperty("Období")]
        [XmlElement("Obdobi")]
        public required string Period { get; set; }

        [JsonProperty("Počet kusů")]
        [XmlElement("PocetKusu")]
        public int Quantity { get; set; }
    }
}