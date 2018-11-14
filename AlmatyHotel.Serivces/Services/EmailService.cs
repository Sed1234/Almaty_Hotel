using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using AlmatyHotel.Infrastructure;
using AlmatyHotel.Core.Models.Entities;
using TemplateEngine.Docx;
using System.IO;

namespace AlmatyHotel.Core.Services
{
   public class EmailService
    {
		private readonly HotelDbContext _dbContext;
		private readonly EmailService _emailService;
		private readonly DocumentService _documentService;
		
		public string GetEmailAddress(int UserId)
		{
			var email = _dbContext.ApplicationUsers.Where(p => p.Id == UserId);
			string email_user=email.Select(p => p.Email).ToString();
			try
			{
				return email_user;
			}
			catch (Exception ex)
			{
				throw ex;
			}			
		}

		public string GetUser(int UserId)
		{
			var name = _dbContext.ApplicationUsers.Find(UserId).FirstName;
            return name;
		}

		public void EmailSend(DateTime date, List<Booking> bookings)
		{
			var parentFolder = Directory.GetParent(Directory
			   .GetParent(Directory
				   .GetParent(Environment.CurrentDirectory).FullName).FullName);

			foreach (var item in bookings)
			{
				if ((item.BookingDate.Day - date.Day) <= 3)
				{
					MailAddress from = new MailAddress("km.sedssm@yandex.ru", "Hotel");
					MailAddress to = new MailAddress(GetEmailAddress(item.Id));
					MailMessage m = new MailMessage(from, to);
					m.Subject = "Booking Reminder";
                    string test = parentFolder + $@"\Invoices\{GetUser(item.OwnerUserId)}.docx";
					m.Attachments.Add(new Attachment(test));
					// адрес smtp-сервера и порт, с которого будем отправлять письмо
					SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);
					// логин и пароль
					smtp.Credentials = new NetworkCredential("km.sedssm@yandex.ru", "3406110");
					smtp.EnableSsl = true;
					smtp.Send(m);
				}
			}
		}
        public EmailService(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
	}
}
