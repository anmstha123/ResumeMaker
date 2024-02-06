// Resume/Controllers/PdfController.cs

using Microsoft.AspNetCore.Mvc;
using Resume.Models;
using Resume.Services.PdfService;

namespace Resume.Controllers
{
    public class PdfController : Controller
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public IActionResult DownloadPdf([FromBody] ResumeData resumeData)
        {
            var pdfResult = _pdfService.GetPDF(resumeData);
            return pdfResult;
        }
    }
}
