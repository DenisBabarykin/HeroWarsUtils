using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.OutputModels;

internal class OutputPlayer
{
    public string Name { get; }
    public int DaysCount { get; }

    public double ActivityTotal { get; }
    public double ActivityMean { get; }
    public double ActivityStandardDeviation { get; }
    public int ActivityZeroDaysCount { get; }
    public bool ActivityAlwaysDailyPlan { get; }
    public double ActivityOutputNorm { get; }
    public double ActivityPercentage { get; } 
    public bool ActivitySuccess { get; }

    public double TitanitTotal { get; }
    public double TitanitMean { get; }
    public double TitanitStandardDeviation { get; }
    public int TitanitZeroDaysCount { get; }
    public bool TitanitAlwaysDailyPlan { get; }
    public double TitanitOutputNorm { get; }
    public double TitanitPercentage { get; }
    public bool TitanitSuccess { get; }

    public OutputPlayer(string name,
        int daysCount,
        double activityTotal,
        double activityMean,
        double activityStandardDeviation,
        int activityZeroDaysCount,
        bool activityAlwaysDailyPlan,
        double activityOutputNorm,
        double activityPercentage,
        bool activitySuccess,
        double titanitTotal,
        double titanitMean,
        double titanitStandardDeviation,
        int titanitZeroDaysCount,
        bool titanitAlwaysDailyPlan,
        double titanitOutputNorm,
        double titanitPercentage,
        bool titanitSuccess)
    {
        Name = name;
        DaysCount = daysCount;
        ActivityTotal = activityTotal;
        ActivityMean = activityMean;
        ActivityStandardDeviation = activityStandardDeviation;
        ActivityZeroDaysCount = activityZeroDaysCount;
        ActivityAlwaysDailyPlan = activityAlwaysDailyPlan;
        ActivityOutputNorm = activityOutputNorm;
        ActivityPercentage = activityPercentage;
        ActivitySuccess = activitySuccess;
        TitanitTotal = titanitTotal;
        TitanitMean = titanitMean;
        TitanitStandardDeviation = titanitStandardDeviation;
        TitanitZeroDaysCount = titanitZeroDaysCount;
        TitanitAlwaysDailyPlan = titanitAlwaysDailyPlan;
        TitanitOutputNorm = titanitOutputNorm;
        TitanitPercentage = titanitPercentage;
        TitanitSuccess = titanitSuccess;
    }
}
