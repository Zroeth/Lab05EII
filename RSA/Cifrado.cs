using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Numerics;

namespace RSA
{
    public class Cifrado : Interface1

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
                    var variables = BigInteger.ModPow(item, (BigInteger)e, (BigInteger)n);
                    byte[] bytes = BitConverter.GetBytes((long)variables);
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


        public List<byte> descifrar(FileStream archivo, int n, int d)
        {

            string mensaje = "";
            List<byte> arch = new List<byte>();
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
                        byte[] countby = new byte[bytes.Count];
                        foreach (var cobytes in bytes)
                        {
                            countby[contador] = cobytes;
                            contador++;
                        }
                        long num = BitConverter.ToInt32(countby, 0);
                        var variables = Convert.ToString(BigInteger.ModPow(num, (BigInteger)d, (BigInteger)n));
                        arch.Add((byte)int.Parse(variables));
                        bytes.Clear();
                        contador = 0;
                    }
                }
            }
            reader.Close();
            archivo.Close();

            return arch;
        }

        public List<string> generarLlave(int p, int q)
        {
            GenerarLlave Llave = new GenerarLlave();
            return Llave.generarLlave(p, q);
        }

       
       
        
    }
}
