public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string desc, int points) : base(name, desc, points)
    {
        _isComplete = false;
    }

    public override bool IsComplete => _isComplete;

    public override int RecordEvent()
    {
        if (_isComplete) return 0;
        _isComplete = true;
        return Points;
    }

    public override string GetStatus()
        => $"{(_isComplete ? "[X]" : "[ ]")} {Name} ({Description})";


    public void SetComplete(bool value)
    {
        _isComplete = value;
    }
}