using StatisticsManagement.Common;
using StatisticsManagement.Converters;
using StatisticsManagement.CalculationModels;
using StatisticsManagement.InputModels;
using Xunit;

namespace StatisticsManagement.Tests;

public class InputToCalculationTableConverterTests
{
    private readonly InputToCalculationTableConverter _converter = new();
    private readonly CsvToInputTableConverter _csvConverter = new();

    [Fact]
    public void Convert_MatchingTables_ReturnsConvertedTable()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,100,200,300,400,500,600,700
Player2,150,250,350,450,550,650,750";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,10,20,30,40,50,60,70
Player2,15,25,35,45,55,65,75";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Players.Count);
    }

    [Fact]
    public void Convert_MatchingTables_ReturnsCorrectPoints()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,100,200,300,400,500,600,700";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Single(result.Players);
        Assert.Equal("Player1", result.Players[0].Name);
        Assert.Equal(7, result.Players[0].Days.Count);
        Assert.Equal(100, result.Players[0].Days[0].ActivityPoints);
        Assert.Equal(10, result.Players[0].Days[0].TitanitPoints);
        Assert.Equal(200, result.Players[0].Days[1].ActivityPoints);
        Assert.Equal(20, result.Players[0].Days[1].TitanitPoints);
    }

    [Fact]
    public void Convert_NegativeActivityPoints_CreatesEmptyDay()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,-1,200,300,400,500,600,700";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Single(result.Players);
        Assert.True(result.Players[0].Days[0].IsEmpty);
    }

    [Fact]
    public void Convert_NegativeTitanitPoints_CreatesEmptyDay()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,100,200,300,400,500,600,700";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,-1,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Single(result.Players);
        Assert.True(result.Players[0].Days[0].IsEmpty);
    }

    [Fact]
    public void Convert_BothNegativePoints_CreatesEmptyDay()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,-1,200,300,400,500,600,700";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,-1,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Single(result.Players);
        Assert.True(result.Players[0].Days[0].IsEmpty);
    }

    [Fact]
    public void Convert_DifferentPlayerCount_ThrowsArgumentException()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,100,200,300,400,500,600,700
Player2,150,250,350,450,550,650,750";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _converter.Convert(activityTable, titanitTable));
    }

    [Fact]
    public void Convert_MatchingWithSlightNameDifference_ReturnsConvertedTable()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Maximussimo,100,200,300,400,500,600,700";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Maximus,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Players);
        Assert.Equal("Maximussimo", result.Players[0].Name);
    }

    [Fact]
    public void Convert_DaysHaveCorrectDayOfWeek()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,100,200,300,400,500,600,700";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Equal(DayOfWeekEnum.Monday, result.Players[0].Days[0].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Tuesday, result.Players[0].Days[1].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Wednesday, result.Players[0].Days[2].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Thursday, result.Players[0].Days[3].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Friday, result.Players[0].Days[4].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Saturday, result.Players[0].Days[5].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Sunday, result.Players[0].Days[6].DayOfWeek);
    }

    [Fact]
    public void Convert_ZeroPoints_AreValid()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,0,0,0,0,0,0,0";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,0,0,0,0,0,0,0";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Single(result.Players);
        Assert.False(result.Players[0].Days[0].IsEmpty);
        Assert.Equal(0, result.Players[0].Days[0].ActivityPoints);
        Assert.Equal(0, result.Players[0].Days[0].TitanitPoints);
    }

    [Fact]
    public void Convert_FullActivityData_ReturnsAllPlayers()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,6358,5808,11726,6612,8138,5712,2956
DenBar h3fc.ru,3880,4710,6630,3596,4896,4250,3580
Xoma32,3902,3864,6806,5774,4950,2140,1558";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,100,200,300,400,500,600,700
DenBar h3fc.ru,150,250,350,450,550,650,750
Xoma32,180,280,380,480,580,680,780";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Players.Count);
        Assert.All(result.Players, player => Assert.Equal(7, player.Days.Count));
    }

    [Fact]
    public void Convert_MixedNegativeAndValidPoints_HandlesCorrectly()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Alucard_vrn,-1,-1,8,1314,988,1730,1604";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Alucard_vrn,-1,-1,60,126,144,288,174";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Single(result.Players);
        Assert.True(result.Players[0].Days[0].IsEmpty);
        Assert.True(result.Players[0].Days[1].IsEmpty);
        Assert.False(result.Players[0].Days[2].IsEmpty);
        Assert.Equal(8, result.Players[0].Days[2].ActivityPoints);
        Assert.Equal(60, result.Players[0].Days[2].TitanitPoints);
    }

    [Fact]
    public void Convert_EmptyActivityTable_ThrowsArgumentException()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _converter.Convert(activityTable, titanitTable));
    }

    [Fact]
    public void Convert_EmptyTitanitTable_ThrowsArgumentException()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,100,200,300,400,500,600,700";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _converter.Convert(activityTable, titanitTable));
    }

    [Fact]
    public void Convert_PlayerWithSpacesInName_MatchesCorrectly()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
