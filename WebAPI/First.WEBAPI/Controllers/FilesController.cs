using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace First.WEBAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class FilesController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromForm] List<IFormFile> files)
    {
        //cloud
        //ftp
        //byte[]
        //backend

        #region Byte array çevirme
        foreach (var file in files)
        {
            var fileByte = FileService.FileConvertByteArrayToDatabase(file);
            string fileName2 = FileService.FileSaveToServer(file,"wwwroot/");
            FileSaveToFtpModel model = new("ftepadres", "username", "password");
            string fileName3 = FileService.FileSaveToFtp(file, model);

            FileService.FileDeleteToServer("wwwroot/asdasd.jpg");
            FileService.FileDeleteToFtp("wwwroot/asdasd.jpg", model);

            using (var memorySteam = new MemoryStream())
            {
                file.CopyTo(memorySteam);
                var fileBytes = memorySteam.ToArray();
                string fileString = Convert.ToBase64String(fileBytes);
            }
        }
        #endregion

        #region Dosyaya Kaydetme
        foreach (var file in files)
        {
            //string fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            //string fileName = Guid.NewGuid().ToString() + fileFormat;
            string fileName = DateTime.Now.Ticks + "-" + file.FileName;


            using (var stream = System.IO.File.Create("wwwroot/" + fileName))
            {
                file.CopyTo(stream);
            }
        }
        #endregion
        return NoContent();
    }
}
