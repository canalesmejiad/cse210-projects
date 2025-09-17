using System;

public class Fraction
{
    private int _top;
    private int _bottom;

    // 1) Constructor sin parámetros -> 1/1
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    // 2) Constructor con un parámetro -> top/1
    public Fraction(int top)
    {
        _top = top;
        _bottom = 1;
    }

    // 3) Constructor con dos parámetros -> top/bottom
    public Fraction(int top, int bottom)
    {
        if (bottom == 0)
            throw new ArgumentException("Denominator cannot be zero.");

        _top = top;
        _bottom = bottom;
    }


    public int GetTop() => _top;

    public void SetTop(int top)
    {
        _top = top;
    }

    public int GetBottom() => _bottom;

    public void SetBottom(int bottom)
    {
        if (bottom == 0)
            throw new ArgumentException("Denominator cannot be zero.");

        _bottom = bottom;
    }


    public string GetFractionString()
    {
        return _bottom == 1 ? $"{_top}" : $"{_top}/{_bottom}";
    }

    public double GetDecimalValue()
    {
        return (double)_top / _bottom;
    }
}