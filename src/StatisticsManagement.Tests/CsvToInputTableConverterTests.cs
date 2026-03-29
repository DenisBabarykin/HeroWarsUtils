using StatisticsManagement.Common;
using StatisticsManagement.Converters;
using StatisticsManagement.InputModels;
using Xunit;

namespace StatisticsManagement.Tests;

public class CsvToInputTableConverterTests
{
    private readonly CsvToInputTableConverter _converter = new();

    [Fact]
    public void Convert_EmptyCsv_ReturnsEmptyTable()
    {
        // Arrange
        string csv = "Игрок,пн,вт,ср,чт,пт,сб,вс";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.InputPlayers);
    }

    [Fact]
    public void Convert_SinglePlayer_ReturnsTableWithOnePlayer()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,6358,5808,11726,6612,8138,5712,2956";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.InputPlayers);
        Assert.Equal("Диман", result.InputPlayers[0].Name);
        Assert.Equal(7, result.InputPlayers[0].InputDays.Count);
    }

    [Fact]
    public void Convert_MultiplePlayers_ReturnsTableWithAllPlayers()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,6358,5808,11726,6612,8138,5712,2956
DenBar h3fc.ru,3880,4710,6630,3596,4896,4250,3580";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.InputPlayers.Count);
        Assert.Equal("Диман", result.InputPlayers[0].Name);
        Assert.Equal("DenBar h3fc.ru", result.InputPlayers[1].Name);
    }

    [Fact]
    public void Convert_PlayerDays_HaveCorrectDayOfWeekEnum()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,6358,5808,11726,6612,8138,5712,2956";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.Equal(DayOfWeekEnum.Monday, result.InputPlayers[0].InputDays[0].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Tuesday, result.InputPlayers[0].InputDays[1].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Wednesday, result.InputPlayers[0].InputDays[2].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Thursday, result.InputPlayers[0].InputDays[3].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Friday, result.InputPlayers[0].InputDays[4].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Saturday, result.InputPlayers[0].InputDays[5].DayOfWeek);
        Assert.Equal(DayOfWeekEnum.Sunday, result.InputPlayers[0].InputDays[6].DayOfWeek);
    }

    [Fact]
    public void Convert_PlayerDays_HaveCorrectPoints()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,6358,5808,11726,6612,8138,5712,2956";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.Equal(6358, result.InputPlayers[0].InputDays[0].Points);
        Assert.Equal(5808, result.InputPlayers[0].InputDays[1].Points);
        Assert.Equal(11726, result.InputPlayers[0].InputDays[2].Points);
        Assert.Equal(6612, result.InputPlayers[0].InputDays[3].Points);
        Assert.Equal(8138, result.InputPlayers[0].InputDays[4].Points);
        Assert.Equal(5712, result.InputPlayers[0].InputDays[5].Points);
        Assert.Equal(2956, result.InputPlayers[0].InputDays[6].Points);
    }

    [Fact]
    public void Convert_WithZeroPoints_ReturnsZeroPoints()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Goltisida,0,1114,1358,1306,1862,1744,2616";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.Equal(0, result.InputPlayers[0].InputDays[0].Points);
    }

    [Fact]
    public void Convert_WithNegativePoints_ReturnsNegativePoints()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Alucard_vrn,-1,-1,8,1314,988,1730,1604";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.Equal(-1, result.InputPlayers[0].InputDays[0].Points);
        Assert.Equal(-1, result.InputPlayers[0].InputDays[1].Points);
    }

    [Fact]
    public void Convert_PlayerWithSpacesInName_ReturnsCorrectName()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
DenBar h3fc.ru,3880,4710,6630,3596,4896,4250,3580";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.Equal("DenBar h3fc.ru", result.InputPlayers[0].Name);
    }

    [Fact]
    public void Convert_LessThanSevenDays_ThrowsFormatException()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср
Диман,6358,5808,11726";

        // Act & Assert
        Assert.Throws<FormatException>(() => _converter.Convert(csv));
    }

    [Fact]
    public void Convert_MoreThanSevenDaysData_ThrowsFormatException()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб
Диман,6358,5808,11726,6612,8138,5712,2956";

        // Act & Assert
        Assert.Throws<FormatException>(() => _converter.Convert(csv));
    }

    [Fact]
    public void Convert_WithEmptyLines_IgnoresEmptyLines()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,6358,5808,11726,6612,8138,5712,2956

DenBar h3fc.ru,3880,4710,6630,3596,4896,4250,3580";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.Equal(2, result.InputPlayers.Count);
    }

    [Fact]
    public void Convert_FullExample_ReturnsAllPlayers()
    {
        // Arrange
        string csv = @"Игрок,пн,вт,ср,чт,пт,сб,вс
Диман,6358,5808,11726,6612,8138,5712,2956
DenBar h3fc.ru,3880,4710,6630,3596,4896,4250,3580
Xoma32,3902,3864,6806,5774,4950,2140,1558";

        // Act
        var result = _converter.Convert(csv);

        // Assert
        Assert.Equal(3, result.InputPlayers.Count);
        Assert.All(result.InputPlayers, player => Assert.Equal(7, player.InputDays.Count));
    }
}
