using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.CalculationModels;

internal class Table
{
    public List<Player> Players { get; }

    public Table(List<Player> players)
    {
        if (players.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(players), "Коллекция игроков не может быть пустой");

        Players = players;
    }
}
