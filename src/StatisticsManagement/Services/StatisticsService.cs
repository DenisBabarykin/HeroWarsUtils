using StatisticsManagement.CalculationModels;
using StatisticsManagement.Converters;
using StatisticsManagement.OutputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.Services;

internal class StatisticsService
{
    public List<OutputPlayer> CalcStatistics(List<Player> players, StatisticsConfig _statisticsConfig)
    {
        List<OutputPlayer> result = players
            .Select(p => new PlayerToOutputPlayerConverter(_statisticsConfig).Convert(p))
            .ToList();

        return Sort(result);
    }

    private static List<OutputPlayer> Sort(List<OutputPlayer> players)
    {
        List<OutputPlayer> result = [];
        List<OutputPlayer> uncountedPlayers = players.Select(p => p).ToList();

        List<OutputPlayer> top1 = uncountedPlayers
            .Where(p => p.ActivitySuccess 
                && p.ActivityAlwaysDailyPlan 
                && p.TitanitSuccess 
                && p.TitanitAlwaysDailyPlan)
            .OrderByDescending(p => p.ActivityPercentage)
            .ToList();
        uncountedPlayers = uncountedPlayers.Except(top1).ToList();
        result.AddRange(top1);

        List<OutputPlayer> top2 = uncountedPlayers
            .Where(p => p.ActivitySuccess
                && p.TitanitSuccess)
            .OrderByDescending(p => p.ActivityPercentage)
            .ToList();
        uncountedPlayers = uncountedPlayers.Except(top2).ToList();
        result.AddRange(top2);

        List<OutputPlayer> middle1 = uncountedPlayers
            .Where(p => p.ActivityPercentage > 75 
                && p.TitanitPercentage > 75)
            .OrderByDescending(p => p.ActivityPercentage)
            .ToList();
        uncountedPlayers = uncountedPlayers.Except(middle1).ToList();
        result.AddRange(middle1);

        List<OutputPlayer> middle2 = uncountedPlayers
            .Where(p => p.ActivityPercentage > 50
                && p.TitanitPercentage > 50)
            .OrderByDescending(p => p.ActivityPercentage)
            .ToList();
        uncountedPlayers = uncountedPlayers.Except(middle2).ToList();
        result.AddRange(middle2);

        List<OutputPlayer> bottom1 = uncountedPlayers
            .Where(p => p.ActivityPercentage > 0
                && p.TitanitPercentage > 0)
            .OrderByDescending(p => p.ActivityPercentage)
            .ToList();
        uncountedPlayers = uncountedPlayers.Except(bottom1).ToList();
        result.AddRange(bottom1);

        List<OutputPlayer> bottom2 = uncountedPlayers
            .Where(p => p.ActivityPercentage > 0
                && p.TitanitPercentage < 1.0)
            .OrderByDescending(p => p.ActivityPercentage)
            .ToList();
        uncountedPlayers = uncountedPlayers.Except(bottom2).ToList();
        result.AddRange(bottom2);

        uncountedPlayers = uncountedPlayers.OrderByDescending(p => p.TitanitPercentage).ToList();
        result.AddRange(uncountedPlayers);

        if (result.Count != players.Count)
            throw new InvalidOperationException("Ошибка в алгоритме сортировки игроков.");

        return result;
    }
}
