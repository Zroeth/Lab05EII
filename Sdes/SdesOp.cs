using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdes
{
    public static class SdesOp
    {
        public static void Cifrar(string path, string root, int llaveI)
        {

            var generarLlave = new GenerarLlave();

            string llave = Convert.ToString(llaveI, 2).PadLeft(10, '0');
            string llave1 = "";
            string llave2 = "";
            var Gllave = generarLlave.GenerarLLaves(llave);
            llave1 = Gllave.Substring(0, 8);
            llave2 = Gllave.Substring(8, 8);
            string root1 = root + @"\\Upload\\" + "Llaves" + Path.GetFileNameWithoutExtension(path) + ".txt";
            using (TextWriter escribir = File.CreateText(root1))
            {
                escribir.Write(llave1 + "|"+llave2);
            }

            string leer = File.ReadAllText(@path, Encoding.UTF8);

            char[] charUwu = leer.ToCharArray();
            string[] binarios = new string[charUwu.Length];
            for (int i = 0; i < charUwu.Length; i++)
            {
                binarios[i] = Convert.ToString(charUwu[i], 2).PadLeft(8, '0').PadRight(0, '0').Substring(0, 8);
            }
            string root2= root + @"\\Upload\\" + "TextoBinario" + Path.GetFileNameWithoutExtension(path) + ".txt";
            using (TextWriter escribir = File.CreateText(root2))
            {
                foreach (string i in binarios)
                {
                    escribir.Write(i + "|");
                }
            }

            string leerEn = File.ReadAllText(root2, Encoding.UTF8);
            string obtenerLLaves = File.ReadAllText(root1, Encoding.UTF8);
            string[] LlavesArchivo = obtenerLLaves.Split('|');

            leerEn = leerEn.Remove(leerEn.Length - 1);

            string[] cosas = leerEn.Split('|');
            string[] cifrado = new string[cosas.Length];

            for (int i = 0; i < cosas.Length; i++)
            {
                var encriptar = new Encriptar();
                cifrado[i] = encriptar.encriptar(LlavesArchivo[0], LlavesArchivo[1], cosas[i]);

            }
            string root3 = root + @"\\Upload\\"  + Path.GetFileNameWithoutExtension(path) + ".sdes";
            using (TextWriter escribir = File.CreateText(root3))
            {

                foreach (string i in cifrado)
                {
                    escribir.Write(i + "|");
                }
            }




        }

        public static void Descifrar(string path, string root, int llaveI)
        {
            var generarLlave = new GenerarLlave();

            string llave = Convert.ToString(llaveI, 2).PadLeft(10, '0');
            string llave1 = "";
            string llave2 = "";
            var Gllave = generarLlave.GenerarLLaves(llave);
            llave1 = Gllave.Substring(0, 8);
            llave2 = Gllave.Substring(8, 8);
            string root1 = root + @"\\Upload\\" + "Llaves2" + Path.GetFileNameWithoutExtension(path) + ".txt";
            using (TextWriter escribir = File.CreateText(root1))
            {
                escribir.Write(llave1 + "|" + llave2);
            }

        
            string obtenerLLaves = File.ReadAllText(root1, Encoding.UTF8);
            string[] LlavesArchivo = obtenerLLaves.Split('|');

            string leerEn = File.ReadAllText(@path, Encoding.UTF8);
            leerEn = leerEn.Remove(leerEn.Length - 1);

            string[] cosas = leerEn.Split('|');
            string[] cifrado = new string[cosas.Length];

            for (int i = 0; i < cosas.Length; i++)
            {
                var encriptar = new Desencriptar();
                cifrado[i] = encriptar.desencriptar(LlavesArchivo[0], LlavesArchivo[1], cosas[i]);

            }
            string root3 = root + @"\\Upload\\" +"Binario"+ Path.GetFileNameWithoutExtension(path) + ".txt";
            using (TextWriter escribir = File.CreateText(root3))
            {

                foreach (string i in cifrado)
                {
                    escribir.Write(i + "|");
                }
            }

            string convertir = File.ReadAllText(root3, Encoding.UTF8);
            convertir = convertir.Remove(convertir.Length - 1);
            string[] aConvertir = convertir.Split('|');
            string s;
            string final = "";
            for (int i = 0; i < aConvertir.Length; i++)
            {


                s = aConvertir[i];
                var primeros = s.Substring(0, 8);
                var enNumero = Convert.ToInt32(primeros, 2);
                final += (char)enNumero;

            }
            string root4 = root + @"\\Upload\\" +  Path.GetFileNameWithoutExtension(path) + ".txt";
            File.WriteAllText(root4, final);





        }
    }
}
