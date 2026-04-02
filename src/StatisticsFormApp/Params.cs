using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsFormApp;

internal static class Params
{
    public static double DailyActivityDefault { get; } = 1000;
    private static double WeeklyActivityDefault { get; } = 10000;
    private static double DailyTitanitDefault { get; } = 75;
    private static double WeeklyTitanitDefault { get; } = 750;

    public static double DailyActivity { get; set; } = DailyActivityDefault;
    public static double WeeklyActivity { get; set; } = WeeklyActivityDefault;
    public static double DailyTitanit { get; set; } = DailyTitanitDefault;
    public static double WeeklyTitanit { get; set; } = WeeklyTitanitDefault;

    public static void Reset()
    {
        DailyActivity = DailyActivityDefault;
        WeeklyActivity = WeeklyActivityDefault;
        DailyTitanit = DailyTitanitDefault;
        WeeklyTitanit = WeeklyTitanitDefault;
    }
}
