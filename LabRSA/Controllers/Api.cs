using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSA;
using System.IO.Compression;

namespace LabRSA.Controllers
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
        [HttpGet("keys")]
        public async Task<IActionResult> GenerarLasLlaves(int p, int q)
        {
            string Privado = $"./keys/private.key";
            string Publica = $"./keys/public.key";
            string Zip = $"./keys/llaves.zip";

            string Zip2 = $"./keys/Llaves.zip";

            FileStream llavePublica = new FileStream(Publica, FileMode.OpenOrCreate, FileAccess.Write);
            FileStream llavePrivada = new FileStream(Privado, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter KeyPublica = new StreamWriter(llavePublica);
            StreamWriter KeyPrivada = new StreamWriter(llavePrivada);


            try
            {
                Cifrado llaves = new Cifrado();
                List<string> keys = llaves.generarLlave(p, q);
                KeyPublica.Write(keys[0]);
                KeyPublica.Close();
                KeyPrivada.Write(keys[1]);
                KeyPrivada.Close();
                System.IO.File.Delete(Zip2);

                using (var file = ZipFile.Open(Zip2, ZipArchiveMode.Create))
                {
                    file.CreateEntryFromFile(Publica, Path.GetFileName(Publica));
                    file.CreateEntryFromFile(Privado, Path.GetFileName(Privado));

                    file.Dispose();
                    MemoryStream enviar = new MemoryStream(System.IO.File.ReadAllBytes($"./keys/Llaves.zip"));

                    return File(enviar, "text/plain", Path.GetFileNameWithoutExtension(Zip) + ".zip");

                }


                System.IO.File.Delete(Publica);
                System.IO.File.Delete(Privado);

            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("rsa")]
        [RequestSizeLimit(40971520)]
        public async Task<IActionResult> cifrarDescifrar([FromForm] IFormFile file, [FromForm] IFormFile key, [FromForm] string nombre)
        {
            Cifrado Cifrado = new Cifrado();
            string ruta = @"wwwroot\Upload\";
            string[] separacion = (key.FileName).Split('.');
            string fSep = separacion[0];


            try
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                }
                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + file.FileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    fileStream.Close();
                }


                using (var filestream = new FileStream((ruta + file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    filestream.Close();
                }

                using (var filestream2 = new FileStream((ruta + key.FileName), FileMode.Create))
                {
                    await key.CopyToAsync(filestream2);
                    filestream2.Close();
                }

                string rutaArchivo = ruta + file.FileName;
                FileStream archivo = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                string linea = System.IO.File.ReadAllText(ruta + key.FileName, System.Text.Encoding.Default);

                if (fSep == "public")
                {
                    FileStream nuevo = new FileStream(_environment.WebRootPath + "\\Upload\\" + nombre + ".txt", FileMode.Create, FileAccess.ReadWrite);
                    string[] info = linea.Split(',');
                    int n = Convert.ToInt32(info[0]);
                    int e = Convert.ToInt32(info[1]);
                    List<byte> bytes = Cifrado.RSACifrado(archivo, n, e);
                    nuevo.Write(bytes.ToArray());
                    nuevo.Close();
                    MemoryStream enviar = new MemoryStream(System.IO.File.ReadAllBytes(_environment.WebRootPath + @"\Upload\"+
                             nombre + ".txt"));
                    return File(enviar, "text/plain",nombre + ".txt");
                }
                else if (fSep == "private")
                {
                    FileStream nuevo = new FileStream(_environment.WebRootPath + "\\Upload\\" + nombre + ".txt", FileMode.Create, FileAccess.ReadWrite);
                    string[] data = linea.Split(',');
                    int n = Convert.ToInt32(data[0]);
                    int d = Convert.ToInt32(data[1]);
                    List<byte> bytes = Cifrado.descifrar(archivo, n, d);
                    nuevo.Write(bytes.ToArray());
                    nuevo.Close();

                    MemoryStream enviar = new MemoryStream(System.IO.File.ReadAllBytes(_environment.WebRootPath + @"\Upload\" +
                             nombre + ".txt"));
                    return File(enviar, "text/plain", nombre + ".txt");
                }
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }


    }
}