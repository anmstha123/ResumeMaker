using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Resume.Models;

namespace Resume.Services.PdfService
{
    public class PdfService: IPdfService
    {
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }
        public FileStreamResult GetPDF()
        {
            var resumeData = GetResumeData(); // You should define your resume data

            Document document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);

                    // Header
                    page.Header()
                        .Text(resumeData.FullName)
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken4);

                    // Content
                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(row =>
                        {
                            row.Item().Column(column =>
                            {
                                // Left column (Personal Information)
                                column.Item().Text("Personal Information").Bold().FontSize(18);
                                column.Item().Text($"Email: {resumeData.Email}");
                                column.Item().Text($"Phone: {resumeData.Phone}");
                                column.Item().Text($"Address: {resumeData.Address}");
                            });


                            row.Item().Column(column =>
                            {
                                // Right column (Experience, Education, etc.)
                                column.Item().Text("Experience").Bold().FontSize(18);
                                foreach (var experience in resumeData.Experiences)
                                {
                                    column.Item().Text($"{experience.Title} at {experience.Company}, {experience.Year}");
                                    column.Item().Text($"{experience.Description}");
                                }

                                column.Item().Text("Education").Bold().FontSize(18);
                                foreach (var education in resumeData.Education)
                                {
                                    column.Item().Text($"{education.Degree} in {education.FieldOfStudy}, {education.Year}");
                                    column.Item().Text($"{education.Institution}");
                                }

                                // Add more sections as needed
                            });
                        });

                    // Footer
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });

            byte[] pdfBytes = document.GeneratePdf();
            MemoryStream ms = new MemoryStream(pdfBytes);

            // Set the FileDownloadName to change the name of the downloaded file
            return new FileStreamResult(ms, "application/pdf");

        }

        private ResumeData GetResumeData()
        {
            // Replace this with your actual data
            return new ResumeData
            {
                FullName = "Anmol Shrestha",
                Email = "anmstha123@gmail.com",
                Phone = "+1234567890",
                Address = "123 Main Street, City, Country",
                Experiences = new[]
                {
                    new Experience { Title = "Software Engineer", Company = "ABC Inc", Year = "2018-2022", Description = "Developed web applications using ASP.NET Core." },
                    // Add more experiences
                },
                Education = new[]
                {
                    new Education { Degree = "Bachelor of Science", FieldOfStudy = "Computer Science", Institution = "XYZ University", Year = "2014-2018" },
                    // Add more education details
                }
            };
        }
    }
}
