using StatisticsManagement.CalculationModels;
using StatisticsManagement.OutputModels;
using StatisticsManagement.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsManagement.Converters;

internal class PlayerToOutputPlayerConverter
{
    private readonly StatisticsConfig _statisticsConfig;

    public PlayerToOutputPlayerConverter(StatisticsConfig statisticsConfig)
    {
        _statisticsConfig = statisticsConfig;
    }

    public OutputPlayer Convert(Player player)
    {
        PlayerStatisticsService playerStatisticsService = new(player, _statisticsConfig);

        OutputPlayer result = new(
            player.Name,
            playerStatisticsService.CalcDaysCount(),
            playerStatisticsService.CalcActivityTotal(),
            playerStatisticsService.CalcActivityMean(),
            playerStatisticsService.CalcActivityStandardDeviation(),
            playerStatisticsService.CalcActivityZeroDaysCount(),
            playerStatisticsService.CalcActivityAlwaysDailyPlan(),
            playerStatisticsService.CalcActivityOutputNorm(),
            playerStatisticsService.CalcActivityPercentage(),
            playerStatisticsService.CalcActivitySuccess(),
            playerStatisticsService.CalcTitanitTotal(),
            playerStatisticsService.CalcTitanitMean(),
            playerStatisticsService.CalcTitanitStandardDeviation(),
            playerStatisticsService.CalcTitanitZeroDaysCount(),
            playerStatisticsService.CalcTitanitAlwaysDailyPlan(),
            playerStatisticsService.CalcTitanitOutputNorm(),
            playerStatisticsService.CalcTitanitPercentage(),
            playerStatisticsService.CalcTitanitSuccess()
        );

        return result;
    }
}
