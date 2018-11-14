using AlmatyHotel.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Docx;

namespace AlmatyHotel.Core.Services
{
    public class DocumentService
    {
        public FileInfo GenerateInvoiceLetter(Booking booking)
        {
            var parentFolder = Directory.GetParent(Directory
                .GetParent(Directory
                    .GetParent(Environment.CurrentDirectory).FullName).FullName);

            FileInfo templateInvoice = new 
                FileInfo(parentFolder + @"\Templates\InvoiceLetterTemplate.docx");

            string outputFile = parentFolder + $@"\Invoices\{booking.OwnerUser.FirstName}.docx";
            File.Copy(templateInvoice.FullName, outputFile);

            var valuesToFill = new Content(
                new FieldContent("ClientName", booking.OwnerUser.FirstName),
                new FieldContent("RoomNumber", booking.Room.RoomNumber.ToString()),
                new FieldContent("DateStart", booking.StartDate.ToShortDateString()),
                new FieldContent("DateFinish", booking.FinishDate.ToShortDateString()));

            using (var outputDocument = new TemplateProcessor(outputFile)
                .SetRemoveContentControls(true))
            {
                outputDocument.FillContent(valuesToFill);
                outputDocument.SaveChanges();
            }

            return new FileInfo(outputFile);
        }
    }
}
