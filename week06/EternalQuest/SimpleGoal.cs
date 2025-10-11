class SimpleGoal : Goal
{
    public bool IsComplete { get; set; }
    public SimpleGoal(string name, string desc, int points) : base(name, desc, points)
    {
        IsComplete = false;
    }
    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            return Points;
        }
        return 0;
    }
    public override string GetStatus()
    {
        string box = IsComplete ? "[X]" : "[]";
        return $"{box} {Name} ({Description})";
    }
}