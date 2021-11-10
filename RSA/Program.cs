using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RSA
{
    public class Program
    {
        static void Main(string[] args)
        {

            int n = 0;
            int e = 0;
            int d = 0;
            int cont = 0;

            Cifrado Cifrar = new Cifrado();
            FileStream filestream = new FileStream(@"e:\Cathy\Downloads\decifrado rsa\Lab05EII-RSA\LabRSA\wwwroot\Upload\Cifrado.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            List<string> llaves = Cifrar.generarLlave(2, 3);


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

            FileStream writer = new FileStream(@"e:\Cathy\Downloads\decifrado rsa\Lab05EII-RSA\LabRSA\wwwroot\Upload\descifrado.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            List<byte> descifrar = Cifrar.descifrar(filestream, n, d);
            writer.Write(descifrar.ToArray());
            writer.Close();
        }
    }
}
