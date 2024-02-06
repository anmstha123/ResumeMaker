using Microsoft.AspNetCore.Mvc;
using Resume.Models;

namespace Resume.Services.PdfService
{
    public interface IPdfService
    {
        public FileStreamResult GetPDF(ResumeData resumeData);
    }
}
