﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensariumDesktop.Model.Components
{
    public class Student : User
    {
        public Student(User u)
        {
            this.AccountType = u.AccountType;
            this.ActiveAccount = u.ActiveAccount;
            this.Birthday = u.Birthday;
            this.Email = u.Email;
            this.FirstName = u.FirstName;
            this.PhoneNumber = u.PhoneNumber;
            this.ProfilePicture = u.ProfilePicture;
            this.RegistrationDate = u.RegistrationDate;
            this.UserID = u.UserID;
            this.Username = u.Username;
            this.UserPrivilegeses = u.UserPrivilegeses;
            this.LastName = u.LastName;
        }
        public string Index { get; set; }
        public DateTime ValidUntil { get; set; }
        public Faculty Faculty { get; set; }
        public int BreakfastCount { get; set; }
        public int LunchCount { get; set; }
        public int DinnerCount { get; set; }
    }
}
