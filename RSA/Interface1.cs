using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RSA
{
    interface Interface1
    {
        List<string> generarLlave(int p, int q);
        List<byte> RSACifrado(FileStream archivo, int n, int e);
        List<byte> descifrar(FileStream archivo, int n, int d);
    }
}
