using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            int e = 0;
            int d = 0;
            int cont = 0;

            Cifrado Cifrar = new Cifrado();
            FileStream filestream = new FileStream(@"..\\..\\Upload\\Cifrado.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            List<string> llaves = Cifrar.generarLlaves(25, 221);

            foreach (var item in llaves)
            {
                if (cont == 0)
                {
                    string[] splited = item.Split(',');
                    n = Convert.ToInt32(splited[0]);
                    e = Convert.ToInt32(splited[1]);
                    cont++;
                }
                else
                {
                    string[] splited = item.Split(',');
                    n = Convert.ToInt32(splited[0]);
                    d = Convert.ToInt32(splited[1]);
                }
            }

            FileStream writer = new FileStream(@"..\\..\\Upload\\Descifrado.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            Descifrar descifrado = new Descifrar();
            List<byte> descifrar = descifrado.descifrar(filestream, n, d);
            writer.Write(descifrar.ToArray());
            writer.Close();
        }
    }
}
