using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement;

public class StatisticsConfig
{
    public double DailyActivity { get; }
    public double WeeklyActivity { get; }
    public double DailyTitanit { get; }
    public double WeeklyTitanit { get; }

    public StatisticsConfig(double dailyActivity, double weeklyActivity, double dailyTitanit, double weeklyTitanit)
    {
        DailyActivity = dailyActivity;
        WeeklyActivity = weeklyActivity;
        DailyTitanit = dailyTitanit;
        WeeklyTitanit = weeklyTitanit;
    }
}
