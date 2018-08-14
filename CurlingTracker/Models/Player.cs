using System;
using System.ComponentModel.DataAnnotations;
namespace CurlingTracker.Models
{
    public class Player 
    {


        public Guid PlayerId {get; set;}

        public string FirstName {get;set;}
       
        public string LastName {get;set;}

        public string Image {get;set;}
        
        
        public Position position {get;set;}

        public bool IsSkip {get;set;}
        
        public Gender Gender {get;set;}

        public enum Position{Lead = 1, Second = 2, Third = 3, Fourth = 4}

        
        public Player(string firstName, string lastName, Gender gender, Position position, bool isSkip, string image = "default.png")
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Gender = gender;
            this.position = position;
            if (this.position == 0)
            {
                throw new Exception("Invalid Position");
            }
            this.Image = image;
            this.IsSkip = isSkip;
        }

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


        public void SetRandomImage()
        {
            Random random = new Random();
            this.Image = "random" + random.Next(1,7) + ".jpeg";
        }
    }
}