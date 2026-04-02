using StatisticsManagement.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.CalculationModels;

internal class Day
{
    public DayOfWeekEnum DayOfWeek { get; }

    public bool IsEmpty { get; }

    public double ActivityPoints => IsEmpty ? throw new InvalidOperationException("Нельзя запрашивать очки у пустого дня") : _activityPoints;
    private readonly double _activityPoints;

    public double TitanitPoints => IsEmpty ? throw new InvalidOperationException("Нельзя запрашивать очки у пустого дня") : _titanitPoints;
    private readonly double _titanitPoints;

    public Day(DayOfWeekEnum dayOfWeek, double activityPoints, double titanitPoints)
    {
        if (activityPoints < 0)
            throw new ArgumentOutOfRangeException(nameof(activityPoints), "Очки не могут быть отрицательными");

        if (titanitPoints < 0)
            throw new ArgumentOutOfRangeException(nameof(titanitPoints), "Очки не могут быть отрицательными");

        DayOfWeek = dayOfWeek;
        _activityPoints = activityPoints;
        _titanitPoints = titanitPoints;
        IsEmpty = false;
    }

    private Day(DayOfWeekEnum dayOfWeek, double activityPoints, double titanitPoints, bool isEmpty)
    {
        DayOfWeek = dayOfWeek;
        _activityPoints = activityPoints;
        _titanitPoints = titanitPoints;
        IsEmpty = isEmpty;
    }

    public static Day CreateEmptyDay(DayOfWeekEnum dayOfWeek)
    {
        return new Day(dayOfWeek, 0, 0, true);
    }
}
