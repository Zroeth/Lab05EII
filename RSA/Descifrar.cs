using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Numerics;

namespace Pruebas
{
    class Descifrar
    {
        public List<byte> descifrar(FileStream archivo, int n, int d)
        {
            string mjsRecibido = "";
            List<byte> bpb = new List<byte>();
            int contador = 0;
            var reader = new BinaryReader(archivo);
            var buffer = new byte[2000000];
            List<byte> bytes = new List<byte>();
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                buffer = reader.ReadBytes(2000000);
                foreach (var item in buffer)
                {
                    bytes.Add(item);
                    if (bytes.Count == 8)
                    {
                        byte[] by = new byte[bytes.Count];
                        foreach (var bytee in bytes)
                        {
                            by[contador] = bytee;
                            contador++;
                        }
                        long num = BitConverter.ToInt32(by, 0);
                        var ok = BigInteger.ModPow(num, (BigInteger)d, (BigInteger)n);
                        bpb.Add((byte)ok);
                        bytes.Clear();
                        contador = 0;
                    }
                }
            }
            reader.Close();
            archivo.Close();

            return bpb;
        }
       
    }
}

