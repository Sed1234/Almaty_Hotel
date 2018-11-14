using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmatyHotel.Core.Models.Entities
{
    public enum RoomType
    {
        Standard, Deluxe, Luxe, Vip
    }

    public class Room : BaseEntity
    {
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        public decimal BaseRoomRate { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual RoomDetail RoomDetail { get; set; }

        public Room(int roomNumber, RoomType roomType, decimal baseRoomRate) : this()
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            BaseRoomRate = baseRoomRate;
        }

        public Room()
        {
            Bookings = new List<Booking>();
        }
    }

    public class RoomDetail : BaseEntity
    {
        public virtual Room Room { get; set; }
        public string PathToImage { get; set; }
        public string RoomDescription { get; set; }

        public RoomDetail(string pathToImage, string roomDescription)
        {
            PathToImage = pathToImage;
            RoomDescription = roomDescription;
        }
    }

    public class Booking : BaseEntity 
    {
        public int OwnerUserId { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal BookingTotalAmount { get; set; }

        public Booking(int userId, int roomId, 
            DateTime startDate, DateTime finishDate, 
            decimal bookingTotalAmount)
        {
            OwnerUserId = userId;
            RoomId = roomId;
            StartDate = startDate;
            FinishDate = finishDate;
            BookingDate = DateTime.UtcNow;
            BookingTotalAmount = bookingTotalAmount;
        }
    }
}
