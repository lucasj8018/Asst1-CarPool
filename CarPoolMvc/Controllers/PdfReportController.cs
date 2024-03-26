using CarPoolLibrary.Models;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CarPoolMvc.Controllers
{
    public class PdfReportController : Controller
    {
        private readonly ILogger<PdfReportController> _logger;
        private readonly CarPoolLibrary.Data.ApplicationDbContext _context;

        public PdfReportController(ILogger<PdfReportController> logger, CarPoolLibrary.Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            MemoryStream ms = new MemoryStream();

            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc, PageSize.A4, false);
            writer.SetCloseStream(false);

            Paragraph header = new Paragraph("The Manifest Report")
              .SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(20);

            document.Add(header);

            Paragraph subheader = new Paragraph(DateTime.Now.ToShortDateString())
              .SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(15);
            document.Add(subheader);

            // empty line
            document.Add(new Paragraph(""));

            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // empty line
            document.Add(new Paragraph(""));

            // Add table containing data
            document.Add(await GetPdfTable());

            // Page Numbers
            int n = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                  .Format("Page " + i + " of " + n)),
                  559, 806, i, TextAlignment.RIGHT,
                  VerticalAlignment.TOP, 0);
            }

            document.Close();
            byte[] byteInfo = ms.ToArray();
            ms.Write(byteInfo, 0, byteInfo.Length);
            ms.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");

            //Uncomment this to return the file as a download
            fileStreamResult.FileDownloadName = "ManifestReport.pdf";

            return fileStreamResult;
        }

        private async Task<Table> GetPdfTable()
        {
            float[] columnWidths = {1, 2, 3, 4, 5};
            Table table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();

            // Table
            // Table table = new Table(5, false);

            // Headings
            Cell cellManifestId = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Manifest ID"));

            Cell cellMemberId = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Member ID"));

            Cell cellDestination = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Destination"));

            Cell cellNotes = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Notes"));

            Cell cellEmail = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Member Email"));

            table.AddCell(cellManifestId);
            table.AddCell(cellMemberId);
            table.AddCell(cellDestination);
            table.AddCell(cellNotes);
            table.AddCell(cellEmail);

            cellManifestId.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellMemberId.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellDestination.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellNotes.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cellEmail.SetVerticalAlignment(VerticalAlignment.MIDDLE);


            Manifest[] manifests = await GetManifestsAsync();

            foreach (var item in manifests)
            {
                Cell cManifestId = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.ManifestId.ToString()));

                Cell cMemberId = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.MemberId.ToString()));

                Cell cDestinationAddress = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.DestinationAddress?.ToString() ?? "N/A"));

                Cell cNotes = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.Notes?.ToString()));

Cell cEmail = new Cell(1, 1)
    .SetTextAlignment(TextAlignment.CENTER)
    .Add(new Paragraph(item.Member?.Email ?? "No Email"));

                table.AddCell(cManifestId);
                table.AddCell(cMemberId);
                table.AddCell(cDestinationAddress);
                table.AddCell(cNotes);
                table.AddCell(cEmail);
            }

            return table;
        }

        private async Task<Manifest[]> GetManifestsAsync()
        {
            var manifest = await _context.Manifests!.ToArrayAsync();
            return manifest!;
        }
    }
}