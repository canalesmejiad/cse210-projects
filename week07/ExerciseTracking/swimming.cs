namespace ExerciseTracking
{
    public sealed class Swimming : Activity
    {
        private readonly int _laps;

        public Swimming(System.DateTime date, int minutes, int laps)
            : base(date, minutes)
        {
            _laps = laps;
        }

        private double DistanceMilesFromLaps()
        {
            return _laps * 50.0 / 1000.0 * 0.62;
        }

        public override double GetDistance() => DistanceMilesFromLaps();

        public override double GetSpeed()
        {
            double d = GetDistance();
            return d == 0 ? 0 : (d / Minutes) * 60.0;
        }

        public override double GetPace()
        {
            double d = GetDistance();
            return d == 0 ? 0 : Minutes / d;
        }
    }
}