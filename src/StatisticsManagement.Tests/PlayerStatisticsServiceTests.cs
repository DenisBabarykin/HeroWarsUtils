using StatisticsManagement.CalculationModels;
using StatisticsManagement.Common;
using StatisticsManagement.Services;
using Xunit;

namespace StatisticsManagement.Tests;

public class PlayerStatisticsServiceTests
{
    private readonly StatisticsConfig _config = new(1000, 7000, 100, 700);

    private Day CreateDay(DayOfWeekEnum day, double activity, double titanit)
    {
        return new Day(day, activity, titanit);
    }

    private Day CreateEmptyDay(DayOfWeekEnum day)
    {
        return Day.CreateEmptyDay(day);
    }

    private Player CreatePlayer(string name, List<Day> days)
    {
        return new Player(name, days);
    }

    private PlayerStatisticsService CreateService(Player player)
    {
        return new PlayerStatisticsService(player, _config);
    }

    [Fact]
    public void CalcDaysCount_AllDaysFilled_ReturnsSeven()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Thursday, 1000, 100),
            CreateDay(DayOfWeekEnum.Friday, 1000, 100),
            CreateDay(DayOfWeekEnum.Saturday, 1000, 100),
            CreateDay(DayOfWeekEnum.Sunday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcDaysCount();

        // Assert
        Assert.Equal(7, result);
    }

    [Fact]
    public void CalcDaysCount_WithEmptyDays_ExcludesEmptyDays()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateEmptyDay(DayOfWeekEnum.Tuesday),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100),
            CreateEmptyDay(DayOfWeekEnum.Thursday),
            CreateDay(DayOfWeekEnum.Friday, 1000, 100),
            CreateDay(DayOfWeekEnum.Saturday, 1000, 100),
            CreateDay(DayOfWeekEnum.Sunday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcDaysCount();

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void CalcDaysCount_MinimumNonEmptyDays_ReturnsOne()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateEmptyDay(DayOfWeekEnum.Tuesday),
            CreateEmptyDay(DayOfWeekEnum.Wednesday),
            CreateEmptyDay(DayOfWeekEnum.Thursday),
            CreateEmptyDay(DayOfWeekEnum.Friday),
            CreateEmptyDay(DayOfWeekEnum.Saturday),
            CreateEmptyDay(DayOfWeekEnum.Sunday)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcDaysCount();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcActivityTotal_AllDaysFilled_ReturnsSum()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 2000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 3000, 100),
            CreateDay(DayOfWeekEnum.Thursday, 4000, 100),
            CreateDay(DayOfWeekEnum.Friday, 5000, 100),
            CreateDay(DayOfWeekEnum.Saturday, 6000, 100),
            CreateDay(DayOfWeekEnum.Sunday, 7000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityTotal();

        // Assert
        Assert.Equal(28000, result);
    }

    [Fact]
    public void CalcActivityTotal_WithEmptyDays_ExcludesEmptyDays()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateEmptyDay(DayOfWeekEnum.Tuesday),
            CreateDay(DayOfWeekEnum.Wednesday, 2000, 100),
            CreateEmptyDay(DayOfWeekEnum.Thursday),
            CreateDay(DayOfWeekEnum.Friday, 3000, 100),
            CreateDay(DayOfWeekEnum.Saturday, 4000, 100),
            CreateDay(DayOfWeekEnum.Sunday, 5000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityTotal();

        // Assert
        Assert.Equal(15000, result);
    }

    [Fact]
    public void CalcActivityTotal_ZeroActivityPoints_ReturnsSumExcludingZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 0, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 2000, 100),
            CreateDay(DayOfWeekEnum.Thursday, 0, 100),
            CreateDay(DayOfWeekEnum.Friday, 1000, 100),
            CreateDay(DayOfWeekEnum.Saturday, 0, 100),
            CreateDay(DayOfWeekEnum.Sunday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityTotal();

        // Assert
        Assert.Equal(5000, result);
    }

    [Fact]
    public void CalcActivityMean_AllDaysEqual_ReturnsExpectedMean()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Thursday, 1000, 100),
            CreateDay(DayOfWeekEnum.Friday, 1000, 100),
            CreateDay(DayOfWeekEnum.Saturday, 1000, 100),
            CreateDay(DayOfWeekEnum.Sunday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityMean();

        // Assert
        Assert.Equal(1000, result);
    }

    [Fact]
    public void CalcActivityMean_VaryingActivity_ReturnsCorrectMean()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 2000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 3000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityMean();

        // Assert
        Assert.Equal(2000, result);
    }

    [Fact]
    public void CalcActivityStandardDeviation_AllDaysSame_ReturnsZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityStandardDeviation();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalcActivityStandardDeviation_TwoValues_ReturnsCorrectDeviation()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 0, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 10, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityStandardDeviation();

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void CalcActivityStandardDeviation_ThreeValues_ReturnsCorrectDeviation()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 2, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 8, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 11, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityStandardDeviation();

        // Assert
        double expected = Math.Sqrt((Math.Pow(2 - 7, 2) + Math.Pow(8 - 7, 2) + Math.Pow(11 - 7, 2)) / 3.0);
        Assert.Equal(expected, result, 10);
    }

    [Fact]
    public void CalcActivityStandardDeviation_WithEmptyDays_ExcludesEmptyDays()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 0, 100),
            CreateEmptyDay(DayOfWeekEnum.Tuesday),
            CreateDay(DayOfWeekEnum.Wednesday, 10, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityStandardDeviation();

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void CalcActivityZeroDaysCount_NoZeroDays_ReturnsZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 500, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcActivityZeroDaysCount();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalcActivityZeroDaysCount_OneZeroDay_ReturnsOne()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 0, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcActivityZeroDaysCount();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcActivityZeroDaysCount_FractionalLessThanZeroPointFive_CountsAsZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 0.4, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcActivityZeroDaysCount();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcActivityZeroDaysCount_FractionalAboveZeroPointFive_DoesNotCountAsZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 0.5, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcActivityZeroDaysCount();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalcActivityZeroDaysCount_WithEmptyDays_ExcludesEmptyDays()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 0, 100),
            CreateEmptyDay(DayOfWeekEnum.Tuesday),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcActivityZeroDaysCount();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcActivityAlwaysDailyPlan_AllDaysMeetRequirement_ReturnsTrue()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcActivityAlwaysDailyPlan();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcActivityAlwaysDailyPlan_OneDayBelowRequirement_ReturnsFalse()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 500, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcActivityAlwaysDailyPlan();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalcActivityAlwaysDailyPlan_ExactlyAtRequirement_ReturnsTrue()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcActivityAlwaysDailyPlan();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcActivityAlwaysDailyPlan_JustBelowRequirement_ReturnsFalse()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 999.99, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcActivityAlwaysDailyPlan();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalcActivityAlwaysDailyPlan_WithEmptyDays_ExcludesEmptyDays()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateEmptyDay(DayOfWeekEnum.Tuesday),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcActivityAlwaysDailyPlan();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcActivityOutputNorm_AlwaysMeetsDaily_ReturnsDailyTimesDaysCount()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1500, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1500, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1500, 100),
            CreateDay(DayOfWeekEnum.Thursday, 1500, 100),
            CreateDay(DayOfWeekEnum.Friday, 1500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityOutputNorm();

        // Assert
        Assert.Equal(5000, result);
    }

    [Fact]
    public void CalcActivityOutputNorm_NotAlwaysMeetsDaily_ReturnsWeeklyBasedNorm()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 500, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 500, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityOutputNorm();

        // Assert
        Assert.Equal(3000, result);
    }

    [Fact]
    public void CalcActivityOutputNorm_SingleDayBelowDaily_ReturnsWeeklyBasedNorm()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityOutputNorm();

        // Assert
        Assert.Equal(3000, result);
    }

    [Fact]
    public void CalcActivityPercentage_ExactlyMeetsNorm_ReturnsOneHundred()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityPercentage();

        // Assert
        Assert.Equal(100.0, result);
    }

    [Fact]
    public void CalcActivityPercentage_ExceedsNorm_ReturnsPercentAboveOneHundred()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 2000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 2000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityPercentage();

        // Assert
        Assert.Equal(200.0, result);
    }

    [Fact]
    public void CalcActivityPercentage_BelowNorm_ReturnsPercentBelowOneHundred()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 500, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityPercentage();

        // Assert
        Assert.Equal(50.0, result);
    }

    [Fact]
    public void CalcActivityPercentage_WithWeeklyNorm_CalculatesCorrectly()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcActivityPercentage();

        // Assert
        Assert.Equal(100.0, result);
    }

    [Fact]
    public void CalcActivitySuccess_ExactlyOneHundredPercent_ReturnsTrue()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcActivitySuccess();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcActivitySuccess_OverOneHundredPercent_ReturnsTrue()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1500, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1500, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcActivitySuccess();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcActivitySuccess_BelowOneHundredPercent_ReturnsFalse()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 800, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 800, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcActivitySuccess();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalcTitanitTotal_AllDaysFilled_ReturnsSum()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 200),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 300),
            CreateDay(DayOfWeekEnum.Thursday, 1000, 400),
            CreateDay(DayOfWeekEnum.Friday, 1000, 500),
            CreateDay(DayOfWeekEnum.Saturday, 1000, 600),
            CreateDay(DayOfWeekEnum.Sunday, 1000, 700)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitTotal();

        // Assert
        Assert.Equal(2800, result);
    }

    [Fact]
    public void CalcTitanitTotal_WithEmptyDays_ExcludesEmptyDays()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateEmptyDay(DayOfWeekEnum.Tuesday),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 200),
            CreateEmptyDay(DayOfWeekEnum.Thursday),
            CreateDay(DayOfWeekEnum.Friday, 1000, 300),
            CreateDay(DayOfWeekEnum.Saturday, 1000, 400),
            CreateDay(DayOfWeekEnum.Sunday, 1000, 500)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitTotal();

        // Assert
        Assert.Equal(1500, result);
    }

    [Fact]
    public void CalcTitanitTotal_ZeroTitanitPoints_ReturnsSumExcludingZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 0),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitTotal();

        // Assert
        Assert.Equal(200, result);
    }

    [Fact]
    public void CalcTitanitMean_AllDaysEqual_ReturnsExpectedMean()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Thursday, 1000, 100),
            CreateDay(DayOfWeekEnum.Friday, 1000, 100),
            CreateDay(DayOfWeekEnum.Saturday, 1000, 100),
            CreateDay(DayOfWeekEnum.Sunday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitMean();

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void CalcTitanitMean_VaryingTitanit_ReturnsCorrectMean()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 200),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 300)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitMean();

        // Assert
        Assert.Equal(200, result);
    }

    [Fact]
    public void CalcTitanitStandardDeviation_AllDaysSame_ReturnsZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitStandardDeviation();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalcTitanitStandardDeviation_TwoValues_ReturnsCorrectDeviation()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 0),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 10)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitStandardDeviation();

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void CalcTitanitStandardDeviation_ThreeValues_ReturnsCorrectDeviation()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 2),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 8),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 11)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitStandardDeviation();

        // Assert
        double expected = Math.Sqrt((Math.Pow(2 - 7, 2) + Math.Pow(8 - 7, 2) + Math.Pow(11 - 7, 2)) / 3.0);
        Assert.Equal(expected, result, 10);
    }

    [Fact]
    public void CalcTitanitZeroDaysCount_NoZeroDays_ReturnsZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 50),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 150)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcTitanitZeroDaysCount();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalcTitanitZeroDaysCount_OneZeroDay_ReturnsOne()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 0),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 150)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcTitanitZeroDaysCount();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcTitanitZeroDaysCount_FractionalLessThanZeroPointFive_CountsAsZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 0.4),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 150)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcTitanitZeroDaysCount();

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcTitanitZeroDaysCount_FractionalAboveZeroPointFive_DoesNotCountAsZero()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 0.5),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 150)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        int result = service.CalcTitanitZeroDaysCount();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalcTitanitAlwaysDailyPlan_AllDaysMeetRequirement_ReturnsTrue()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcTitanitAlwaysDailyPlan();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcTitanitAlwaysDailyPlan_OneDayBelowRequirement_ReturnsFalse()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 50),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcTitanitAlwaysDailyPlan();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalcTitanitAlwaysDailyPlan_ExactlyAtRequirement_ReturnsTrue()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcTitanitAlwaysDailyPlan();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcTitanitAlwaysDailyPlan_JustBelowRequirement_ReturnsFalse()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 99.99),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcTitanitAlwaysDailyPlan();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalcTitanitOutputNorm_AlwaysMeetsDaily_ReturnsDailyTimesDaysCount()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 150),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 150),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 150),
            CreateDay(DayOfWeekEnum.Thursday, 1000, 150),
            CreateDay(DayOfWeekEnum.Friday, 1000, 150)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitOutputNorm();

        // Assert
        Assert.Equal(500, result);
    }

    [Fact]
    public void CalcTitanitOutputNorm_NotAlwaysMeetsDaily_ReturnsWeeklyBasedNorm()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 50),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 50),
            CreateDay(DayOfWeekEnum.Wednesday, 1000, 50)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitOutputNorm();

        // Assert
        Assert.Equal(300, result);
    }

    [Fact]
    public void CalcTitanitPercentage_ExactlyMeetsNorm_ReturnsOneHundred()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitPercentage();

        // Assert
        Assert.Equal(100.0, result);
    }

    [Fact]
    public void CalcTitanitPercentage_ExceedsNorm_ReturnsPercentAboveOneHundred()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 200),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 200)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitPercentage();

        // Assert
        Assert.Equal(200.0, result);
    }

    [Fact]
    public void CalcTitanitPercentage_BelowNorm_ReturnsPercentBelowOneHundred()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 50),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 50)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        double result = service.CalcTitanitPercentage();

        // Assert
        Assert.Equal(50.0, result);
    }

    [Fact]
    public void CalcTitanitSuccess_ExactlyOneHundredPercent_ReturnsTrue()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 100),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 100)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcTitanitSuccess();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcTitanitSuccess_OverOneHundredPercent_ReturnsTrue()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 150),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 150)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcTitanitSuccess();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalcTitanitSuccess_BelowOneHundredPercent_ReturnsFalse()
    {
        // Arrange
        List<Day> days = new()
        {
            CreateDay(DayOfWeekEnum.Monday, 1000, 80),
            CreateDay(DayOfWeekEnum.Tuesday, 1000, 80)
        };
        Player player = CreatePlayer("TestPlayer", days);
        PlayerStatisticsService service = CreateService(player);

        // Act
        bool result = service.CalcTitanitSuccess();

        // Assert
        Assert.False(result);
    }
}