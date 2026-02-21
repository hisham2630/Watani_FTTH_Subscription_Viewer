using ClosedXML.Excel;

namespace WataniFTTH.Services;

public static class ExcelExporter
{
    public static void Export(
        List<string> headers,
        List<string[]> rows,
        string filePath,
        IProgress<(int current, int total, string message)>? progress = null)
    {
        var totalRows = rows.Count;
        progress?.Report((0, totalRows, "جاري إنشاء ملف الاكسل..."));

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("الاشتراكات");
        worksheet.RightToLeft = false;

        // Header row
        for (int i = 0; i < headers.Count; i++)
        {
            worksheet.Cell(1, i + 1).Value = headers[i];
        }

        // Data rows
        for (int row = 0; row < totalRows; row++)
        {
            for (int col = 0; col < headers.Count; col++)
            {
                worksheet.Cell(row + 2, col + 1).Value = rows[row][col];
            }

            if (row % 50 == 0 || row == totalRows - 1)
            {
                progress?.Report((row + 1, totalRows, $"جاري الكتابة... {row + 1}/{totalRows}"));
            }
        }

        progress?.Report((totalRows, totalRows, "جاري تطبيق التنسيق..."));

        // Apply Table Style Medium 9 (blue)
        var range = worksheet.Range(1, 1, totalRows + 1, headers.Count);
        var table = range.CreateTable();
        table.Theme = XLTableTheme.TableStyleMedium9;

        // Center all cells
        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        // Customer Name column (col 1): bold, Segoe UI, size 16
        var nameRange = worksheet.Range(2, 1, totalRows + 1, 1);
        nameRange.Style.Font.Bold = true;
        nameRange.Style.Font.FontSize = 16;
        nameRange.Style.Font.FontName = "Segoe UI";

        // Phone column (col 2): bold, Segoe UI, size 22
        var phoneRange = worksheet.Range(2, 2, totalRows + 1, 2);
        phoneRange.Style.Font.Bold = true;
        phoneRange.Style.Font.FontSize = 22;
        phoneRange.Style.Font.FontName = "Segoe UI";

        progress?.Report((totalRows, totalRows, "جاري حفظ الملف..."));

        worksheet.Columns().AdjustToContents();
        workbook.SaveAs(filePath);

        progress?.Report((totalRows, totalRows, "تم التصدير بنجاح!"));
    }
}
