using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Numerics;

namespace Pruebas
{
    class Cifrado
    {
        public List<byte> RSACifrado(FileStream archivo, int n, int e)
        {
            List<byte> lista = new List<byte>();
            var reader = new BinaryReader(archivo);
            var buffer = new byte[2000000];
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                buffer = reader.ReadBytes(2000000);
                foreach (var item in buffer)
                {
                    var ok = BigInteger.ModPow(item, (BigInteger)e, (BigInteger)n);
                    byte[] bytes = BitConverter.GetBytes((long)ok);
                    foreach (var b in bytes)
                    {
                        lista.Add(b);
                    }
                }
            }
            reader.Close();
            archivo.Close();

            return lista;
        }
        public List<string> generarLlaves(int p, int q)
        {
             GenerarLlave Llave  = new GenerarLlave();
            return Llave.generarLlave(p, q);
        }
    }
}
