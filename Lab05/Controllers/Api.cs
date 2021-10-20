using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab05.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdes;

namespace Lab05.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Api : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public Api(IWebHostEnvironment environment)
        {
            _environment = environment;

        }
        public class FileUploadAPI
        {
            public IFormFile file { get; set; }

        }

        [HttpPost("cipher/sdes")]
        public IActionResult postSdes([FromForm] Información objFile)
        {
            try
            {
                if (objFile.file.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.file.FileName))
                    {
                        objFile.file.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        string s = @_environment.WebRootPath;
                        SdesOp.Cifrar(fileStream.Name, s, objFile.Llave);

                        MemoryStream enviar = new MemoryStream(System.IO.File.ReadAllBytes(_environment.WebRootPath +
                           "\\Upload\\" + Path.GetFileNameWithoutExtension(objFile.file.FileName) + ".sdes"));
                        return File(enviar, "text/plain", Path.GetFileNameWithoutExtension(objFile.file.FileName) + ".sdes");
                    }
                }
                else
                {
                    return StatusCode(500);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { name = "Internal Server Error", message = ex.Message });
            }


        }

        [HttpPost("decipher/sdes")]
        public IActionResult postDecipherZZ([FromForm] Información objFile)
        {
            try
            {
                if (objFile.file.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.file.FileName))
                    {
                        objFile.file.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        string s = @_environment.WebRootPath;
                        SdesOp.Descifrar(fileStream.Name, s, objFile.Llave);

                        MemoryStream enviar = new MemoryStream(System.IO.File.ReadAllBytes(_environment.WebRootPath +
                           "\\Upload\\" + Path.GetFileNameWithoutExtension(objFile.file.FileName) + ".txt"));
                        return File(enviar, "text/plain", Path.GetFileNameWithoutExtension(objFile.file.FileName) + ".txt");
                    }
                }
                else
                {
                    return StatusCode(500);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { name = "Internal Server Error", message = ex.Message });
            }
        }



    }
}
