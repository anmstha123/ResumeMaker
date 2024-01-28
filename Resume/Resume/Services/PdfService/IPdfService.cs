using Microsoft.AspNetCore.Mvc;

namespace Resume.Services.PdfService
{
    public interface IPdfService
    {
        public FileStreamResult GetPDF();
    }
}
