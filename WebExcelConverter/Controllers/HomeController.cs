using Microsoft.AspNetCore.Mvc;
using ExcelToJsonWeb.Helpers;
using WebExcelConverter.Models;
using WebExcelConverter.Services;

namespace WebExcelConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExcelService _excelService;

        public HomeController(ExcelService excelService) 
        {
            _excelService = excelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadFile(UploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                ViewBag.Message = Constants.FileNotSelected;
                return View("Index");
            }

            if (Path.GetExtension(model.File.FileName).ToLower() != Constants.ExcelFileExtension)
            {
                ViewBag.Message = Constants.InvalidFileType;
                return View("Index");
            }

            try
            {
                string outputFilePath;
                

                if (model.OutputFormat == "xml")
                {
                    outputFilePath = _excelService.ConvertExcelToXml(
                        model.File.OpenReadStream(),
                        model.File.FileName
                    );
                    ViewBag.Message = "Konverze do XML úspěšná!";
                }
                else
                {
                    outputFilePath = _excelService.ConvertExcelToJson(
                        model.File.OpenReadStream(),
                        model.File.FileName
                    );
                    ViewBag.Message = Constants.ConversionSuccess;
                }

               
                ViewBag.OutputFilePath = Path.GetFileName(outputFilePath);   
                return View("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.Message = $"{Constants.ErrorPrefix} {ex.Message}";
                return View("Index");
            }
        }

        public IActionResult DownloadFile(string fileName)
        {
            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            if (System.IO.File.Exists(filePath))
            {
               
                string contentType = fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)
                    ? "application/xml"
                    : "application/json";

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, contentType, fileName);
            }

            return NotFound();
        }
    }
}