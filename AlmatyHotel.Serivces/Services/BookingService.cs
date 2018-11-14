using AlmatyHotel.Core.Models.Entities;
using AlmatyHotel.Core.Services;
using AlmatyHotel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmatyHotel.Services
{
    public class BookingService
    {
        private readonly HotelDbContext _dbContext;
        private readonly PaymentService _paymentService;
        private readonly EmailService _emailService;
        private readonly DocumentService _documentService;
        private readonly SmsService _smsService;

        public void BookRoomWorkflow(
            int userId, int roomId, 
            DateTime startDate, DateTime finishDate, 
            string cardNumber, string cardCvCode, string phoneNumber)
        {
            var user = _dbContext.ApplicationUsers.Find(userId);
            var room = _dbContext.Rooms.Find(roomId);

            if (user == null || room == null)
                throw new Exception("No required details found for this workflow!");

            if(CheckRoomAvailability(room, startDate, finishDate))
            {
                var totalAmount = CalculateTotalAmountForBooking(room, startDate, finishDate);
                var booking = new Booking(userId, roomId, startDate, finishDate, totalAmount);

                try
                {
                    _paymentService.MakePayment(userId, totalAmount, cardNumber, cardCvCode);
                    _documentService.GenerateInvoiceLetter(booking);

                    using (DbContextTransaction transactionScoped = _dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            _dbContext.Bookings.Add(booking);
                            _dbContext.SaveChanges();
                            transactionScoped.Commit();
                        }
                        catch (Exception ex)
                        {
                            transactionScoped.Rollback();
                            throw ex;
                        }

                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }

            }
            
            

        }

        public decimal CalculateTotalAmountForBooking(Room room, 
            DateTime startDate, DateTime finishDate)
        {
            return (finishDate - startDate).Days * room.BaseRoomRate;
        }


        public bool CheckRoomAvailability(Room room, 
            DateTime startDate, DateTime finishDate)
        {
            var availability = room.Bookings
                .Any(p => p.StartDate >= startDate 
                    && p.FinishDate <= finishDate);

            return !availability;
        }

        public BookingService(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
