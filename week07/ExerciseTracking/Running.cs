namespace ExerciseTracking
{
    public sealed class Running : Activity
    {
        private readonly double _distanceMiles;


        public Running(System.DateTime date, int minutes, double distanceMiles)
            : base(date, minutes)
        {
            _distanceMiles = distanceMiles;
        }

        public override double GetDistance() => _distanceMiles;

        public override double GetSpeed()
        {

            return (_distanceMiles / Minutes) * 60.0;
        }

        public override double GetPace()
        {

            return _distanceMiles == 0 ? 0 : Minutes / _distanceMiles;
        }
    }
}