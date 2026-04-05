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

    }
}
