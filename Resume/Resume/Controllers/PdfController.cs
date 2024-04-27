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
            var val = new ResumeData();
            return View(val);
        }

        //[HttpPost]
        public IActionResult DownloadPdf([FromBody] ResumeData resumeData)
        {
            var pdfResult = _pdfService.GetPDF(resumeData);
            return pdfResult;
        }

        [HttpGet]
        public IActionResult AddEducationField()
        {
            var educationField = new Education();

            return PartialView("_EducationFieldPartial", educationField);
        }

        [HttpGet]
        public IActionResult AddExperienceField()
        {
            var experienceField = new Experience();

            return PartialView("_ExperienceFieldPartial", experienceField);
        }
    }
}
