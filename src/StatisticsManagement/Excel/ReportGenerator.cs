using ClosedXML.Excel;
using StatisticsManagement.OutputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.Excel;

internal class ReportGenerator
{
    private readonly List<OutputPlayer> _players;

    public ReportGenerator(List<OutputPlayer> players)
    {
        _players = players;
    }

    public void WriteReportToFile(string filename)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Players");

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
            "Процент выполнения нормы Активности", 
            "Норма Активности выполнена",
            "Всего набрано очков Титанита",
            "Среднее кол-во Титанита",
            "Среднеквадратическое отклонение Титанита",
            "Кол-во дней без Титанита",
            "Каждый день набирал норму по Титаниту",
            "Персональная норма Титанита",
            "Процент выполнения нормы Титанита",
            "Норма Титанита выполнена",
        };

        for (int col = 1; col <= headers.Length; col++)
        {
            worksheet.Cell(row, col).Value = headers[col - 1];
        }

        row++;

        foreach (var player in _players)
        {
            worksheet.Cell(row, 1).Value = player.Name;
            worksheet.Cell(row, 2).Value = player.DaysCount;
            worksheet.Cell(row, 3).Value = player.ActivityTotal;
            worksheet.Cell(row, 4).Value = player.ActivityMean;
            worksheet.Cell(row, 5).Value = player.ActivityStandardDeviation;
            worksheet.Cell(row, 6).Value = player.ActivityZeroDaysCount;
            worksheet.Cell(row, 7).Value = player.ActivityAlwaysDailyPlan;
            worksheet.Cell(row, 8).Value = player.ActivityOutputNorm;
            worksheet.Cell(row, 9).Value = player.ActivityPercentage;
            worksheet.Cell(row, 10).Value = player.ActivitySuccess;
            worksheet.Cell(row, 11).Value = player.TitanitTotal;
            worksheet.Cell(row, 12).Value = player.TitanitMean;
            worksheet.Cell(row, 13).Value = player.TitanitStandardDeviation;
            worksheet.Cell(row, 14).Value = player.TitanitZeroDaysCount;
            worksheet.Cell(row, 15).Value = player.TitanitAlwaysDailyPlan;
            worksheet.Cell(row, 16).Value = player.TitanitOutputNorm;
            worksheet.Cell(row, 17).Value = player.TitanitPercentage;
            worksheet.Cell(row, 18).Value = player.TitanitSuccess;

            row++;
        }

        workbook.SaveAs(filename);
    }
}
