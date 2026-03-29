using StatisticsManagement.CalculationModels;
using StatisticsManagement.InputModels;
using StatisticsManagement.StringComparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.Converters;

internal class InputToCalculationTableConverter
{
    private readonly IStringDistanceComparator _stringDistanceComparator = new SoftWxDamerauComparator();

    /// <summary>
    /// Merges two tables into one.
    /// </summary>
    public Table Convert(InputTable activityTable, InputTable titanitTable)
    {
        if (activityTable.InputPlayers.Count != titanitTable.InputPlayers.Count)
            throw new ArgumentException(nameof(titanitTable), "Количество игроков в таблицах активности и титанита не совпадают.");

        int playersCount = activityTable.InputPlayers.Count;
        List<Player> players = new List<Player>(playersCount);

        HashSet<InputPlayer> selectedTitanitPlayers = new();
                
        foreach (var inputPlayer in activityTable.InputPlayers)
        {
            var closestPlayer = titanitTable.InputPlayers.MinBy(p => _stringDistanceComparator.CalcDistance(inputPlayer.Name, p.Name));
            if (closestPlayer is null)
                throw new ArgumentNullException(nameof(closestPlayer));

            if (!selectedTitanitPlayers.Add(closestPlayer))
                throw new ArgumentException(nameof(titanitTable), $"Игрок '{inputPlayer.Name}' был смаплен с уже смапленным ранее игроком '${closestPlayer.Name}'.");

            if (inputPlayer.InputDays.Count != closestPlayer.InputDays.Count)
                throw new ArgumentException(nameof(titanitTable), "Количество дней у игрока в таблицах не совпало.");

            List<Day> days = inputPlayer.InputDays
                .Zip(closestPlayer.InputDays, (d1, d2) => 
                    (d1.Points >= 0 && d2.Points >= 0) ? (new Day(d1.DayOfWeek, d1.Points, d2.Points)) : (Day.CreateEmptyDay(d1.DayOfWeek)))
                .ToList();

            players.Add(new Player(inputPlayer.Name, days));
        }

        return new Table(players);
    }
}
