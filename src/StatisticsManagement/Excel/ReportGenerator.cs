using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using StatisticsManagement.OutputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.Excel;

internal class ReportGenerator
{
    private readonly List<OutputPlayer> _players;

    private readonly XLColor HeaderBackground = XLColor.FromArgb(197, 217, 241);
    private readonly XLColor GoodFont = XLColor.FromArgb(0, 97, 0);
    private readonly XLColor GoodBackground = XLColor.FromArgb(198, 239, 206);
    private readonly XLColor BadFont = XLColor.FromArgb(156, 0, 6);
    private readonly XLColor BadBackground = XLColor.FromArgb(255, 199, 206);
    private readonly XLColor TopFont = XLColor.White;
    private readonly XLColor TopBackground = XLColor.FromArgb(63, 201, 89);
    private readonly XLColor BottomFont = XLColor.White;
    private readonly XLColor BottomgBackground = XLColor.FromArgb(255, 91, 111);

    public ReportGenerator(List<OutputPlayer> players)
    {
        _players = players;
    }

    public void WriteReportToFile(string filename)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Players");
        worksheet.Style.Font.FontName = "Arial";

        int row = 1;

        var headers = new[]
        {
            "Игрок", 
            "Кол-во учтенных дней",
            "Всего набрано очков Активности", 
            "Средняя Активность", 
            "Среднеквадратическое отклонение Активности", 
            "Кол-во дней без Активности", 
            "Каждый день набирал норму по Активности", 
            "Персональная норма Активности", 
            "Процент выполнения нормы Активности, %", 
            "Норма Активности выполнена",
            "Всего набрано очков Титанита",
            "Среднее кол-во Титанита",
            "Среднеквадратическое отклонение Титанита",
            "Кол-во дней без Титанита",
            "Каждый день набирал норму по Титаниту",
            "Персональная норма Титанита",
            "Процент выполнения нормы Титанита, %",
            "Норма Титанита выполнена",
        };

        for (int col = 1; col <= headers.Length; col++)
        {
            var cell = worksheet.Cell(row, col);
            cell.Value = headers[col - 1];
            cell.Style.Font.Bold = true;
            cell.Style.Alignment.SetWrapText();
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            cell.Style.Fill.BackgroundColor = HeaderBackground;
        }

        row++;

