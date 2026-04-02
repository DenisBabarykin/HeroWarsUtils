using StatisticsManagement.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.Converters;

internal class DayOfWeekConverter
{
    private static readonly Dictionary<string, DayOfWeekEnum> _russianToEnum = new(StringComparer.OrdinalIgnoreCase)
    {
        ["пн"] = DayOfWeekEnum.Monday,
        ["вт"] = DayOfWeekEnum.Tuesday,
        ["ср"] = DayOfWeekEnum.Wednesday,
        ["чт"] = DayOfWeekEnum.Thursday,
        ["пт"] = DayOfWeekEnum.Friday,
        ["сб"] = DayOfWeekEnum.Saturday,
        ["вс"] = DayOfWeekEnum.Sunday
    };

    private static readonly Dictionary<DayOfWeekEnum, string> _enumToRussian = new()
    {
        [DayOfWeekEnum.Monday] = "ПН",
        [DayOfWeekEnum.Tuesday] = "ВТ",
        [DayOfWeekEnum.Wednesday] = "СР",
        [DayOfWeekEnum.Thursday] = "ЧТ",
        [DayOfWeekEnum.Friday] = "ПТ",
        [DayOfWeekEnum.Saturday] = "СБ",
        [DayOfWeekEnum.Sunday] = "ВС"
    };

    public DayOfWeekEnum ConvertToEnum(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new ArgumentException("Input string cannot be empty", nameof(str));
        }

        if (!_russianToEnum.TryGetValue(str, out DayOfWeekEnum result))
        {
            throw new ArgumentException($"Invalid day of week string: {str}");
        }

        return result;
    }

    public string ConvertToString(DayOfWeekEnum value)
    {
        return _enumToRussian[value];
    }
}
