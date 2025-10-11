// ChecklistGoal.cs
using System;

public class ChecklistGoal : Goal
{
    // Campos privados (encapsulación)
    private int _targetCount;
    private int _currentCount;
    private int _bonus;


    public ChecklistGoal(string name, string desc, int points, int target, int bonus)
        : base(name, desc, points)
    {
        _targetCount = Math.Max(1, target);
        _bonus = Math.Max(0, bonus);
        _currentCount = 0;
    }


    public override bool IsComplete => _currentCount >= _targetCount;


    public override int RecordEvent()
    {
        if (IsComplete) return 0;

        _currentCount++;
        int earned = Points;
        if (_currentCount == _targetCount)
        {
            earned += _bonus;
        }
        return earned;
    }


    public override string GetStatus()
    {
        string box = IsComplete ? "[X]" : "[ ]";
        return $"{box} {Name} ({Description}) -- {_currentCount}/{_targetCount}";
    }


    public int TargetCount => _targetCount;
    public int CurrentCount => _currentCount;
    public int Bonus => _bonus;

    public void SetCurrent(int value)
    {
        _currentCount = Math.Max(0, Math.Min(value, _targetCount));
    }
}