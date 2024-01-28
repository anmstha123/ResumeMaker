// Resume/Controllers/PdfController.cs

using Microsoft.AspNetCore.Mvc;
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

        public IActionResult DownloadPdf()
        {
            var pdfResult = _pdfService.GetPDF();
            return pdfResult;
        }
    }
}
