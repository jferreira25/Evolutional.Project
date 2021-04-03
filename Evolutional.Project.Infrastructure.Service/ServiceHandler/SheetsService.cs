using Evolutional.Project.Domain.Services.Xlsx;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Evolutional.Project.Infrastructure.Service.ServiceHandler
{
    public class SheetsService : ISheetsService
    {
        public byte[] Generate(string sheetName, IEnumerable<object> objects)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add(sheetName);

                ws.Cells["A1"].LoadFromDataTable(ToDataTable(objects), true, TableStyles.Light1);

                ws.Cells.AutoFitColumns();

                var fileBytes = pck.GetAsByteArray();
                return fileBytes;
            }
        }

        private static DataTable ToDataTable(IEnumerable<object> items)
        {
            var first = items.FirstOrDefault();

            var dataTable = new DataTable(first.GetType().Name);

            foreach (var prop in GeneratorHelper.GetPropertiesInfo(first))
            {
                dataTable.Columns.Add(prop.GetDescriptionOrPropertyName());
            }

            foreach (var item in items)
            {
                var properties = GeneratorHelper.GetPropertiesInfo(first);

                var values = new object[properties.Length];

                for (var i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValueFromPropertyInfo(item);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
