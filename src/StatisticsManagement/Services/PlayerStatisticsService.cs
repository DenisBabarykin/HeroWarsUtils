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
        return _statisticsConfig.WeeklyActivity / 7.0 * CalcDaysCount();
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
        throw new NotImplementedException();
    }

    public double CalcTitanitMean()
    {
        throw new NotImplementedException();
    }

    public double CalcTitanitStandardDeviation()
    {
        throw new NotImplementedException();
    }

    public int CalcTitanitZeroDaysCount()
    {
        throw new NotImplementedException();
    }

    public bool CalcTitanitAlwaysDailyPlan()
    {
        throw new NotImplementedException();
    }

    public double CalcTitanitOutputNorm()
    {
        throw new NotImplementedException();
    }

    public double CalcTitanitPercentage()
    {
        throw new NotImplementedException();
    }

    public bool CalcTitanitSuccess()
    {
        throw new NotImplementedException();
    }
}
