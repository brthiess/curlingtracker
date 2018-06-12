using System;

namespace CurlingTracker.Utility {
    public class DateUtil{
        private static Random gen = new Random();
        public static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;           
            return start.AddDays(gen.Next(range));
        }
    }
}