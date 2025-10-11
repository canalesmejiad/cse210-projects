class CheckListGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int Bonus { get; set; }
    public ChecklistGoal(string name, string desc, int points, int target, int bonus) : base(name, desc, points)
    {
        TargetCount = target;
        Bonus = bonus;
        CurrentCount = 0;
    }

    public override int RecordEvent()
    {
        if (CurrentCount < TargetCount)
        {
            CurrentCount++;
            if (CurrentCount == TargetCount)
                return Points + Bonus;
            return Points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        string box = CurrentCount >= TargetCount ? "[X]" : "[ ]";
        return $"{box} {Name} ({Description}) -- {CurrentCount}/{TargetCount}";
    }
}

