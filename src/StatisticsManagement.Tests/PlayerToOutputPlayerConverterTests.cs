using StatisticsManagement.CalculationModels;
using StatisticsManagement.Converters;
using StatisticsManagement.OutputModels;
using Xunit;

namespace StatisticsManagement.Tests;

public class PlayerToOutputPlayerConverterTests
{
    private readonly StatisticsConfig _config = new(5000, 35000, 1000, 7000);

    [Fact]
    public void Convert_ValidPlayer_ReturnsOutputPlayerWithCorrectName()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TestPlayer", result.Name);
    }

    [Fact]
    public void Convert_ValidPlayer_ReturnsCorrectDaysCount()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(3, result.DaysCount);
    }

    [Fact]
    public void Convert_ValidPlayer_ReturnsCorrectActivityTotal()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 6000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 4000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(15000, result.ActivityTotal);
    }

    [Fact]
    public void Convert_ValidPlayer_ReturnsCorrectTitanitTotal()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 2000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 3000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(6000, result.TitanitTotal);
    }

    [Fact]
    public void Convert_ValidPlayer_ReturnsCorrectActivityMean()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 3000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 7000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(5000, result.ActivityMean);
    }

    [Fact]
    public void Convert_ValidPlayer_ReturnsCorrectTitanitMean()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 2000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 3000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(2000, result.TitanitMean);
    }

    [Fact]
    public void Convert_PlayerWithZeroActivityDays_ReturnsCorrectZeroDaysCount()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 0, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 0, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(2, result.ActivityZeroDaysCount);
    }

    [Fact]
    public void Convert_PlayerWithZeroTitanitDays_ReturnsCorrectZeroDaysCount()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 0),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 0)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(2, result.TitanitZeroDaysCount);
    }

    [Fact]
    public void Convert_PlayerAlwaysMeetsDailyPlan_ReturnsActivityAlwaysDailyPlanTrue()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 6000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 7000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.True(result.ActivityAlwaysDailyPlan);
    }

    [Fact]
    public void Convert_PlayerDoesNotMeetDailyPlan_ReturnsActivityAlwaysDailyPlanFalse()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 4000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 6000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 7000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.False(result.ActivityAlwaysDailyPlan);
    }

    [Fact]
    public void Convert_PlayerAlwaysMeetsDailyTitanitPlan_ReturnsTitanitAlwaysDailyPlanTrue()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 2000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 3000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.True(result.TitanitAlwaysDailyPlan);
    }

    [Fact]
    public void Convert_PlayerDoesNotMeetDailyTitanitPlan_ReturnsTitanitAlwaysDailyPlanFalse()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 500),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 2000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 3000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.False(result.TitanitAlwaysDailyPlan);
    }

    [Fact]
    public void Convert_PlayerWithSuccessfulActivity_ReturnsActivitySuccessTrue()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.True(result.ActivitySuccess);
    }

    [Fact]
    public void Convert_PlayerWithUnsuccessfulActivity_ReturnsActivitySuccessFalse()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 4000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 4000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 4000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.False(result.ActivitySuccess);
    }

    [Fact]
    public void Convert_PlayerWithSuccessfulTitanit_ReturnsTitanitSuccessTrue()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 2000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 3000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.True(result.TitanitSuccess);
    }

    [Fact]
    public void Convert_PlayerWithUnsuccessfulTitanit_ReturnsTitanitSuccessFalse()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 500),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 500),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 500)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.False(result.TitanitSuccess);
    }

    [Fact]
    public void Convert_ValidPlayer_ReturnsCorrectActivityStandardDeviation()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 4000, 1000),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 6000, 1000)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(816.496580927726, result.ActivityStandardDeviation, precision: 2);
    }

    [Fact]
    public void Convert_ValidPlayer_ReturnsCorrectTitanitStandardDeviation()
    {
        // Arrange
        List<Day> days =
        [
            new(Common.DayOfWeekEnum.Monday, 5000, 800),
            new(Common.DayOfWeekEnum.Tuesday, 5000, 1000),
            new(Common.DayOfWeekEnum.Wednesday, 5000, 1200)
        ];
        Player player = new("TestPlayer", days);

        // Act
        PlayerToOutputPlayerConverter converter = new(_config);
        OutputPlayer result = converter.Convert(player);

        // Assert
        Assert.Equal(163.2993161855452, result.TitanitStandardDeviation, precision: 2);
    }
}
