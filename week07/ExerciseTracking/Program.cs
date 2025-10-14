using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    class Program
    {
        static void Main()
        {
            var activities = new List<Activity>
            {
                new Running(new DateTime(2022, 11, 3), 30, 3.0),
                new Cycling(new DateTime(2022, 11, 3), 45, 16.0),
                new Swimming(new DateTime(2022, 11, 3), 25, 40)
            };

            foreach (var a in activities)
            {
                Console.WriteLine(a.GetSummary());
            }
        }
    }
}