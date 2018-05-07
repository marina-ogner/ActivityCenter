using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityProject.Models
{
    public class Activity : BaseEntity
    {
        public int ActivityId { get; set; }
        public string Title { get; set; }
        public DateTime? Datetime { get; set; }
        public int? Duration { get; set; }
        public string Description { get; set; }
        public int UserId {get; set; }
        public User User { get; set; }
        public string Address { get; set; }
        
        public List<Participant> Participants { get; set; }
        public Activity(){
            Participants = new List<Participant>();
        }

         public Boolean ParticipantExist(int UserId)
        {
            
                if (Participants.Exists(p => p.UserId == UserId))
                {
                    return true;
                }
            return false;
        }

        public int NumberParticipants(){
            return Participants.Count;
        }

        public string ConvertDuration(){
            if(Duration>60){
                return $"{(Duration/60)} hours";
            }
            if(Duration>24*60){
                return $"{(Duration/24*60)} days";
            }
            return $"{(Duration)} minutes";
        }

        public int TimeDifference(){
            DateTime Today = DateTime.Now;
            Double Difference = ((DateTime)(Datetime)).Subtract(Today).TotalMinutes;
            if(Difference<0){
                return -1;
            }
            return 1;
            // if(Difference>60){
            //     return $"{(int)Math.Floor(Difference/60)} hours";
            // }
            // if(Difference>24*60){
            //     return $"{(int)Math.Floor(Difference/24*60)} days";
            // }
            // return $"{(int)Math.Floor(Difference)} minutes";
        }



    }
}