using ClosedXML.Excel;
using ExcelConverterLibrary.Models;
using System.Xml.Serialization;

namespace ExcelConverterLibrary
{
    public class XmlExcelConverter : IExcelConverter
    {
        public string Convert(string filePath)
        {
            List<Client> clients = ConvertExcelToClients(filePath);
            return SerializeToXml(clients);
        }

        private static List<Client> ConvertExcelToClients(string filePath)
        {
            Dictionary<string, Client> clients = new Dictionary<string, Client>();

            using (var workbook = new XLWorkbook(filePath))
            {
                IXLWorksheet sheet = workbook.Worksheet(1);
                IXLRangeRows rows = sheet.RangeUsed().RowsUsed();

                foreach (var rangeRow in rows.Skip(1)) // Skip header
                {
                    ProcessRow(rangeRow, sheet, clients);
                }
            }

            return new List<Client>(clients.Values);
        }

        private static void ProcessRow(IXLRangeRow row, IXLWorksheet sheet, Dictionary<string, Client> clients)
        {
            string clientName = row.Cell(1).GetString();
            string companyID = row.Cell(2).GetString();
            string orderName = row.Cell(3).GetString();

            if (string.IsNullOrWhiteSpace(clientName) || string.IsNullOrWhiteSpace(companyID) || string.IsNullOrWhiteSpace(orderName))
                return;

            if (!clients.ContainsKey(companyID))
            {
                clients[companyID] = new Client
                {
                    Name = clientName,
                    CompanyID = companyID,
                    Orders = new List<Order>()
                };
            }

            var order = new Order
            {
                OrderName = orderName,
                ProductionUnits = ProcessProductionUnits(row, sheet)
            };

            clients[companyID].Orders.Add(order);
        }

        private static List<ProductionUnit> ProcessProductionUnits(IXLRangeRow row, IXLWorksheet sheet)
        {
            var productionUnits = new List<ProductionUnit>();

            for (int col = 4; col <= sheet.LastColumnUsed().ColumnNumber(); col++)
            {
                var cell = row.Cell(col);
                if (cell.IsEmpty()) continue;

                string period = sheet.Cell(1, col).GetDateTime().ToString("MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                int quantity = cell.GetValue<int>();

                productionUnits.Add(new ProductionUnit
                {
                    Period = period,
                    Quantity = quantity
                });
            }

            return productionUnits;
        }

        private static string SerializeToXml(List<Client> clients)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Client>), new XmlRootAttribute("Klienti"));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, clients);
                return writer.ToString();
            }
        }
    }
}