        foreach (var player in _players)
        {
            worksheet.Cell(row, 1).Value = player.Name;

            worksheet.Cell(row, 2).Value = player.DaysCount;

            worksheet.Cell(row, 3).Value = Math.Round(player.ActivityTotal);

            worksheet.Cell(row, 4).Value = Math.Round(player.ActivityMean);

            worksheet.Cell(row, 5).Value = Math.Round(player.ActivityStandardDeviation);

            worksheet.Cell(row, 6).Value = player.ActivityZeroDaysCount;
            worksheet.Cell(row, 6).Style.Font.FontColor = player.ActivityZeroDaysCount == 0 ? GoodFont : BadFont;
            worksheet.Cell(row, 6).Style.Fill.BackgroundColor = player.ActivityZeroDaysCount == 0 ? GoodBackground : BadBackground;

            worksheet.Cell(row, 7).Value = player.ActivityAlwaysDailyPlan ? "Да" : "Нет";
            worksheet.Cell(row, 7).Style.Font.FontColor = player.ActivityAlwaysDailyPlan ? GoodFont : BadFont;
            worksheet.Cell(row, 7).Style.Fill.BackgroundColor = player.ActivityAlwaysDailyPlan ? GoodBackground : BadBackground;

            worksheet.Cell(row, 8).Value = Math.Round(player.ActivityOutputNorm);

            worksheet.Cell(row, 9).Value = Math.Round(player.ActivityPercentage);
            worksheet.Cell(row, 9).Style.NumberFormat.Format = "0\"%\"";
            worksheet.Cell(row, 9).Style.Font.FontColor = player.ActivityPercentage >= 100 ? GoodFont : BadFont;
            worksheet.Cell(row, 9).Style.Fill.BackgroundColor = player.ActivityPercentage >= 100 ? GoodBackground : BadBackground;
            worksheet.Cell(row, 9).Style.Font.FontColor = player.ActivityPercentage < 33 ? BottomFont : worksheet.Cell(row, 9).Style.Font.FontColor;
            worksheet.Cell(row, 9).Style.Fill.BackgroundColor = player.ActivityPercentage < 33 ? BottomgBackground : worksheet.Cell(row, 9).Style.Fill.BackgroundColor;
            worksheet.Cell(row, 9).Style.Font.FontColor = player.ActivityPercentage > 300 ? TopFont : worksheet.Cell(row, 9).Style.Font.FontColor;
            worksheet.Cell(row, 9).Style.Fill.BackgroundColor = player.ActivityPercentage > 300 ? TopBackground : worksheet.Cell(row, 9).Style.Fill.BackgroundColor;

            worksheet.Cell(row, 10).Value = player.ActivitySuccess ? "Да" : "Нет";
            worksheet.Cell(row, 10).Style.Font.FontColor = player.ActivitySuccess ? GoodFont : BadFont;
            worksheet.Cell(row, 10).Style.Fill.BackgroundColor = player.ActivitySuccess ? GoodBackground : BadBackground;

            worksheet.Cell(row, 11).Value = Math.Round(player.TitanitTotal);

            worksheet.Cell(row, 12).Value = Math.Round(player.TitanitMean);

            worksheet.Cell(row, 13).Value = Math.Round(player.TitanitStandardDeviation);

            worksheet.Cell(row, 14).Value = player.TitanitZeroDaysCount;
            worksheet.Cell(row, 14).Style.Font.FontColor = player.TitanitZeroDaysCount == 0 ? GoodFont : BadFont;
            worksheet.Cell(row, 14).Style.Fill.BackgroundColor = player.TitanitZeroDaysCount == 0 ? GoodBackground : BadBackground;

            worksheet.Cell(row, 15).Value = player.TitanitAlwaysDailyPlan ? "Да" : "Нет";
            worksheet.Cell(row, 15).Style.Font.FontColor = player.TitanitAlwaysDailyPlan ? GoodFont : BadFont;
            worksheet.Cell(row, 15).Style.Fill.BackgroundColor = player.TitanitAlwaysDailyPlan ? GoodBackground : BadBackground;

            worksheet.Cell(row, 16).Value = Math.Round(player.TitanitOutputNorm);

            worksheet.Cell(row, 17).Value = Math.Round(player.TitanitPercentage);
            worksheet.Cell(row, 17).Style.NumberFormat.Format = "0\"%\"";
            worksheet.Cell(row, 17).Style.Font.FontColor = player.TitanitPercentage >= 100 ? GoodFont : BadFont;
            worksheet.Cell(row, 17).Style.Fill.BackgroundColor = player.TitanitPercentage >= 100 ? GoodBackground : BadBackground;
            worksheet.Cell(row, 17).Style.Font.FontColor = player.TitanitPercentage < 33 ? BottomFont : worksheet.Cell(row, 17).Style.Font.FontColor;
            worksheet.Cell(row, 17).Style.Fill.BackgroundColor = player.TitanitPercentage < 33 ? BottomgBackground : worksheet.Cell(row, 17).Style.Fill.BackgroundColor;
            worksheet.Cell(row, 17).Style.Font.FontColor = player.TitanitPercentage > 300 ? TopFont : worksheet.Cell(row, 17).Style.Font.FontColor;
            worksheet.Cell(row, 17).Style.Fill.BackgroundColor = player.TitanitPercentage > 300 ? TopBackground : worksheet.Cell(row, 17).Style.Fill.BackgroundColor;

            worksheet.Cell(row, 18).Value = player.TitanitSuccess ? "Да" : "Нет";
            worksheet.Cell(row, 18).Style.Font.FontColor = player.TitanitSuccess ? GoodFont : BadFont;
            worksheet.Cell(row, 18).Style.Fill.BackgroundColor = player.TitanitSuccess ? GoodBackground : BadBackground;

            row++;
        }

        worksheet.Columns().AdjustToContents(2, worksheet.LastRowUsed()!.RowNumber(), 12, 20);
        var range = worksheet.RangeUsed();
        range!.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        foreach (var cell in range.Cells())
        {
            if (cell.DataType == XLDataType.Number && cell.Style.NumberFormat.Format != "0\"%\"")
                cell.Style.NumberFormat.Format = "#,##0";
        }

        workbook.SaveAs(filename);
    }
}
