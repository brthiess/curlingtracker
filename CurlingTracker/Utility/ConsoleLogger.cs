using System;
using System.Collections.Generic;
using CurlingTracker.Models;


namespace CurlingTracker 
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        public void Log(string listTitle, List<string> strings)
        {
            string resultString = listTitle + "\n";
            foreach(string s in strings)
            {
                resultString += s + "\n";
            }
            Console.WriteLine(resultString);
        }

        public void Log(string listTitle, List<Event> events)
        {
            string resultString = listTitle + "\n";
            foreach(Event e in events)
            {
                resultString += e.Print() + "\n";
            }
            Console.WriteLine(resultString);
        }
    }
}