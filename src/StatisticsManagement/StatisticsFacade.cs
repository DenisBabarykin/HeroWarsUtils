using StatisticsManagement.Converters;
using StatisticsManagement.InputModels;
using StatisticsManagement.CalculationModels;
using System;
using System.Collections.Generic;
using System.Text;
using StatisticsManagement.OutputModels;
using StatisticsManagement.Services;
using StatisticsManagement.Excel;

namespace StatisticsManagement;

public class StatisticsFacade
{
    public void Process(string activityCsv, string titanitCsv, string outputFilename, StatisticsConfig statisticsConfig)
    {
        CsvToInputTableConverter csvToInputTableConverter = new();

        InputTable activityTable = csvToInputTableConverter.Convert(activityCsv);
        InputTable titanitTable = csvToInputTableConverter.Convert(titanitCsv);

        Table table = new InputToCalculationTableConverter().Convert(activityTable, titanitTable);

        List<OutputPlayer> outputPlayers = new StatisticsService().CalcStatistics(table.Players, statisticsConfig);

        new ReportGenerator(outputPlayers).WriteReportToFile(outputFilename);
    }

}
