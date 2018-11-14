using System;
using AlmatyHotel.Core.Models.Entities;
using AlmatyHotel.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlmatyHotel.Tests
{
    [TestClass]
    public class HotelDbContextTests
    {
        [TestMethod]
        public void ShouldCreateDatabase()
        {
            HotelDbContext db = new HotelDbContext();
            db.ApplicationUsers.Add(new ApplicationUser(2,"Sergey", "sergey@gmail.com", "12345"));
            db.SaveChanges();

        }
    }
}
