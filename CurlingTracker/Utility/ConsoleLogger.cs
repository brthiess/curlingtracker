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
            string resultString = "\n\n********" + listTitle + "********\n";
            foreach(string s in strings)
            {
                resultString += s + "\n";
            }
            Console.WriteLine(resultString);
        }

        public void Log(string listTitle, IEnumerable<IPrintable> iPrintables)
        {
            string resultString = "\n\n********" + listTitle + "********\n";
            foreach(IPrintable p in iPrintables)
            {
                resultString += p.Print() + "\n";
            }
            Console.WriteLine(resultString);
        }

        public void Log(string message, IPrintable iPrintable)
        {
            Console.WriteLine("\n" + message);
            Console.WriteLine(iPrintable.Print());
        }
    }
}