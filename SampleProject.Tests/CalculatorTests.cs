namespace SampleProject.Tests;

using TUnit;

public class CalculatorTests
{
    [Test]
    public void Add_WhenCalled_ReturnsTheSumOfArguments()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(1, 2);

        // Assert
        Assert.Equals(3, result);
    }
}