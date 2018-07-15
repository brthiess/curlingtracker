using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using CurlingTracker.Models;

namespace CurlingTracker.Utility
{
    public class StringUtil
    {
        public static string FirstLetterToUpper(string str)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

        public static string ToXML(Dictionary<int, End> dictionary)
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(dictionary.GetType());
            serializer.Serialize(stringwriter, dictionary);
            return stringwriter.ToString();
        }

        public static Dictionary<int, End> LoadFromXMLString(string xmlText)
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(Dictionary<int, End>));
            return serializer.Deserialize(stringReader) as Dictionary<int, End>;
        }
    }
}