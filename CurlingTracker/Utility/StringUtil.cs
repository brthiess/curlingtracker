using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using CurlingTracker.Models;
using Newtonsoft.Json;
using System.Text;

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

        public static string ToJson(Dictionary<int, End> dictionary)
        {
            string serializedObject = JsonConvert.SerializeObject(dictionary);
            return serializedObject;
        }

        public static Dictionary<int, End> LoadFromJsonString(string jsonText)
        {
            var dictionary = JsonConvert.DeserializeObject<Dictionary<int, End>>(jsonText);
            return dictionary;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}