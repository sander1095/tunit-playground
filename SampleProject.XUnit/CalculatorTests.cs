namespace SampleProject.XUnit;
public class CalculatorTests
{
    [Fact]
    public void Add_WhenCalled_ReturnsTheSumOfArguments()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(1, 2);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void Add_WhenOverflowing_ReturnsIntMinValue()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(int.MaxValue, 1);

        // Assert
        Assert.Equal(int.MinValue, result);
    }
}