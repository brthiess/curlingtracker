using System;

namespace CurlingTracker.Models
{
    public class Player 
    {
        public Player(string firstName, string lastName)
        {
            
        }


        public Guid PlayerId {get; set;}

        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string GetFullName() 
        {
            return FirstName + " " + LastName;
        } 
        
        public enum Gender {MEN, WOMEN}
      
    }
}