namespace SampleProject.Tests;
public class CalculatorTests
{
    [Test]
    public async Task Add_WhenCalled_ReturnsTheSumOfArguments()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(1, 2);

        // Assert
        await Assert.That(result).IsEqualTo(3);
    }

    [Test]
    public async Task Add_WhenOverflowing_ReturnsIntMinValue()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(int.MaxValue, 1);

        // Assert
        await Assert.That(result).IsEqualTo(int.MinValue);
    }
}