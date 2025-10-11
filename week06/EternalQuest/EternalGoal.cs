class CheckListGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int Bonus { get; set; }

    public CheckListGoal(string name, string desc, int points, int target, int bonus) : base(name, desc, points) { }
    public override int RecordEvent() => Points;
    public override string GetStatus() => $"[∞] {Name} ({Description})";
}