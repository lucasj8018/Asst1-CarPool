using CarPoolLibrary.Data;
using CarPoolLibrary.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPoolMvc.Controllers
{
    public class PdfReportController : Controller
    {
        private readonly ILogger<PdfReportController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PdfReportController(ILogger<PdfReportController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            MemoryStream ms = new MemoryStream();

            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc, PageSize.A4.Rotate(), false);
            writer.SetCloseStream(false);

            ImageData imageData = ImageDataFactory.Create("wwwroot/images/logo.png");
            Image logo = new Image(imageData).SetWidth(80).SetFixedPosition(36, PageSize.A4.Rotate().GetTop() - 103);
            document.Add(logo);

            Paragraph banner = new Paragraph("The Manifest Report")
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20)
                .SetMarginTop(20)
                .SetPadding(10);
            document.Add(banner);

            Paragraph subheader = new Paragraph(DateTime.Now.ToShortDateString())
              .SetTextAlignment(TextAlignment.RIGHT)
              .SetFontSize(15);
            document.Add(subheader);

            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // empty line
            document.Add(new Paragraph(""));

            document.Add(await GetPdfTable());

            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                document.ShowTextAligned(new Paragraph(String.Format("Page " + i + " of " + pdfDoc.GetNumberOfPages())),
                    559, 806, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
            }

            document.Close();
            byte[] byteInfo = ms.ToArray();
            ms.Write(byteInfo, 0, byteInfo.Length);
            ms.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");

            fileStreamResult.FileDownloadName = "ManifestReport.pdf";

            return fileStreamResult;
        }

        private async Task<Table> GetPdfTable()
        {
            PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            float cellHeight = 30;

            float[] columnWidths = { 2, 4, 4, 4, 3, 4 };
            Table table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();

            // Headings
            Cell cellDriver = new Cell(1, 1)
               .SetHeight(cellHeight)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Driver").SetFont(fontBold));

            Cell cellDestination = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Dest.").SetFont(fontBold));

            Cell cellMeetingAddress = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Meeting Address").SetFont(fontBold));

            Cell cellDateTime = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Date & Time").SetFont(fontBold));

            Cell cellPassengers = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Passengers").SetFont(fontBold));

            Cell cellNotes = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Notes").SetFont(fontBold));

            table.AddCell(cellDriver);
            table.AddCell(cellDestination);
            table.AddCell(cellMeetingAddress);
            table.AddCell(cellDateTime);
            table.AddCell(cellPassengers);
            table.AddCell(cellNotes);

            cellDriver.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellDestination.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellMeetingAddress.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellDateTime.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellPassengers.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellNotes.SetVerticalAlignment(VerticalAlignment.MIDDLE);

            Manifest[] manifests = await GetManifestsAsync();

            foreach (var item in manifests)
            {
                var name = (item.Member?.FirstName ?? "") + " " + (item.Member?.LastName ?? "");
                var dateandtime = (item.Trip?.Date != null && item.Trip?.Time != null) ? $"{item.Trip.Date} {item.Trip.Time}" : "N/A";
                var destinationAddress = item.Trip?.Destination ?? "N/A";
                var meetingAddress = item.Trip?.MeetingAddress ?? "N/A";
                var passengers = item.Trip?.Members?
                    .Select(m => $"{m.FirstName} {m.LastName}")
                    .ToList() ?? new List<string>();
                var passengersText = passengers.Any() ? string.Join("\n ", passengers) : "No passengers";
                var notes = item.Notes ?? "N/A";

                Cell cDriver = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(name));
                Cell cDestination = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(destinationAddress));
                Cell cMeetingAddress = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(meetingAddress));
                Cell cDateTime = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(dateandtime));
                Cell cPassengers = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(passengersText));
                Cell cNotes = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(notes));

                table.AddCell(cDriver);
                table.AddCell(cDestination);
                table.AddCell(cMeetingAddress);
                table.AddCell(cDateTime);
                table.AddCell(cPassengers);
                table.AddCell(cNotes);

                cDriver.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                cDestination.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                cMeetingAddress.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                cDateTime.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                cPassengers.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                cNotes.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            }

            return table;
        }

        private async Task<Manifest[]> GetManifestsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return new Manifest[0];
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAdmin)
            {
                return await _context.Manifests!
                    .Include(m => m.Member)
                    .Include(m => m.Trip)
                    .ThenInclude(t => t!.Members)
                    .ToArrayAsync();
            }
            else
            {
                var userEmail = user.Email;
                var tripsAsPassenger = _context.Trips?
                    .Include(t => t.Members)
                    .Where(t => t.Members!.Any(m => m.Email == userEmail))
                    .Select(t => t.TripId)
                    .ToList();

                return await _context.Manifests!
                    .Include(m => m.Member)
                    .Include(m => m.Trip)
                    .ThenInclude(t => t!.Members)
                    .Where(m => m.Member!.Email == userEmail || tripsAsPassenger!.Contains(m.Trip!.TripId))
                    .ToArrayAsync();
            }
        }
    }
}