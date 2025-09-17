using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Testing Getters and Setters...");

        Fraction f = new Fraction(3, 4);
        Console.WriteLine("Original fraction: " + f.GetFractionString());

        // Cambiar numerador
        f.SetTop(5);
        Console.WriteLine("New top: " + f.GetTop());

        // Cambiar denominador
        f.SetBottom(6);
        Console.WriteLine("New bottom: " + f.GetBottom());

        Console.WriteLine("Updated fraction: " + f.GetFractionString());
    }
}