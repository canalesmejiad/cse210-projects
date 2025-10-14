namespace ExerciseTracking
{
    public sealed class Cycling : Activity
    {
        private readonly double _avgSpeedMph;


        public Cycling(System.DateTime date, int minutes, double avgSpeedMph)
            : base(date, minutes)
        {
            _avgSpeedMph = avgSpeedMph;
        }

        public override double GetDistance()
        {

            return _avgSpeedMph * (Minutes / 60.0);
        }

        public override double GetSpeed() => _avgSpeedMph;

        public override double GetPace()
        {

            return _avgSpeedMph == 0 ? 0 : 60.0 / _avgSpeedMph;
        }
    }
}