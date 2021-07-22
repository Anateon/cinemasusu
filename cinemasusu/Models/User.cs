using System;
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

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
