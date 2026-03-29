using StatisticsManagement.Common;
using StatisticsManagement.InputModels;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StatisticsManagement.Converters;

internal class CsvToInputTableConverter
{
    private readonly DayOfWeekConverter _dayOfWeekConverter = new();

    public InputTable Convert(string csv)
    {
        char[] lineBreakChars = { '\r', '\n', '\u2028', '\u2029' };
        string[] lines = csv.Split(lineBreakChars, StringSplitOptions.RemoveEmptyEntries);
        string[] headers = lines[0].Split(',');
        List<InputPlayer> list = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            string[] parts = line.Split(',');
            InputPlayer inputPlayer = new();
            inputPlayer.Name = parts[0];
            List<InputDay> inputDays = new();

            for (int j = 1; j < parts.Length && j < headers.Length; j++)
            {
                string str = parts[j];
                InputDay inputDay = new(_dayOfWeekConverter.ConvertToEnum(headers[j]), double.Parse(str));
                inputDays.Add(inputDay);
            }

            if (inputDays.Count != 7)
            {
                throw new FormatException($"Player '{parts[0]}' has {inputDays.Count} days, expected 7");
            }

            inputPlayer.InputDays = inputDays;
            list.Add(inputPlayer);
        }

        InputTable result = new();
        result.InputPlayers = list;

        return result;
    }
}
