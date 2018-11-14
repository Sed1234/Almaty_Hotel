using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmatyHotel.Core.Models.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        public ApplicationUser(int id,string firstName, string email, string password) 
            : this()
        {
            Id = id;
            FirstName = firstName;
            Email = email;
            Password = password;
        }

        public ApplicationUser()
        {
            Bookings = new List<Booking>();
        }
    }
}
