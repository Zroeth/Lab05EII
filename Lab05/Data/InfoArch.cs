using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab05.Data
{
    public interface InfoArch<T>
    {

        IFormFile ArchivoCargado { get; set; }
        int Llave { get; set; }
    }
}
