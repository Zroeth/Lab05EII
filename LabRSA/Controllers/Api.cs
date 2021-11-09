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
        [HttpGet("keys/{p}/{q}")]
        public async Task<IActionResult> generarLasLlaves(int p, int q)
        {
            string Privado = $"./keys/private.key";
            string Publica = $"./keys/public.key";
            string Zip = $"./keys/llaves.zip";
           
           
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

                using (var file = ZipFile.Open(Zip, ZipArchiveMode.Create))
                {
                    file.CreateEntryFromFile(Publica, Path.GetFileName(Publica));
                    file.CreateEntryFromFile(Privado, Path.GetFileName(Privado));
                }

                System.IO.File.Delete(Publica);
                System.IO.File.Delete(Privado);

                return StatusCode(200);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("{nombre}")]
        [RequestSizeLimit(40971520)]
        public async Task<IActionResult> cifrarDescifrar([FromForm] IFormFile file, [FromForm] IFormFile key, string nombre)
        {
            Cifrado Cifrado = new Cifrado();
            string ruta = @"\\Upload\\";
            string[] separacion = (key.FileName).Split('.');
            string fSep = separacion[0];

            try
            {
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

                if (fSep == "Llave publica")
                {
                    FileStream nuevo = new FileStream(@".\" + nombre + ".txt", FileMode.Create, FileAccess.ReadWrite);
                    string[] info = linea.Split(',');
                    int n = Convert.ToInt32(info[0]);
                    int e = Convert.ToInt32(info[1]);
                    List<byte> bytes = Cifrado.RSACifrado(archivo, n, e);
                    nuevo.Write(bytes.ToArray());
                    nuevo.Close();
                    System.IO.File.Delete(ruta + file.FileName);
                    System.IO.File.Delete(ruta + key.FileName);
                }
                else if (fSep == "Llave privada")
                {
                    FileStream nuevo = new FileStream(@".\" + nombre + ".txt", FileMode.Create, FileAccess.ReadWrite);
                    string[] data = linea.Split(',');
                    int n = Convert.ToInt32(data[0]);
                    int d = Convert.ToInt32(data[1]);
                    List<byte> bytes = Cifrado.descifrar(archivo, n, d);
                    nuevo.Write(bytes.ToArray());
                    nuevo.Close();
                    System.IO.File.Delete(ruta + file.FileName);
                    System.IO.File.Delete(ruta + key.FileName);
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

