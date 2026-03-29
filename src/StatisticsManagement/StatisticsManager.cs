using StatisticsManagement.Converters;
using StatisticsManagement.InputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement;

public class StatisticsManager
{
    public void Process(string activityCsv, string titanitCsv, string outputFilename, StatisticsConfig statisticsConfig)
    {
        CsvToInputTableConverter csvToInputTableConverter = new();

        InputTable activityTable = csvToInputTableConverter.Convert(activityCsv);
        InputTable titanitTable = csvToInputTableConverter.Convert(titanitCsv); 


    }
}
