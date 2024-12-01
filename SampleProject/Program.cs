Console.WriteLine("Hello! Let's add 2 numbers together!");

Console.Write("Enter the first number: ");
int number1 = Convert.ToInt32(Console.ReadLine());
Console.Write("Enter the second number: ");
int number2 = Convert.ToInt32(Console.ReadLine());

var calculator = new Calculator();

var result = calculator.Add(number1, number2);

Console.WriteLine($"Result: {result}");

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}