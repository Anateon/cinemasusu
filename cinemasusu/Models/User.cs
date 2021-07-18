﻿using System;
using System.Collections.Generic;

#nullable disable

namespace cinemasusu.Models
{
    public partial class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}