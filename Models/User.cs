using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityProject.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Participant> Activities { get; set; }
        public User(){
            Activities = new List<Participant>();
        }


        public string FirstLast(){
            return $"{FirstName} {LastName}";
        }

    }
}