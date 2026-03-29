using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.CalculationModels;

internal class Player
{
    public string Name { get; }

    public List<Day> Days { get; }

    public Player(string name, List<Day> days)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(nameof(name), "Имя не может быть пустым");

        if (days.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(days), "Коллекция дней не может быть пустой.");

        Name = name;
        Days = days;
    }
}