DenBar h3fc.ru,3880,4710,6630,3596,4896,4250,3580";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
DenBar h3fc.ru,325,330,330,75,345,390,270";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Single(result.Players);
        Assert.Equal("DenBar h3fc.ru", result.Players[0].Name);
    }

    [Fact]
    public void Convert_CyrillicNames_MatchesCorrectly()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,6358,5808,11726,6612,8138,5712,2956
Александр,2190,2534,3640,1872,3648,1644,2300";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,100,200,300,400,500,600,700
Александр,210,270,306,570,330,210,570";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Equal(2, result.Players.Count);
    }

    [Fact]
    public void Convert_MultiplePlayers_AllDaysProcessed()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Maximussimo,1248,2680,10494,4220,5000,1168,952
The Undead,2490,2132,2102,3114,3956,4766,1546
Detka,2710,1692,5126,1794,6180,1470,476";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Maximussimo,1050,810,966,912,786,828,792
The Undead,750,570,510,930,870,450,510
Detka,432,366,492,510,486,372,210";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Equal(3, result.Players.Count);
        foreach (var player in result.Players)
        {
            Assert.Equal(7, player.Days.Count);
            foreach (var day in player.Days)
            {
                Assert.False(day.IsEmpty);
            }
        }
    }

    [Fact]
    public void Convert_AllNegativeDays_RaisesExceptionFromDayConstructor()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,-1,-1,-1,-1,-1,-1,-1";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act & Assert - all days will be empty, which violates Player constructor requirement
        Assert.Throws<ArgumentException>(() => _converter.Convert(activityTable, titanitTable));
    }

    [Fact]
    public void Convert_ReversedPlayerOrder_MapsCorrectly()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player1,100,200,300,400,500,600,700
Player2,150,250,350,450,550,650,750
Player3,200,300,400,500,600,700,800";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Player3,30,40,50,60,70,80,90
Player2,20,30,40,50,60,70,80
Player1,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Equal(3, result.Players.Count);
        Assert.Equal("Player1", result.Players[0].Name);
        Assert.Equal(100, result.Players[0].Days[0].ActivityPoints);
        Assert.Equal(10, result.Players[0].Days[0].TitanitPoints);
        Assert.Equal("Player2", result.Players[1].Name);
        Assert.Equal(150, result.Players[1].Days[0].ActivityPoints);
        Assert.Equal(20, result.Players[1].Days[0].TitanitPoints);
        Assert.Equal("Player3", result.Players[2].Name);
        Assert.Equal(200, result.Players[2].Days[0].ActivityPoints);
        Assert.Equal(30, result.Players[2].Days[0].TitanitPoints);
    }

    [Fact]
    public void Convert_ScrambledPlayerOrder_MapsCorrectly()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Alpha,100,200,300,400,500,600,700
Beta,150,250,350,450,550,650,750
Gamma,200,300,400,500,600,700,800
Delta,250,350,450,550,650,750,850";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Gamma,70,80,90,100,110,120,130
Alpha,10,20,30,40,50,60,70
Delta,90,100,110,120,130,140,150
Beta,30,40,50,60,70,80,90";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Equal(4, result.Players.Count);
        Assert.Equal("Alpha", result.Players[0].Name);
        Assert.Equal(100, result.Players[0].Days[0].ActivityPoints);
        Assert.Equal(10, result.Players[0].Days[0].TitanitPoints);
        Assert.Equal("Beta", result.Players[1].Name);
        Assert.Equal(150, result.Players[1].Days[0].ActivityPoints);
        Assert.Equal(30, result.Players[1].Days[0].TitanitPoints);
        Assert.Equal("Gamma", result.Players[2].Name);
        Assert.Equal(200, result.Players[2].Days[0].ActivityPoints);
        Assert.Equal(70, result.Players[2].Days[0].TitanitPoints);
        Assert.Equal("Delta", result.Players[3].Name);
        Assert.Equal(250, result.Players[3].Days[0].ActivityPoints);
        Assert.Equal(90, result.Players[3].Days[0].TitanitPoints);
    }

    [Fact]
    public void Convert_SlightlyDifferentNamesWithDifferentOrder_MapsCorrectly()
    {
        // Arrange
        string activityCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Maximussimo,100,200,300,400,500,600,700
JohnDoe,150,250,350,450,550,650,750";

        string titanitCsv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
JohnDoe,20,30,40,50,60,70,80
Maximus,10,20,30,40,50,60,70";

        var activityTable = _csvConverter.Convert(activityCsv);
        var titanitTable = _csvConverter.Convert(titanitCsv);

        // Act
        var result = _converter.Convert(activityTable, titanitTable);

        // Assert
        Assert.Equal(2, result.Players.Count);
        Assert.Equal("Maximussimo", result.Players[0].Name);
        Assert.Equal(100, result.Players[0].Days[0].ActivityPoints);
        Assert.Equal(10, result.Players[0].Days[0].TitanitPoints);
        Assert.Equal("JohnDoe", result.Players[1].Name);
        Assert.Equal(150, result.Players[1].Days[0].ActivityPoints);
        Assert.Equal(20, result.Players[1].Days[0].TitanitPoints);
    }
}
