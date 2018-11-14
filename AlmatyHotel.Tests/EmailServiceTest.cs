using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlmatyHotel.Core.Models.Entities;
using AlmatyHotel.Core.Services;
using AlmatyHotel.Infrastructure;

namespace AlmatyHotel.Tests
{
	[TestClass]
	public class EmailServiceTest
	{

		[TestMethod]
		public void ShouldSendEmailMessage()
		{
            HotelDbContext dbContext = new HotelDbContext();
            EmailService email = new EmailService(dbContext);
			DocumentService service = new DocumentService();
			List<Booking> bookings = new List<Booking>();

			Booking book = new Booking(41, 23, new DateTime(2018, 11, 16), new DateTime(2018, 11, 24), 70000);
			book.OwnerUser = new ApplicationUser(32,"Test1", "sed_ssm@mail.ru", "123456");
			book.Room = new Room(23, RoomType.Luxe, 1000);
			bookings.Add(book);
            dbContext.Bookings.Add(book);
            dbContext.ApplicationUsers.Add(book.OwnerUser);
            dbContext.SaveChanges();

            Booking book1 = new Booking(42, 33, new DateTime(2018, 12, 10), new DateTime(2018, 12, 17), 70000);
			book1.OwnerUser = new ApplicationUser(33,"Test2", "km.sedssm@gmail.com", "123456");
			book1.Room  = new Room(33, RoomType.Luxe, 1000);
			bookings.Add(book1);
            dbContext.Bookings.Add(book1);
            dbContext.ApplicationUsers.Add(book1.OwnerUser);
            dbContext.SaveChanges();

            Booking book2 = new Booking(43, 43, new DateTime(2018, 12, 10), new DateTime(2018, 12, 17), 70000);
			book2.OwnerUser=new ApplicationUser(34,"Test3", "km.sedssm@gmail.com", "123456");
			book2.Room= new Room(43, RoomType.Luxe, 1000);
			bookings.Add(book2);
            dbContext.Bookings.Add(book2);
            dbContext.ApplicationUsers.Add(book2.OwnerUser);
            dbContext.SaveChanges();
            foreach (Booking item in bookings)
			{
				service.GenerateInvoiceLetter(item);
                
            }

            email.EmailSend(DateTime.Now, bookings);


        }
	}
}

