using StatisticsManagement.StringComparation;

namespace StatisticsManagement.Tests;

public class SoftWxDamerauComparatorTests
{
    private readonly IStringDistanceComparator _comparator = new SoftWxDamerauComparator();

    [Fact]
    public void CalcDistance_IdenticalStrings_ReturnsZero()
    {
        // Arrange
        string str1 = "test";
        string str2 = "test";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalcDistance_EmptyStrings_ReturnsZero()
    {
        // Arrange
        string str1 = "";
        string str2 = "";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalcDistance_OneEmptyString_ReturnsLengthOfOther()
    {
        // Arrange
        string str1 = "";
        string str2 = "test";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void CalcDistance_SingleInsertion_ReturnsOne()
    {
        // Arrange
        string str1 = "test";
        string str2 = "tests";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcDistance_SingleDeletion_ReturnsOne()
    {
        // Arrange
        string str1 = "tests";
        string str2 = "test";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcDistance_SingleSubstitution_ReturnsOne()
    {
        // Arrange
        string str1 = "test";
        string str2 = "best";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcDistance_SingleTransposition_ReturnsOne()
    {
        // Arrange
        string str1 = "test";
        string str2 = "tset";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcDistance_MultipleEdits_ReturnsHigherDistance()
    {
        // Arrange
        string str1 = "kitten";
        string str2 = "sitting";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void CalcDistance_MultipleTranspositions_ReturnsHigherDistance()
    {
        // Arrange
        string str1 = "abcdef";
        string str2 = "bacdef";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalcDistance_ComplexEditsAndTranspositions_ReturnsHighDistance()
    {
        // Arrange
        string str1 = "algorithm";
        string str2 = "thalamus";

        // Act
        int result = _comparator.CalcDistance(str1, str2);

        // Assert
        Assert.True(result > 0);
        Assert.True(result > 3);
    }

    [Fact]
    public void CalcDistance_MoreEditsHaveMoreDistance()
    {
        // Arrange
        string baseString = "test";
        string oneEdit = "best";
        string twoEdits = "bext";
        string threeEdits = "bxty";

        // Act
        int distOne = _comparator.CalcDistance(baseString, oneEdit);
        int distTwo = _comparator.CalcDistance(baseString, twoEdits);
        int distThree = _comparator.CalcDistance(baseString, threeEdits);

        // Assert
        Assert.True(distTwo > distOne);
        Assert.True(distThree > distTwo);
    }

    [Fact]
    public void CalcDistance_TranspositionHasLowerDistanceThanSubstitutions()
    {
        // Arrange
        string str1 = "ab";
        string transposition = "ba";
        string substitutions = "xy";

        // Act
        int distTransposition = _comparator.CalcDistance(str1, transposition);
        int distSubstitutions = _comparator.CalcDistance(str1, substitutions);

        // Assert
        Assert.Equal(1, distTransposition);
        Assert.Equal(2, distSubstitutions);
    }

    [Fact]
    public void CalcDistance_DifferentStringsWithSameLength_DifferentDistance()
    {
        // Arrange
        string str1 = "car";
        string str2 = "cat";
        string str3 = "dog";

        // Act
        int dist1 = _comparator.CalcDistance(str1, str2);
        int dist2 = _comparator.CalcDistance(str1, str3);

        // Assert
        Assert.Equal(1, dist1);
        Assert.Equal(3, dist2);
    }
}
