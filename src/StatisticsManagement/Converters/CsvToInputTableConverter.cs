using StatisticsManagement.InputModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace StatisticsManagement.Converters;

internal class CsvToInputTableConverter
{
    public InputTable Convert(string csv)
    {
        var table = new InputTable();
        var lines = csv.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        // Пропускаем заголовок (первая строка)
        foreach (var line in lines.Skip(1))
        {
            var columns = line.Split(',');

            var player = new InputPlayer
            {
                Name = columns[0],
                Points = columns
                    .Skip(1)
                    .Select(c => double.Parse(c.Trim(), CultureInfo.InvariantCulture))
                    .Where(p => p != -1)
                    .ToList()
            };

            table.InputPlayers.Add(player);
        }

        return table;
    }
}
