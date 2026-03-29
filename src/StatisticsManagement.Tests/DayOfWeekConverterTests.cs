using StatisticsManagement.Common;
using StatisticsManagement.Converters;
using Xunit;

namespace StatisticsManagement.Tests;

public class DayOfWeekConverterTests
{
    private readonly DayOfWeekConverter _converter = new();

    [Fact]
    public void ConvertToEnum_LowercaseMonday_ReturnsMonday()
    {
        // Arrange
        string input = "пн";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Monday, result);
    }

    [Fact]
    public void ConvertToEnum_UppercaseMonday_ReturnsMonday()
    {
        // Arrange
        string input = "ПН";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Monday, result);
    }

    [Fact]
    public void ConvertToEnum_MixedCaseMonday_ReturnsMonday()
    {
        // Arrange
        string input = "Пн";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Monday, result);
    }

    [Fact]
    public void ConvertToEnum_LowercaseTuesday_ReturnsTuesday()
    {
        // Arrange
        string input = "вт";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Tuesday, result);
    }

    [Fact]
    public void ConvertToEnum_UppercaseTuesday_ReturnsTuesday()
    {
        // Arrange
        string input = "ВТ";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Tuesday, result);
    }

    [Fact]
    public void ConvertToEnum_LowercaseWednesday_ReturnsWednesday()
    {
        // Arrange
        string input = "ср";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Wednesday, result);
    }

    [Fact]
    public void ConvertToEnum_LowercaseThursday_ReturnsThursday()
    {
        // Arrange
        string input = "чт";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Thursday, result);
    }

    [Fact]
    public void ConvertToEnum_LowercaseFriday_ReturnsFriday()
    {
        // Arrange
        string input = "пт";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Friday, result);
    }

    [Fact]
    public void ConvertToEnum_LowercaseSaturday_ReturnsSaturday()
    {
        // Arrange
        string input = "сб";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Saturday, result);
    }

    [Fact]
    public void ConvertToEnum_LowercaseSunday_ReturnsSunday()
    {
        // Arrange
        string input = "вс";

        // Act
        var result = _converter.ConvertToEnum(input);

        // Assert
        Assert.Equal(DayOfWeekEnum.Sunday, result);
    }

    [Fact]
    public void ConvertToEnum_EmptyString_ThrowsArgumentException()
    {
        // Arrange
        string input = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _converter.ConvertToEnum(input));
    }

    [Fact]
    public void ConvertToEnum_Null_ThrowsArgumentException()
    {
        // Arrange
        string input = null!;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _converter.ConvertToEnum(input));
    }

    [Fact]
    public void ConvertToEnum_Whitespace_ThrowsArgumentException()
    {
        // Arrange
        string input = "   ";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _converter.ConvertToEnum(input));
    }

    [Fact]
    public void ConvertToEnum_InvalidString_ThrowsArgumentException()
    {
        // Arrange
        string input = "xyz";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _converter.ConvertToEnum(input));
    }

    [Fact]
    public void ConvertToString_Monday_ReturnsUpperMonday()
    {
        // Arrange
        DayOfWeekEnum input = DayOfWeekEnum.Monday;

        // Act
        string result = _converter.ConvertToString(input);

        // Assert
        Assert.Equal("ПН", result);
    }

    [Fact]
    public void ConvertToString_Tuesday_ReturnsUpperTuesday()
    {
        // Arrange
        DayOfWeekEnum input = DayOfWeekEnum.Tuesday;

        // Act
        string result = _converter.ConvertToString(input);

        // Assert
        Assert.Equal("ВТ", result);
    }

    [Fact]
    public void ConvertToString_Wednesday_ReturnsUpperWednesday()
    {
        // Arrange
        DayOfWeekEnum input = DayOfWeekEnum.Wednesday;

        // Act
        string result = _converter.ConvertToString(input);

        // Assert
        Assert.Equal("СР", result);
    }

    [Fact]
    public void ConvertToString_Thursday_ReturnsUpperThursday()
    {
        // Arrange
        DayOfWeekEnum input = DayOfWeekEnum.Thursday;

        // Act
        string result = _converter.ConvertToString(input);

        // Assert
        Assert.Equal("ЧТ", result);
    }

    [Fact]
    public void ConvertToString_Friday_ReturnsUpperFriday()
    {
        // Arrange
        DayOfWeekEnum input = DayOfWeekEnum.Friday;

        // Act
        string result = _converter.ConvertToString(input);

        // Assert
        Assert.Equal("ПТ", result);
    }

    [Fact]
    public void ConvertToString_Saturday_ReturnsUpperSaturday()
    {
        // Arrange
        DayOfWeekEnum input = DayOfWeekEnum.Saturday;

        // Act
        string result = _converter.ConvertToString(input);

        // Assert
        Assert.Equal("СБ", result);
    }

    [Fact]
    public void ConvertToString_Sunday_ReturnsUpperSunday()
    {
        // Arrange
        DayOfWeekEnum input = DayOfWeekEnum.Sunday;

        // Act
        string result = _converter.ConvertToString(input);

        // Assert
        Assert.Equal("ВС", result);
    }

    [Fact]
    public void Convert_RoundTrip_AllDays_ReturnsUppercase()
    {
        // Arrange
        string[] inputs = ["пн", "вт", "ср", "чт", "пт", "сб", "вс"];
        string[] expected = ["ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС"];

        // Act & Assert
        for (int i = 0; i < inputs.Length; i++)
        {
            var enumValue = _converter.ConvertToEnum(inputs[i]);
            string result = _converter.ConvertToString(enumValue);
            Assert.Equal(expected[i], result);
        }
    }
}