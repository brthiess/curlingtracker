using System;

namespace CurlingTracker.Models
{
    public class Player 
    {
        public enum Position{Lead, Second, Third, Fourth}
        public Player(string firstName, string lastName, Gender gender, Position position, bool isSkip, string image = "default.png")
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.position = position;
            this.Image = image;
            this.IsSkip = isSkip;
        }


        public Guid PlayerId {get; set;}

        public string FirstName {get;set;}
        public string LastName {get;set;}

        public string Image {get;set;}
        public Position position {get;set;}

        public bool IsSkip {get;set;}

        public string GetFullName() 
        {
            return FirstName + " " + LastName;
        }       

        public string GetPositionName()
        {
            if (!IsSkip)
            {
                return this.position.ToString();
            }
            else
            {
                return "Skip";
            }
        }
        public Gender Gender {get;set;}
    }
}