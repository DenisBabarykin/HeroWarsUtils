using StatisticsManagement.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.InputModels;

internal struct InputDay
{
    public DayOfWeekEnum DayOfWeek;
    public double Points;

    public InputDay(DayOfWeekEnum dayOfWeek, double points)
    {
        DayOfWeek = dayOfWeek;
        Points = points;
    }
}
