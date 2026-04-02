using StatisticsManagement.CalculationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.Services;

internal class PlayerStatisticsService
{
    private readonly Player _player;
    private readonly StatisticsConfig _statisticsConfig;

    public PlayerStatisticsService(Player player, StatisticsConfig statisticsConfig)
    {
        _player = player;
        _statisticsConfig = statisticsConfig;
    }

    public int CalcDaysCount()
    {
        return _player.Days.Count(d => !d.IsEmpty);
    }

    public double CalcActivityTotal()
    {
        return _player.Days.Where(d => !d.IsEmpty).Sum(d => d.ActivityPoints);
    }

    public double CalcActivityMean()
    {
        return CalcActivityTotal() / CalcDaysCount();
    }

    public double CalcActivityStandardDeviation()
    {
        double mean = CalcActivityMean();
        double[] points = _player.Days
            .Where(d => !d.IsEmpty)
            .Select(d => d.ActivityPoints)
            .ToArray();

        double dispersion = points.Average(point => Math.Pow(point - mean, 2));
        return Math.Sqrt(dispersion);
    }

    public int CalcActivityZeroDaysCount()
    {
        return _player.Days.Where(d => !d.IsEmpty)
            .Select(d => d.ActivityPoints)
            .Count(p => p < 0.5);
    }

    public bool CalcActivityAlwaysDailyPlan()
    {
        return _player.Days.Where(d => !d.IsEmpty)
            .Select(d => d.ActivityPoints)
            .All(p => p >= _statisticsConfig.DailyActivity);
    }

    public double CalcActivityOutputNorm()
    {
        return CalcActivityAlwaysDailyPlan() 
            ? _statisticsConfig.DailyActivity * CalcDaysCount()
            : _statisticsConfig.WeeklyActivity * CalcDaysCount() / 7.0;
    }

    public double CalcActivityPercentage()
    {
        return CalcActivityTotal() / CalcActivityOutputNorm() * 100.0;
    }

    public bool CalcActivitySuccess()
    {
        return CalcActivityPercentage() >= 100;
    }

    public double CalcTitanitTotal()
    {
        return _player.Days.Where(d => !d.IsEmpty).Sum(d => d.TitanitPoints);
    }

    public double CalcTitanitMean()
    {
        return CalcTitanitTotal() / CalcDaysCount();
    }

    public double CalcTitanitStandardDeviation()
    {
        double mean = CalcTitanitMean();
        double[] points = _player.Days
            .Where(d => !d.IsEmpty)
            .Select(d => d.TitanitPoints)
            .ToArray();

        double dispersion = points.Average(point => Math.Pow(point - mean, 2));
        return Math.Sqrt(dispersion);
    }

    public int CalcTitanitZeroDaysCount()
    {
        return _player.Days.Where(d => !d.IsEmpty)
            .Select(d => d.TitanitPoints)
            .Count(p => p < 0.5);
    }

    public bool CalcTitanitAlwaysDailyPlan()
    {
        return _player.Days.Where(d => !d.IsEmpty)
            .Select(d => d.TitanitPoints)
            .All(p => p >= _statisticsConfig.DailyTitanit);
    }

    public double CalcTitanitOutputNorm()
    {
        return CalcTitanitAlwaysDailyPlan()
            ? _statisticsConfig.DailyTitanit * CalcDaysCount()
            : _statisticsConfig.WeeklyTitanit * CalcDaysCount() / 7.0;
    }

    public double CalcTitanitPercentage()
    {
        return CalcTitanitTotal() / CalcTitanitOutputNorm() * 100.0;
    }

    public bool CalcTitanitSuccess()
    {
        return CalcTitanitPercentage() >= 100;
    }
}
