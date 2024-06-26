﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TS.FileMimeTypeControl;

namespace DosyaGüvenlikKontrolu.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpPost]
    public IActionResult SaveFile([FromForm] IFormFile file)
    {
        //137,80,78,71 => png
        //255,216,255 => jpg
        //70, 90, 144=> exe


        bool checkFile1= file.CheckForJpg();
        bool checkFile2 = file.CheckForPng();

        //using(var stream = new MemoryStream())
        //{
        //    file.CopyTo(stream);
        //    byte[] fileBytes =stream.ToArray();
        //}

        //jpg ve png formatlarını bekliyorum!

        return NoContent(); //14:15 görüşelim
    }
}


//public static class Extensions
//{
//    public static bool CheckFileForJpg(this IFormFile file)
//    {
//        using (var stream = new MemoryStream())
//        {
//            file.CopyTo(stream);
//            byte[] fileBytes = stream.ToArray();
//            string jpgValue = fileBytes[0].ToString() + fileBytes[1].ToString() + fileBytes[2].ToString();


//            if(jpgValue != "255216255")
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//}