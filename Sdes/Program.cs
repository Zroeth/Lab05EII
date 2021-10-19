using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Sdes
{
    class Program
    {
        static void Main(string[] args)
        {
          

            string leer = File.ReadAllText("wii.txt",Encoding.UTF8);

            char[] charUwu=leer.ToCharArray();
            string[] binarios = new string[charUwu.Length];
            for (int i = 0; i < charUwu.Length; i++)
            {
                binarios[i] = Convert.ToString(charUwu[i], 2).PadLeft(8, '0').PadRight(0,'0').Substring(0, 8);
              //  Console.OutputEncoding = System.Text.Encoding.UTF8;
              //  Console.WriteLine(binarios[i]);
            }
           
            using (TextWriter escribir = File.CreateText("TextoBinario.txt"))
            {

                foreach (string i in binarios)
                {
                    escribir.Write(i + "|");
                }
            }

            Console.WriteLine("Se creo el archivo con el equivalente de los valores en binario");

            Console.WriteLine("Escribir llave");
            int llaveInt = int.Parse(Console.ReadLine());

            if(llaveInt>256)
            {
                //son tontos
                Console.WriteLine("El valor de la llave no debe ser mayor a 256");
                Environment.Exit(0);
            }

            string llave = Convert.ToString(llaveInt, 2).PadLeft(10, '0');
            string llave1="";
            string llave2="";
            if (esBinario(llave) && llave.Length == 10)
            {
                var generarLlave = new GenerarLlave();

                var Gllave = generarLlave.GenerarLLaves(llave);
                llave1 = Gllave.Substring(0, 8);
                llave2 = Gllave.Substring(8, 8);
            }
            using (TextWriter escribir = File.CreateText("Llaves.txt"))
            {
                escribir.Write(llave1 + "|"+llave2);
            }

            string leerEn = File.ReadAllText("TextoBinario.txt", Encoding.UTF8);
            string obtenerLLaves = File.ReadAllText("Llaves.txt", Encoding.UTF8);
            string[] LlavesArchivo = obtenerLLaves.Split('|');

            leerEn =leerEn.Remove(leerEn.Length - 1);
            
            string[] cosas = leerEn.Split('|');
            string[] cifrado = new string[cosas.Length];

            for (int i = 0; i < cosas.Length; i++)
            {
                var encriptar = new Encriptar();
                cifrado[i] = encriptar.encriptar(LlavesArchivo[0], LlavesArchivo[1], cosas[i]);

            }
            using (TextWriter writer = File.CreateText("Cifrados.txt"))
            {

                foreach (string i in cifrado)
                {
                    writer.Write(i+"|");
                }
            }


            Console.WriteLine("Se creo el archivo con los datos cifrados");


            string leerDes = File.ReadAllText("Cifrados.txt", Encoding.UTF8);
            leerDes = leerDes.Remove(leerDes.Length - 1);
            string[] cosasDes = leerDes.Split('|');
            string[] Descifrado = new string[cosasDes.Length];

            for (int i = 0; i < cosasDes.Length; i++)
            {

                var desencriptar = new Desencriptar();

                Descifrado[i]= desencriptar.desencriptar(LlavesArchivo[0], LlavesArchivo[1], cosasDes[i]);


            }
            using (TextWriter writer = File.CreateText("DescifradoBin.txt"))
            {

                foreach (string i in Descifrado)
                {
                    writer.Write(i + "|");
                }
            }
            Console.WriteLine("Se creo el archivo con los datos descifrados pero en binario");
            string convertir = File.ReadAllText("DescifradoBin.txt", Encoding.UTF8);
            convertir = convertir.Remove(convertir.Length - 1);
            string[] aConvertir = convertir.Split('|');
            string s;
            string result = "";
            for (int i = 0; i < aConvertir.Length; i++)
            {


                s = aConvertir[i];
                var first8 = s.Substring(0, 8);
                var number = Convert.ToInt32(first8, 2);
                result += (char)number;
                
            }
            System.IO.File.WriteAllText(@"Original.txt", result);
            Console.WriteLine("Se creo el archivo con los datos originales");


        }
        static bool esBinario(string s)
        {
            foreach (var c in s)
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }
            return true;
        }
        static Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }
    }
}
