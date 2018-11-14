using AlmatyHotel.Core.Models.Entities;
using AlmatyHotel.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmatyHotel.Tests
{
    [TestClass]
    public class DocumentServiceTests
    {
        [TestMethod]
        public void ShouldCreateInvoiceLetter()
        {
            DocumentService service = new DocumentService();

            Booking record = new Booking(10, 23, new DateTime(2018, 12, 10), new DateTime(2018, 12, 17), 70000);
            record.OwnerUser = new ApplicationUser(1,"Test", "test@gmail.com", "123456");
            record.Room = new Room(23, RoomType.Luxe, 1000);

            service.GenerateInvoiceLetter(record);
        }
    }
}
