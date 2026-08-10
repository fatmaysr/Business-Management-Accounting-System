using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace BusinessLayer.Concrete
{
    public class ExcelExportService
    {
        public byte[] ExportToExcel<T>(List<T> dataList, string sheetName, Dictionary<string, string> columnMappings)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheetName);

                // 1. Başlıkları Yaz
                int columnIndex = 1;
                foreach (var mapping in columnMappings)
                {
                    var cell = worksheet.Cell(1, columnIndex);
                    cell.Value = mapping.Value;
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#3b82f6");
                    cell.Style.Font.FontColor = XLColor.White;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    columnIndex++;
                }

                // 2. Verileri Doldur
                int rowIndex = 2;
                foreach (var item in dataList)
                {
                    columnIndex = 1;
                    foreach (var mapping in columnMappings)
                    {
                        var prop = typeof(T).GetProperty(mapping.Key);
                        if (prop != null)
                        {
                            var val = prop.GetValue(item, null);
                            worksheet.Cell(rowIndex, columnIndex).Value = val != null ? val.ToString() : "";
                        }
                        columnIndex++;
                    }
                    rowIndex++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}