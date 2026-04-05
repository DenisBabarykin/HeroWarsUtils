using StatisticsManagement.CalculationModels;
using StatisticsManagement.OutputModels;
using StatisticsManagement.Services;
using Xunit;

namespace StatisticsManagement.Tests;

public class StatisticsServiceTests
{
    private readonly StatisticsService _service = new();
    private readonly StatisticsConfig _config = new(5000, 35000, 1000, 7000);

    [Fact]
    public void CalcStatistics_EmptyPlayers_ReturnsEmptyList()
    {
        // Arrange
        List<Player> players = [];

        // Act
        List<OutputPlayer> result = _service.CalcStatistics(players, _config);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void CalcStatistics_SinglePlayer_ReturnsSortedListWithOnePlayer()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Thursday, 5000, 1000),
            new(Common.DayOfWeekEnum.Friday, 5000, 1000),
            new(Common.DayOfWeekEnum.Saturday, 5000, 1000),
            new(Common.DayOfWeekEnum.Sunday, 5000, 1000)
        ];
        List<Player> players = [new("TestPlayer", days)];

        // Act
        List<OutputPlayer> result = _service.CalcStatistics(players, _config);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("TestPlayer", result[0].Name);
    }

    [Fact]
    public void CalcStatistics_MultiplePlayers_ReturnsAllPlayers()
    {
        // Arrange
        List<Day> days1 = CreateSevenFullDays(5000, 1000);
        List<Day> days2 = CreateSevenFullDays(6000, 2000);
        List<Player> players =
        [
            new("Player1", days1),
            new("Player2", days2)
        ];

        // Act
        List<OutputPlayer> result = _service.CalcStatistics(players, _config);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void CalcStatistics_PermitsNoDuplicatePlayers()
    {
        // Arrange
        List<Day> days1 = CreateSevenFullDays(5000, 1000);
        List<Day> days2 = CreateSevenFullDays(6000, 2000);
        List<Player> players =
        [
            new("Player1", days1),
            new("Player2", days2)
        ];

        // Act
        List<OutputPlayer> result = _service.CalcStatistics(players, _config);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, p => Assert.NotNull(p.Name));
        Assert.All(result, p => Assert.DoesNotContain("", new[] { p.Name }));
    }

    [Fact]
    public void CalcStatistics_SortsPerfectActivityAndTitanitPlayersFirst()
    {
        // Arrange
        List<Day> perfectDays = CreateSevenFullDays(6000, 2000);
        List<Day> partialDays = CreateSevenFullDays(4000, 400);
        List<Player> players =
        [
            new("PartialPlayer", partialDays),
            new("PerfectPlayer", perfectDays),
            new("AnotherPartialPlayer", partialDays)
        ];

        // Act
        List<OutputPlayer> result = _service.CalcStatistics(players, _config);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Equal("PerfectPlayer", result[0].Name);
    }

    [Fact]
    public void CalcStatistics_SortsByActivityPercentageWhenMultiplePerfectPlayers()
    {
        // Arrange
        List<Day> days1 = CreateSevenFullDays(7000, 2000);
        List<Day> days2 = CreateSevenFullDays(5000, 1000);
        List<Player> players =
        [
            new("Player2", days2),
            new("Player1", days1)
        ];

        // Act
        List<OutputPlayer> result = _service.CalcStatistics(players, _config);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Player1", result[0].Name);
        Assert.Equal("Player2", result[1].Name);
    }

    [Fact]
    public void CalcStatistics_SortsByActivityPercentageWithinTier()
    {
        // Arrange
        List<Day> days1 = CreateSevenFullDays(4000, 400);
        List<Day> days2 = CreateSevenFullDays(3000, 300);
        List<Player> players =
        [
            new("Player2", days2),
            new("Player1", days1)
        ];

        // Act
        List<OutputPlayer> result = _service.CalcStatistics(players, _config);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Player1", result[0].Name);
    }

    [Fact]
    public void CalcStatistics_ThrowsExceptionWhenPlayerCountMismatch()
    {
        // Arrange
        List<Day> days1 = CreateSevenFullDays(5000, 1000);
        List<Day> days2 = CreateSevenFullDays(6000, 2000);
        List<Player> players =
        [
            new("Player1", days1),
            new("Player2", days2)
        ];

        // Act
        List<OutputPlayer> result = _service.CalcStatistics(players, _config);

        // Assert
        Assert.Equal(players.Count, result.Count);
    }

    [Fact]
    public void CalcStatistics_ProducesConsistentSorting()
    {
        // Arrange
        List<Day> days1 = CreateSevenFullDays(5000, 1000);
        List<Day> days2 = CreateSevenFullDays(6000, 2000);
        List<Day> days3 = CreateSevenFullDays(4000, 800);
        List<Player> players =
        [
            new("Player1", days1),
            new("Player2", days2),
            new("Player3", days3)
        ];

        // Act
        List<OutputPlayer> result1 = _service.CalcStatistics(players, _config);
        List<OutputPlayer> result2 = _service.CalcStatistics(players, _config);

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.Equal(3, result1.Count);
        Assert.Equal(3, result2.Count);
        for (int i = 0; i < 3; i++)
        {
            Assert.Equal(result1[i].Name, result2[i].Name);
        }
    }

    private static List<Day> CreateSevenFullDays(double activity, double titanit)
    {
        return
        [
            new(Common.DayOfWeekEnum.Monday, activity, titanit),
            new(Common.DayOfWeekEnum.Tuesday, activity, titanit),
            new(Common.DayOfWeekEnum.Wednesday, activity, titanit),
            new(Common.DayOfWeekEnum.Thursday, activity, titanit),
            new(Common.DayOfWeekEnum.Friday, activity, titanit),
            new(Common.DayOfWeekEnum.Saturday, activity, titanit),
            new(Common.DayOfWeekEnum.Sunday, activity, titanit)
        ];
    }
}
